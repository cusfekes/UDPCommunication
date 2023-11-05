using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UDPCommunication.Models;
using UDPCommunication.Models.CustomEventArgs;
using UDPCommunication.Models.Enums;
using UDPCommunication.Service.Services;
using UDPCommunication.UI.Utils;

namespace UDPCommunication.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UDPService udpService;

        private CryptoService cryptoService;

        public MainWindow()
        {
            InitializeComponent();
            txtSourceIP.Text = "127.0.0.1";
            txtSourcePort.Text = "9090";

            txtDestIP.Text = "127.0.0.2";
            txtDestPort.Text = "9090";
            btnListen.Tag = UDPListenStatusEnum.NotListening;

            udpService = new UDPService();
            cryptoService = new CryptoService();
            udpService.udpMessageFired += UdpMessageFired;
        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {
            UDPListenStatusEnum status = (UDPListenStatusEnum)Enum.Parse(typeof(UDPListenStatusEnum), btnListen.Tag.ToString());
            if (status == UDPListenStatusEnum.NotListening)
                StartListen();
            else if (status == UDPListenStatusEnum.Listening)
                StopListen();


            //if (string.IsNullOrEmpty(txtDestIP.Text.Trim()))
            //{
            //    MessageBox.Show("Hedef IP adresi giriniz");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtDestPort.Text.Trim()))
            //{
            //    MessageBox.Show("Hedef port numarası giriniz");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtSourcePort.Text.Trim()))
            //{
            //    MessageBox.Show("Kaynak port numarası giriniz");
            //    return;
            //}
            //int destPort, sourcePort;
            //bool success = int.TryParse(txtDestPort.Text.Trim(), out destPort);
            //if (!success)
            //{
            //    MessageBox.Show("Hedef port numarası için nümerik bir değer giriniz");
            //    return;
            //}
            //success = int.TryParse(txtSourcePort.Text.Trim(), out sourcePort);
            //if (!success)
            //{
            //    MessageBox.Show("Kaynak port numarası için nümerik bir değer giriniz");
            //    return;
            //}
            //OperationResult<UdpClient> connection = new UDPService().OpenConnection(txtDestIP.Text.Trim(), destPort);
            //if (connection.Success)
            //{
            //    udpClient = connection.Result;
            //    ManageMessagePanelActivity(true);
            //    txtMessage.Focus();
            //}
            //else
            //{
            //    MessageBox.Show(connection.Message);
            //    ManageMessagePanelActivity(false);
            //}
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
            if(result.Success)
            {
                string decryptedMessage = result.Result;
                UDPPacket udpPacket = e.Data;
                udpPacket.Message = decryptedMessage;
                gridSource.Items.Add(udpPacket);
            }
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
            UDPPacket udpPacket = (UDPPacket)gridSource.SelectedItem;
        }
    }
}
