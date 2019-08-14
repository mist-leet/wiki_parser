using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
namespace wiki_parser
{
    class ParserUrl
    {
        /// <summary>
        /// Stores all URLS
        /// </summary>
        static List<string> urlArr;
        /// <summary>
        /// checking for URL existence
        /// </summary>
        /// <param name="url"></param>
        /// <returns>true if exist else false</returns>
        public static bool urlExist(string[] url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string HTMLSource;
                    for (int i = 0; i < 4; i++)
                        HTMLSource = wc.DownloadString(url[i]);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Randomizes the selection of four URL
        /// </summary>
        public static string[] url
        {
            get 
            {
                Random rnd = new Random();
                string[] url4 = new string[4];
                if (urlArr.Count < 4)
                {
                    throw new FormatException("not enough URLs");
                }
                for(int i=0; i < 4; i++)
                {
                    int index = rnd.Next(0, urlArr.Count);
                    url4[i] = urlArr[index];
                    urlArr.RemoveAt(index);
                }
                if (!urlExist(url4))
                    throw new FormatException("invalid URL");
                return url4;
            }
        }

        /// <summary>
        /// Pars a url string to a list
        /// </summary>
        /// <param name="urls"> A string of all the URL</param>
        private static void strToUrls(string urls)
        {
            string str = "";
            urlArr = new List<string>();
            for( int i = 0; i < urls.Length; i++)  //https://....//.....https://
            {
                str += urls[i];
                if (i+6<urls.Length && urls[i + 6] == ':')
                {
                    urlArr.Add(str);
                    str = "";
                }
            }
            urlArr.Add(str);
        }

        /// <summary>
        /// Parse URLS from a text file to an array of four random URLs
        /// </summary>
        /// <param name="path"> The path to the file </param>
        /// <returns> The output is fed 4 random URLS from the file </returns>
        public static string[] urls(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string stringUrls = System.Text.Encoding.Default.GetString(array);
                strToUrls(stringUrls);
                return url;
            }
        }
    }
}
