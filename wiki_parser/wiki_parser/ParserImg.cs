using System;
using System.Net;
using System.IO;

namespace wiki_parser
{
    class ParserImg : IParser
    {
        /// <summary>
        /// Search a link to a picture in string
        /// </summary>
        /// <param name="s"> string to check </param>
        /// <returns> link to a picture or null if there isn't </returns>
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

        /// <summary>
        /// Connect to url and find link to the first picture
        /// </summary>
        /// <param name="url"> link to page to find a picture </param>
        /// <returns></returns>
        public string[] GetData(string url)
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            string img = "";

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (FindPicUrl(line) != null)
                        {
                            img = FindPicUrl(line);
                            break;
                        }
                    }
                }
            }
            return new[] { img };
        }
    }
}
