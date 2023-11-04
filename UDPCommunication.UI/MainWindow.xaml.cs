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
using UDPCommunication.Service.Services;

namespace UDPCommunication.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UdpClient udpClient;

        public MainWindow()
        {
            InitializeComponent();
            txtDestIP.Focus();
            ManageMessagePanelActivity(false);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDestIP.Text.Trim()))
            {
                MessageBox.Show("Hedef IP adresi giriniz");
                return;
            }
            if (string.IsNullOrEmpty(txtDestPort.Text.Trim()))
            {
                MessageBox.Show("Hedef port numarası giriniz");
                return;
            }
            if (string.IsNullOrEmpty(txtSourcePort.Text.Trim()))
            {
                MessageBox.Show("Kaynak port numarası giriniz");
                return;
            }
            int destPort, sourcePort;
            bool success = int.TryParse(txtDestPort.Text.Trim(), out destPort);
            if (!success)
            {
                MessageBox.Show("Hedef port numarası için nümerik bir değer giriniz");
                return;
            }
            success = int.TryParse(txtSourcePort.Text.Trim(), out sourcePort);
            if (!success)
            {
                MessageBox.Show("Kaynak port numarası için nümerik bir değer giriniz");
                return;
            }
            OperationResult<UdpClient> connection = new UDPService().OpenConnection(txtDestIP.Text.Trim(), destPort);
            if (connection.Success)
            {
                udpClient = connection.Result;
                ManageMessagePanelActivity(true);
                txtMessage.Focus();
            }
            else
            {
                MessageBox.Show(connection.Message);
                ManageMessagePanelActivity(false);
            }
        }

        public void ManageMessagePanelActivity(bool isEnabled)
        {
            txtMessage.IsEnabled = isEnabled;
            btnSend.IsEnabled = isEnabled;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
            {
                MessageBox.Show("Gönderilecek mesaj bilgisi giriniz");
                return;
            }
            string a = new CryptoService().Encrypt(txtMessage.Text.Trim()).Result;
            string b = new CryptoService().Decrypt(a).Result;
        }
    }
}
