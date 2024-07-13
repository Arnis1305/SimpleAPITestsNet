using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPITestNet.Models.GetUser
{
    public class Data
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }

        public Data(){}

        public Data(int id, string email, string first_name, string last_name, string avatar)
        {
            this.id = id;
            this.email = email; 
            this.first_name = first_name;
            this.last_name = last_name;
            this.avatar = avatar;
        }

        public override bool Equals(object? obj)
        {
            return obj is Data data &&
                   id == data.id &&
                   email == data.email &&
                   first_name == data.first_name &&
                   last_name == data.last_name &&
                   avatar == data.avatar;
        }
    }
}
