using System;
using System.Collections.Generic;
using System.Text;

namespace CheckBox_Searcher.Objects
{
    public class XmlItem
    {
        public string Name;
        public string Phone;
        public string Mail;
        public string Address;

        #region Ctor

        /// <summary>
        /// Creating the XmlItem
        /// </summary>
        public XmlItem(string Name, string Phone, string Mail, string Address)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Mail = Mail;
            this.Address = Address;
        }
        #endregion
    }
}
