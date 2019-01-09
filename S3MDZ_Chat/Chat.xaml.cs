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
        
        BitmapImage bi;



        public Chat()
        {
            DiffieHellman.GenerateKey();            
            ConnectionManager.ReceiveMessage(MessageReceived);            
            InitializeComponent();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.ScrollToBottom();
            bi = new BitmapImage(new Uri(@"C:\Users\Ryker\source\repos\S3MDZ_Chat\S3MDZ_Chat\Assets\anon.png"));
            
            
            

        }

        private void Button_Send(object sender, RoutedEventArgs e)
        {
            if(textBoxUserInput.Text != "") {
                
                Run run = new Run(textBoxUserInput.Text);                                
                Paragraph paragraph = new Paragraph(run);                
                paragraph.TextAlignment = TextAlignment.Right;
                paragraph.Background = Brushes.LightBlue;
                paragraph.Padding = new Thickness(10,5,10,5);
                chatBlockMain.Document.Blocks.Add(paragraph);           
                //ConnectionManager.Send(AES.EncryptMessage(textBoxUserInput.Text));
                textBoxUserInput.Text = "";
                scrollViewer.ScrollToBottom();
            }

        }

        private void Button_EndConnection(object sender, RoutedEventArgs e)
        {

        }

        private void MessageReceived(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                Image image = new Image();
                image.Source = bi;
                image.Width = 25;
                image.Height = 25;
                InlineUIContainer container = new InlineUIContainer(image);
                Run run = new Run(AES.DecryptMessage(message));
                Paragraph paragraph = new Paragraph(run);
                paragraph.TextAlignment = TextAlignment.Left;
                paragraph.Background = Brushes.LightGray;
                paragraph.Padding = new Thickness(10, 5, 10, 5);
                chatBlockMain.Document.Blocks.Add(paragraph);
                scrollViewer.ScrollToBottom();
            });

        }
    }
}
