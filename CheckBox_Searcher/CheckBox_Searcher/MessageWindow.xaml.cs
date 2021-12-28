using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CheckBox_Searcher
{
    /// <summary>
    /// Interaction logic for MessgeWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        bool MyDialogResult;
        public MessageWindow()
        {
            InitializeComponent();
            MyDialogResult = false;
            Message.Text = "Are you sure you want to delete this info?";
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            MyDialogResult = true;
            this.Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = MyDialogResult;
        }
    }
}
