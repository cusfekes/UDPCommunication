using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using UDPCommunication.Models;
using UDPCommunication.Models.CustomEventArgs;
using UDPCommunication.Models.DomainModels;
using UDPCommunication.Models.Enums;
using UDPCommunication.Service.Interfaces;
using UDPCommunication.Service.Services;
using UDPCommunication.UI.Utils;

namespace UDPCommunication.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICryptoService cryptoService;
        private readonly IUDPLogService udpLogService;
        private UDPService udpService;

        public MainWindow(IUDPLogService _udpLogService, ICryptoService _cryptoService)
        {
            InitializeComponent();
            txtSourceIP.Text = "127.0.0.1";
            txtSourcePort.Text = "9090";

            txtDestIP.Text = "127.0.0.2";
            txtDestPort.Text = "9090";
            btnListen.Tag = UDPListenStatusEnum.NotListening;

            udpLogService = _udpLogService;
            udpService = new UDPService();
            cryptoService = _cryptoService;
            udpService.udpMessageFired += UdpMessageFired;
            LoadUDPMessages();
        }

        private void LoadUDPMessages()
        {
            OperationResult<List<UDPLog>> result = udpLogService.GetAllItems();
            if (result.Success)
            {
                List<UDPLog> source = result.Result;
                gridSource.Items.Clear();
                foreach (UDPLog log in source)
                    gridSource.Items.Add(log);
            }
        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {
            UDPListenStatusEnum status = (UDPListenStatusEnum)Enum.Parse(typeof(UDPListenStatusEnum), btnListen.Tag.ToString());
            if (status == UDPListenStatusEnum.NotListening)
                StartListen();
            else if (status == UDPListenStatusEnum.Listening)
                StopListen();
        }

        private async void StartListen()
        {
            OperationResult<IPEndPoint> result = UDPUtil.CreateIPEndPoint(txtSourceIP.Text.Trim(), txtSourcePort.Text.Trim());
            if (result.Success)
            {
                btnListen.Tag = UDPListenStatusEnum.Listening;
                btnListen.Content = "Dinleniyor...";
                await udpService.StartListening(result.Result);
            }
            else
            {
                MessageBox.Show(result.Message);
                btnListen.Tag = UDPListenStatusEnum.NotListening;
                btnListen.Content = "Bağlan";
            }
        }

        private async void StopListen()
        {
            await udpService.StopListening();
            btnListen.Content = "Bağlan";
            btnListen.Tag = UDPListenStatusEnum.NotListening;
        }

        private void UdpMessageFired(object sender, UDPPacketArgs e)
        {
            OperationResult<string> result = cryptoService.Decrypt(e.Data.Message);
            if (result.Success)
            {
                string decryptedMessage = result.Result;
                UDPLog udpLog = e.Data;
                udpLog.Message = decryptedMessage;
                SaveUDPMessage(udpLog);
            }
        }

        private void SaveUDPMessage(UDPLog udpLog)
        {
            gridSource.Items.Add(udpLog);
            txtMessage.Clear();
            OperationResult<bool> result = udpLogService.SaveItem(udpLog);
            if (!result.Success)
                MessageBox.Show(result.Message);
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
            {
                MessageBox.Show("Gönderilecek mesaj bilgisi giriniz");
                return;
            }

            OperationResult<IPEndPoint> result = UDPUtil.CreateIPEndPoint(txtDestIP.Text.Trim(), txtDestPort.Text.Trim());
            if (result.Success)
            {
                string encryptedMessage = cryptoService.Encrypt(txtMessage.Text.Trim()).Result;
                await udpService.SendMessageAsync(result.Result, encryptedMessage);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridSource.SelectedItem == null)
            {
                MessageBox.Show("Silmek istediğiniz öğeyi seçiniz");
                return;
            }
            UDPLog udpLog = (UDPLog)gridSource.SelectedItem;
            OperationResult<bool> result = udpLogService.DeleteItem(udpLog.Id);
            if (result.Success)
                LoadUDPMessages();
            else
                MessageBox.Show(result.Message);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (dtStartDate.SelectedDate == null || dtEndDate.SelectedDate == null)
            {
                MessageBox.Show("Başlangıç ve bitiş tarihi seçiniz");
                return;
            }
            if (DateTime.Compare(dtStartDate.SelectedDate.Value, dtEndDate.SelectedDate.Value) == 1)
            {
                MessageBox.Show("Başlangıç tarihi bitiş tarihinden sonra olamaz");
                return;
            }
            OperationResult<List<UDPLog>> result = udpLogService.GetItemsByDateRange(dtStartDate.SelectedDate.Value, dtEndDate.SelectedDate.Value);
            if (result.Success)
            {
                List<UDPLog> filterList = result.Result;
                gridSource.Items.Clear();
                foreach (UDPLog log in filterList)
                    gridSource.Items.Add(log);

            }
            else
                MessageBox.Show(result.Message);
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            dtStartDate.SelectedDate = null;
            dtEndDate.SelectedDate = null;
            LoadUDPMessages();
        }
    }
}
