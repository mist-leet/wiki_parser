using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wiki_parser
{
    class Parser
    {
        private IParser _setting;

        public Parser(IParser setting)
        {
            _setting = setting;
        }
        
        public WikiData Parse(string url)
        {
             return _setting.GetData(url);
        }
    }
}
