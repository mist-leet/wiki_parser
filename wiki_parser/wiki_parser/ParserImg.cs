using System;
using System.Net;
using System.IO;

namespace wiki_parser
{
    class ParserImg : IParser
    {
        private string FindPicUrl(string s)
        {
            int start = s.IndexOf("https://upload.wikimedia.org/");
            if (start > 0)
            {
                int i;
                for (i = start; s[i] != '\"'; i++) ;
                return s.Substring(start, i - start);
            }
            else
                return null;
        }
        public WikiData GetData(string url)
        {
            WebRequest req = WebRequest.Create("https://en.wikipedia.org/wiki/Saint_Petersburg");
            WebResponse res = req.GetResponse();

            string img = "";

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null
                            || img == "")
                    {
                        if (FindPicUrl(line) != null && img == "")
                            img = FindPicUrl(line);
                    }
                }
            }
            return new WikiData("", img);
        }
    }
}
