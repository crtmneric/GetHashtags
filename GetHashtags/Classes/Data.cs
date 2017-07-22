using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetHashtags.Classes
{
    class Data
    {
        public Data()
        {
        }

        private string user;
        private string twit;
        public string Twit
        {
            get
            {
                return twit;
            }
            set
            {
                twit = value;
            }
        }
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }
       
    }
}
