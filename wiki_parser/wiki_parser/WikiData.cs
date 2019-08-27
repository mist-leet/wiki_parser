using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wiki_parser
{
    class WikiData
    {
        public string[] title;
        public string[] img_url;
        public WikiData(string[] urls)
        {   
            for(int i = 0; i < 4; i++)
            {
                Array.Resize<string>(ref title, title.Length + 1);
                title[i] = ParserTitle.GetData(urls[i])[0];
                s
            }
            
        }
}
