using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPITestNet.Models.GetUser
{
    public class SingleUser
    {
        public Data data {  get; set; }
        public Support support { get; set; }

        public SingleUser(){}

        public SingleUser(Data data, Support support)
        {
            this.data = data;
            this.support = support;
        }

        public override bool Equals(object? obj)
        {
            return obj is SingleUser user &&
                   EqualityComparer<Data>.Default.Equals(data, user.data) &&
                   EqualityComparer<Support>.Default.Equals(support, user.support);
        }
    }
}
