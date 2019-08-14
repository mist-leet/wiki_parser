using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wiki_parser
{
    class WikiData
    {
        public string title;
        public string img_url;
        public WikiData(string a, string b)
        {
            title = a;
            img_url = b;
        }
        public WikiData(WikiData a, WikiData b)
        {
            title = a.title;
            img_url = b.img_url;
        }
    }
}
