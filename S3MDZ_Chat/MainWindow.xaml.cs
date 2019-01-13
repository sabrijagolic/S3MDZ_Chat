using System;
using System.Collections.Generic;
using System.Linq;
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
using S3MDZ_Chat.Encription;
using S3MDZ_Chat.Connection;
using System.Threading;

namespace S3MDZ_Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool requestSending = false;

        public MainWindow()
        {                       
            InitializeComponent();
            
            ConnectionManager.ListenForRemoteGuest(StartChat, AcceptConnection, HideProgressbar);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                if (requestSending == false)
                {
                    requestSending = true;
                    IPTextBox.IsEnabled = false;
                    ConnectionProgressBar.Visibility = Visibility.Visible;
                    ConnectionLabel.Visibility = Visibility.Visible;
                    ConnectionManager.StartChat(IPTextBox.Text);
                    ConnectButton.Content = "Cancel connection";
                }
                else
                {
                    requestSending = false;
                    ConnectButton.Content = "Connect";
                    HideProgressbar();
                }
            
        }

        private void StartChat()
        {
            this.Dispatcher.Invoke(() =>
            {
                ConnectionProgressBar.Visibility = Visibility.Hidden;
                ConnectionLabel.Visibility = Visibility.Hidden;
                Chat chat = new Chat(this);
                chat.Show();
                requestSending = false;
                IPTextBox.Text = "";                
                ConnectButton.Content = "Connect";
                HideProgressbar();
                this.Hide();
            });
        }

        private void AcceptConnection(Action<string> callback)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to accept a connection?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                callback("2");
            }
            else if (result == MessageBoxResult.No)
            {
                callback("3");
            }
        }

        public void HideProgressbar()
        {
            ConnectionProgressBar.Visibility = Visibility.Hidden;
            ConnectionLabel.Visibility = Visibility.Hidden;
            IPTextBox.IsEnabled = true;
        }

        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }
            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }
            byte tempForParsing;
            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        

        private void IPTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidateIPv4(IPTextBox.Text))
            {
                ConnectButton.IsEnabled = true;
                IPTextBox.Background = Brushes.White;
                ConnectButton.Content = "Connect";

            }
            else
            {
                ConnectButton.IsEnabled = false;
                IPTextBox.Background = Brushes.Red;
                ConnectButton.Content = "Invalid IP";
                

            }
        }
    }
}
