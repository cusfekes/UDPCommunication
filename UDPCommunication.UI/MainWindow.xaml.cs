using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using UDPCommunication.Models;
using UDPCommunication.Models.CustomEventArgs;
using UDPCommunication.Models.DomainModels;
using UDPCommunication.Models.Enums;
using UDPCommunication.Service.Interfaces;
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
        private readonly IUDPService udpService;
        private DispatcherTimer connectionTimer;
        private int seconds = 0;

        public MainWindow(IUDPLogService _udpLogService, ICryptoService _cryptoService, IUDPService _udpService)
        {
            InitializeComponent();
            // Define example ip and port values
            txtSourceIP.Text = "127.0.0.1";
            txtSourcePort.Text = "9090";

            txtDestIP.Text = "127.0.0.2";
            txtDestPort.Text = "9090";
            btnListen.Tag = UDPListenStatusEnum.NotListening;

            udpLogService = _udpLogService;
            udpService = _udpService;
            cryptoService = _cryptoService;

            // Create timer object
            connectionTimer = new DispatcherTimer();
            connectionTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            connectionTimer.Interval = new TimeSpan(0, 0, 1);

            udpService.udpMessageFired += UdpMessageFired;
            LoadUDPMessages();
        }

        private void LoadUDPMessages()
        {
            // Load the messages from database
            OperationResult<List<UDPLog>> result = udpLogService.GetAllItems();
            if (result.Success)
            {
                List<UDPLog> source = result.Result;
                gridSource.Items.Clear();
                foreach (UDPLog log in source)
                    gridSource.Items.Add(log);
            }
            else
                MessageBox.Show(result.Message);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            seconds++;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            lblTimer.Content = "Bağlantı Süresi: " + time.ToString();
        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {
            // This is working as toggle button. Starts or stops listening the given ip address and port number
            UDPListenStatusEnum status = (UDPListenStatusEnum)Enum.Parse(typeof(UDPListenStatusEnum), btnListen.Tag.ToString());
            if (status == UDPListenStatusEnum.NotListening)
                StartListen();
            else if (status == UDPListenStatusEnum.Listening)
                StopListen();
        }

        private async void StartListen()
        {
            // Starts to listening
            OperationResult<IPEndPoint> result = UDPUtil.CreateIPEndPoint(txtSourceIP.Text.Trim(), txtSourcePort.Text.Trim());
            if (result.Success)
            {
                btnListen.Tag = UDPListenStatusEnum.Listening;
                btnListen.Content = "Dinleniyor...";
                connectionTimer.Start();
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
            //Stops to listening
            await udpService.StopListening();
            connectionTimer.Stop();
            btnListen.Content = "Bağlan";
            btnListen.Tag = UDPListenStatusEnum.NotListening;
        }

        private void UdpMessageFired(object sender, UDPPacketArgs e)
        {
            // Event fired while sending or listening any messages. Decrypt the message then save to database
            OperationResult<string> result = cryptoService.Decrypt(e.GetData().Message);
            if (result.Success)
            {
                string decryptedMessage = result.Result;
                UDPLog udpLog = e.GetData();
                udpLog.Message = decryptedMessage;
                SaveUDPMessage(udpLog);
            }
        }

        private void SaveUDPMessage(UDPLog udpLog)
        {
            // Save the message to database
            gridSource.Items.Add(udpLog);
            txtMessage.Clear();
            OperationResult<bool> result = udpLogService.SaveItem(udpLog);
            if (!result.Success)
                MessageBox.Show(result.Message);
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // Send a message to given ip address and port number
            if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
            {
                MessageBox.Show("Gönderilecek mesaj bilgisi giriniz");
                return;
            }

            OperationResult<IPEndPoint> result = UDPUtil.CreateIPEndPoint(txtDestIP.Text.Trim(), txtDestPort.Text.Trim());
            if (result.Success)
            {
                // Encrypt the message then save to given ip address and port number
                string encryptedMessage = cryptoService.Encrypt(txtMessage.Text.Trim()).Result;
                await udpService.SendMessageAsync(result.Result, encryptedMessage);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected message from database
            if (gridSource.SelectedItem == null)
            {
                MessageBox.Show("Silmek istediğiniz öğeyi seçiniz");
                return;
            }
            MessageBoxResult dialogResult = MessageBox.Show("Seçili öğeyi silmek istiyor musunuz?", "Uyarı", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                UDPLog udpLog = (UDPLog)gridSource.SelectedItem;
                OperationResult<bool> result = udpLogService.DeleteItem(udpLog.Id);
                if (result.Success)
                    LoadUDPMessages();
                else
                    MessageBox.Show(result.Message);
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            // Search messages by filtering sending or listening date
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
            // Clear date selections in UI
            dtStartDate.SelectedDate = null;
            dtEndDate.SelectedDate = null;
            LoadUDPMessages();
        }
    }
}
