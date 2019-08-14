using System;
using System.Net;
using System.IO;

namespace wiki_parser
{
    class ParserTitle : IParser
    {
        private string FindName(string s)
        {
            int start = s.IndexOf("<title>") + 7;
            int end = s.IndexOf("</title>");
            if (end > 0)
                return s.Substring(start, end - start);
            else
                return null;
        }
        public WikiData GetData(string url)
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            string title = "";

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null
                        && title == "" )
                    {
                        if (FindName(line) != null && title == "")
                            title = FindName(line);
                    }
                }
            }
            return new WikiData(title, "");
        }
    }
}
