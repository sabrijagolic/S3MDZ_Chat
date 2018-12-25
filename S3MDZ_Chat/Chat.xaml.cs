using S3MDZ_Chat.Connection;
using S3MDZ_Chat.Encription;
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
using System.Windows.Shapes;

namespace S3MDZ_Chat
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        public Chat()
        {
            DiffieHellman.GenerateKey();
            ConnectionManager.ReceiveMessage(MessageReceived);
            InitializeComponent();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.ScrollToBottom();
            

        }

        private void Button_Send(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(AES.IsNull());
            chatBlockMain.TextAlignment = TextAlignment.Right;
            chatBlockMain.Text += "\n Ja: " + textBoxUserInput.Text; 
            ConnectionManager.Send(AES.EncryptMessage(textBoxUserInput.Text));
            textBoxUserInput.Text = "";
            scrollViewer.ScrollToBottom();

        }

        private void Button_EndConnection(object sender, RoutedEventArgs e)
        {

        }

        private void MessageReceived(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                chatBlockMain.TextAlignment = TextAlignment.Left;
                chatBlockMain.Text += "\n  Neko:" + AES.DecryptMessage(message);
                scrollViewer.ScrollToBottom();
            });
            
        }
    }
}
