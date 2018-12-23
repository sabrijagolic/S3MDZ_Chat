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


        public MainWindow()
        {
            InitializeComponent();            
            ConnectionManager.ListenForRemoteGuest(StartChat, AcceptConnection);
            //AcceptConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionManager.StartChat(IPTextBox.Text);
            Chat chat = new Chat();
            chat.Show();
            this.Close();
        }
        private void StartChat()
        {
            this.Dispatcher.Invoke(() =>
            {
                Chat chat = new Chat();
                chat.Show();
                this.Close();   
            });
        }
        private void AcceptConnection(Action<string> callback)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to accept a connection?",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                callback("2");
            } else if (result == MessageBoxResult.No)
            {
                callback("3");
            }
        }
    }
}
