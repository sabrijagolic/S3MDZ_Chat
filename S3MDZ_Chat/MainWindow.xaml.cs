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
            
            ConnectionManager.Receive();
            
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionManager.InitializeConnectionManager(IPTextBox.Text);
            ConnectionManager.Send();
            Chat chat = new Chat();
            chat.Show();
            this.Close();
        }
    }
}
