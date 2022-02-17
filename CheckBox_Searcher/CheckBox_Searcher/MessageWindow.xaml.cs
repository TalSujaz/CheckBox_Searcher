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

        #region Ctor

        /// <summary>
        /// Creates the MessageWindow
        /// </summary>
        public MessageWindow()
        {
            InitializeComponent();
            MyDialogResult = false;
            Message.Text = "Are you sure you want to delete this info?";
        }
        #endregion

        #region UI methods

        /// <summary>
        /// Changes MyDialogResult to true and close the win
        /// </summary>
        /// <param name="sender">The YesBtn clicked.</param>
        /// <param name="e">Parameters associated to the mouse event.</param>
        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            MyDialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Close the win
        /// </summary>
        /// <param name="sender">The NoBtn clicked.</param>
        /// <param name="e">Parameters associated to the mouse event.</param>
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        /// <summary>
        /// Changes MyDialogResult to true and close the win
        /// </summary>
        /// <param name="sender">The X btn is clicked.</param>
        /// <param name="e">Parameters associated to the mouse event.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = MyDialogResult;
        }
        #endregion
    }
}
