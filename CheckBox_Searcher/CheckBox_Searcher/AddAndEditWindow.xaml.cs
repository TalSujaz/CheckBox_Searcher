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
using CheckBox_Searcher.Helpers;
using CheckBox_Searcher.Objects;

namespace CheckBox_Searcher
{
    /// <summary>
    /// Interaction logic for AddAndEditWindow.xaml
    /// </summary>
    public partial class AddAndEditWindow : Window
    {
        public bool IsEdit;
        public string EditId;
        public AddAndEditWindow()
        {            
            InitializeComponent();
            IsEdit = false;
        }
        public AddAndEditWindow(string ID)
        {
            InitializeComponent();
            XmlItem Item = XmlHelper.FindUser(ID);
            NameBox.Text = Item.Name;
            PhoneBox.Text = Item.Phone;
            MailBox.Text = Item.Mail;
            AddressBox.Text = Item.Address;
            IsEdit = true;
            EditId = ID;
        }
        public AddAndEditWindow(string ID,string ItemElementName)
        {
            InitializeComponent();
            XmlItem Item = XmlHelper.FindUser(ID);
            NameBox.Text = Item.Name;
            PhoneBox.Text = Item.Phone;
            MailBox.Text = Item.Mail;
            AddressBox.Text = Item.Address;
            IsEdit = true;
            EditId = ID;
            switch (ItemElementName)
            {
                case "Name":
                    NameBox.Text = "";
                    break;
                case "Phone":
                    PhoneBox.Text = "";
                    break;
                case "Mail":
                    MailBox.Text = "";
                    break;
                case "Address":
                    AddressBox.Text = "";
                    break;
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string name, phone, mail, address;
            name = NameBox.Text.ToString();
            phone = PhoneBox.Text.ToString();
            mail = MailBox.Text.ToString();
            address = AddressBox.Text.ToString();
            if (!name.Equals("") && !phone.Equals("") && !mail.Equals("") && !address.Equals(""))
            {
                XmlItem Item = new XmlItem(name, phone, mail, address);
                if (IsEdit)
                {
                    XmlHelper.EditUser(EditId, Item);
                    this.Close();
                }
                else
                { 
                    XmlHelper.AddUser(Item);
                    this.Close();
                }
               
            }
            
        }
    }
}
