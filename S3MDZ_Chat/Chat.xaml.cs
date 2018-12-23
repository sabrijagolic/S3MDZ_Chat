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
            InitializeComponent();
            //DiffieHellman.InitializeExchange();
            //AES.InitializeEncryptor(null);            
            //chatBlockMain.Text = AES.EncryptMessage("OMEGALUL");
            //chatBlockMain.Text = AES.DecryptMessage(chatBlockMain.Text);         
            //ConnectionManager.InitializeConnectionManager();
            //ConnectionManager.Receive();

        
        }

        private void Button_Send(object sender, RoutedEventArgs e)
        {
           
            //chatBlockMain.Text = AES.EncryptMessage(textBoxUserInput.Text);
            //ConnectionManager.Send();
        }

        private void Button_EndConnection(object sender, RoutedEventArgs e)
        {

        }
    }
}
