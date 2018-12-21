﻿using S3MDZ_Chat.Encription;
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
            DiffieHellman alice = new DiffieHellman("alice");
            DiffieHellman bob = new DiffieHellman("bob");

            AES aliceAES = new AES();
            AES bobAES = new AES();

            aliceAES.InitializeEncryptor(alice,bob);
            bobAES.InitializeEncryptor(bob,alice);
            chatBlockMain.Text = aliceAES.EncryptMessage("OMEGALUL");
            chatBlockMain.Text = bobAES.DecryptMessage(chatBlockMain.Text);
            
            
        }

        private void Button_Send(object sender, RoutedEventArgs e)
        {
           
            //chatBlockMain.Text = AES.EncryptMessage(textBoxUserInput.Text);
        }

        private void Button_EndConnection(object sender, RoutedEventArgs e)
        {

        }
    }
}
