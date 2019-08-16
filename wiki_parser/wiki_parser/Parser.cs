using System;
using System.Net;
using System.IO;


namespace wiki_parser
{
    class Parser
    {
        private IParser _setting;

        public Parser(IParser setting)
        {
            _setting = setting;
        }
        
        public string[] Parse(string url)
        {
            return _setting.GetData(url);
        }

    }
}
