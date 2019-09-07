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
        public bool is_only_russian = true;

        public string[] GetData(string url)
        {
            //string[] categories = GetCategoryUrl(url);
            string[] categories = GetCategorySpecialUrl(url);
            var urls = new List<string>();

            //if (categories.Length == 0)
            //    categories = GetCategorySpecialUrl(url);
            

            foreach (string _url in categories)
            {
                WebRequest req = WebRequest.Create(_url);
                WebResponse res = req.GetResponse();

                using (Stream stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        string pattern = "<li><a href=\"(/wiki/(\\S)*)\" title=";

                        while ((line = reader.ReadLine()) != null)
                        {
                            for (int i = 0; line.IndexOf("class=\"mw-category-group\"") < 0; i++)
                                line = reader.ReadLine();

                            string first_sym = Regex.Match(line, "<h3>(\\S)?</h3>").Groups[1].ToString();

                            while ((line = reader.ReadLine()) != null)
                            {

                                var m = Regex.Match(line, pattern + "\"[" + first_sym + "]");
                                if (m.Success)
                                    urls.Add(m.Groups[1].ToString());
                                else
                                    break;
                            }
                            break;
                        }

                    }
                }
            }


            return urls.Select(u => $"https://ru.wikipedia.org{u}").ToArray();
        }

        private string[] GetCategoryUrl(string url)
        {
            var urls = new List<string>();
            string line = "";
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    // <a class="external text" href="https://ru.wikipedia.org/w/index.php?title=%D0%9A%D0">Ё</a>
                    while ((line != null
                        && line.IndexOf("<a class=\"external text\"") < 0))
                        line = reader.ReadLine();
                    line = reader.ReadLine();

                }
            }
            string pattern;

            if (is_only_russian)
                pattern = "<a class=\"external text\" href=\"(\\S*)\">[а-яА-Я]</a>";
            else
                pattern = "<a class=\"external text\" href=\"(\\S*)\">\\S+</a>";

            if (Regex.Match(line, pattern).Success)
                foreach (Match match in Regex.Matches(line, pattern))
                    urls.Add(Regex.Replace(match.Groups[1].ToString(), "amp;", ""));

            return urls.ToArray();
        }

        private string[] GetCategorySpecialUrl(string url)
        {
            var urls = new List<string>();
            string line = "";
            string pattern;
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();


            if (is_only_russian)
                pattern = "<a class=\"external text\" href=\"(\\S*)\">[а-яА-Я]</a>";
            else
                pattern = "<a class=\"external text\" href=\"(\\S*)\">\\S+</a>";

            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    // <a class="external text" href="https://ru.wikipedia.org/w/index.php?title=%D0%9A%D0">Ё</a>
                    while ((line != null
                        && line.IndexOf("<a class=\"external text\"") < 0))
                        line = reader.ReadLine();
                    while ((line != null
                        && line.IndexOf("<a class=\"external text\"") > 0))
                    {
                        if (Regex.Match(line, pattern).Success)
                            foreach (Match match in Regex.Matches(line, pattern))
                                urls.Add(Regex.Replace(match.Groups[1].ToString(), "amp;", ""));
                        line = reader.ReadLine();
                    }
                }
            }

            return urls.ToArray();
        }
    }
}
