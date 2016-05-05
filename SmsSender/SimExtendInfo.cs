using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsSender
{
    class SimExtendInfo
    {
        private string balance;
        private string phone_number;

        // Properties
        public string Balance
        {
            get
            {
                return this.balance;
            }
            set
            {
                this.balance = value;
            }
        }

        public string Phone_number
        {
            get
            {
                return this.phone_number;
            }
            set
            {
                this.phone_number = value;
            }
        }

    }
}
