using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPITestNet.Models.PostUser
{
    public class User
    {
        public string name {  get; set; }
        public string job {  get; set; }
        public string id { get; set; }
        public string createdAt {  get; set; }

        public User(string name, string job, string id, string createdAt)
        {
            this.name = name;
            this.job = job;
            this.id = id;
            this.createdAt = createdAt;
        }

        public User(string name, string job) 
        { 
            this.name = name;
            this.job = job;
        }
        public User(){}
    }
}
