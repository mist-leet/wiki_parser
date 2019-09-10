using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wiki_parser
{
    class WikiData
    {
        public string[] title;

        public string[] img_url;

        private int[] r;

        private string[] url_list;

        private const int n = 4;

        /// <summary>
        /// Creating a WikiData instance
        /// </summary>
        /// <param name="urls"> array of links to chose </param>
        public WikiData(string[] urls)
        {
            title    = new string[] { };
            img_url  = new string[] { };
            r        =    new int[] { };
            url_list = new string[] { };

            r = MakeRandomList(urls.Length);

            url_list = urls;

            for (int i = 0; i < 4; i++)
            {
                Array.Resize<string>(ref title, title.Length + 1);
                Array.Resize<string>(ref img_url, img_url.Length + 1);

                title[i] = new ParserTitle().GetData(url_list[r[i]])[0];
                img_url[i] = new ParserImg().GetData(url_list[r[i]])[0];
            }
            int correct_elements = 0;
            while (correct_elements < 4)
            {
                if (title[correct_elements] == null || img_url[correct_elements] == null ||
                    title[correct_elements] == "" || img_url[correct_elements] == "")
                {
                    int q = GetRandomNumber(r, url_list.Length);
                    title[correct_elements]   = new ParserTitle().GetData(url_list[q])[0];
                    img_url[correct_elements] = new ParserImg().GetData(url_list[q])[0];
                }
                else
                    correct_elements++;
            }
        }

        /// <summary>
        /// Check is all elements different
        /// </summary>
        /// <param name="r"> array to check </param>
        /// <returns>true if different, false if not</returns>
        private bool CheckRandom(int[] r)
        {
            for (int i = 0; i < r.Length; i++)
                for (int j = 0; j < r.Length; j++)
                    if (i == j)
                        continue;
                    else
                        if (r[i] == r[j])
                            return false;
            return true;
        }

        /// <summary>
        /// Create an array of 4 random integer value from 0 to maxValue
        /// </summary>
        /// <param name="maxValue"> max value in array</param>
        /// <returns>array of random numbers</returns>
        private int[] MakeRandomList(int maxValue)
        {
            Random rnd = new Random();
            int[] r = new int[n];
            for (int i = 0; i < n; i++)
                r[i] = rnd.Next(maxValue); 

            while (!CheckRandom(r))
                for (int i = 0; i < n; i++)
                    r[i] = rnd.Next(maxValue);
            return r;
        }

        /// <summary>
        /// Get random number which is not in r
        /// </summary>
        /// <param name="r"></param>
        /// <param name="maxValue">max value in r</param>
        /// <returns>random number</returns>
        private int GetRandomNumber(int[] r, int maxValue)
        {
            while (true)
            {
                int i;
                int rn = new Random().Next(maxValue);
                for (i = 0; i < r.Length - 1; i++)
                {
                    if (r[i] == rn)
                        break;
                }
                if (r[i] != rn)
                    return rn;
            }
        }
    }
}
