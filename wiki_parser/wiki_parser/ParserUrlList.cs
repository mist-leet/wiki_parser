using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace wiki_parser
{
    class ParserUrlList : IParser
    {
        public string[] GetData(string url)
        {
            string[] urls = new string[1];
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    int i = 0;
                    string line = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        //<li><a href="/wiki/%D0%" title="Австробэйлиецветные">Австробэйлиецветные</a></li>\
                        if (Regex.IsMatch(line, "^<li><a href=\"/wiki/*</a></li>$"))
                        {
                            urls[i] = line;
                            i++;
                            Array.Resize<string>(ref urls, i);
                        }
                    }
                }
            }
            return urls;
        }
    }
}
