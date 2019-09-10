using System;
using System.Net;
using System.IO;

namespace wiki_parser
{
    class ParserTitle : IParser
    {
        /// <summary>
        /// Find title tag
        /// </summary>
        /// <param name="s"> string to check </param>
        /// <returns>value of title tag</returns>
        private string FindName(string s)
        {
            int start = s.IndexOf("<title>") + 7;
            int end = s.IndexOf("</title>");
            if (end > 0)
            {
                s = s.Substring(start, end - start);
                int start_1 = s.IndexOf(" - Wikipedia");
                int start_2 = s.IndexOf(" — Википедия");
                if (start_1 + start_2 > 0)
                    return s.Substring(0, start_1 + start_2 + 1);
                else

                return s.Substring(0, start + 1);
            }
            else
                return null;
        }

        /// <summary>
        /// Connect to url and find title value
        /// </summary>
        /// <param name="url"> link to page to find </param>
        /// <returns> title value </returns>
        public string[] GetData(string url)
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            string title = "";

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (FindName(line) != null)
                        {
                            title = FindName(line);
                            break;
                        }
                    }
                }
            }
            return new [] { title };
        }   
    }
}
