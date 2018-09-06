using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Code_Test_A
{
    class Program
    {
        static void Main(string[] args)
        {

            var json="";
            List<string> kota = new List<string>();
            string url = "https://raw.githubusercontent.com/lutangar/cities.json/master/cities.json";
                         
            Console.WriteLine("Input Kota ");
            string inputan = Console.ReadLine();

            //--Fetching Data--
            var webClient = new WebClient();
            webClient.Proxy = null;
            try
            {
                json = webClient.DownloadString(url);
            }catch(WebException e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                Environment.Exit(0);
            }

            var jsonArray = JArray.Parse(json);

            for (int i = 0; i < jsonArray.Count; i++)
            {

                if (jsonArray[i]["country"].ToString() == "ID")
                {
                    kota.Add(jsonArray[i]["name"].ToString());
                }
            }
            //--Fetching Data end--

            List<string> hasil = new List<string>();
            for (int i = 0; i < kota.Count; i++)
            {
                int len_inputan = inputan.Length;
                int len_text = kota[i].Length;
                //--Compute Levensthein  Distance--
                if (LevenstheinDis(inputan.ToLower(),kota[i].ToLower()) < 4)
                {
                    hasil.Add(kota[i]);
                }
            }
            Console.WriteLine(String.Join(", ", hasil.ToArray()));
            Console.ReadKey();
        }
        public static int LevenstheinDis(string s, string t)
        {
            int dis_cos;
            int len_s = s.Length;
            int len_t = t.Length;
            int[,] dis = new int[len_s + 1, len_t + 1];

            if (len_s == 0)
            {
                return len_t;
            }

            if (len_t == 0)
            {
                return len_s;
            }

            for (int i = 0; i <= len_s; dis[i, 0] = i++){
            }

            for (int j = 0; j <= len_t; dis[0, j] = j++){
            }

            for (int i = 1; i <= len_s; i++){
                for (int j = 1; j <= len_t; j++)
                {
                    if (t[j - 1] == s[i - 1])
                        dis_cos = 0;
                    else
                        dis_cos = 1;

                    dis[i, j] = Math.Min(Math.Min(dis[i - 1, j] + 1, dis[i, j - 1] + 1), dis[i - 1, j - 1] + dis_cos);
                }
            }
            return dis[len_s, len_t];
        }
    }
}
