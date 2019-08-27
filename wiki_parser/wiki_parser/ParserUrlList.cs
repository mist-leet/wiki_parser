using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace wiki_parser
{
    class ParserUrlList : IParser
    {
        public string[] GetData(string url)
        {
            var urls = new List<string>();
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    int i = 0;
                    string line = "";
                    string pattern = "<li><a href=\"(/wiki/(\\S)*)\" title=";
                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        var m = Regex.Match(line, pattern);
                        if (m.Success)
                        {
                            urls.Add(m.Groups[1].ToString());
                        }
                    }
                }
            }

            return urls.Select(u => $"https://ru.wikipedia.org{u}").ToArray();
        }
    }
}
