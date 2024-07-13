using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPITestNet.Models.GetUser
{
    public class UsersPage
    {
        public int page {  get; set; }
        public int per_page {  get; set; }
        public int total { get; set; }
        public int total_pages {  get; set; }

        public List<Data> data { get; set; }

        public Support support { get; set; }

        public UsersPage() { }

        public UsersPage(int page, int per_page, int total, int total_pages, List<Data> data, Support support)
        {
            this.page = page;
            this.per_page = per_page;
            this.total = total;
            this.total_pages = total_pages;
            this.data = data;
            this.support = support;
        }
    }
}
