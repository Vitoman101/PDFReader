using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PDFReader
{
    class Program
    {
        static void Main(string[] args)
        {

            Reader test = new Reader();
            string lokacijaStari = @"C:\Users\Viktor\Desktop\Viktor\pdfovi\stara godina\CS101-L01.pdf";
            string lokacijaNovi = @"C:\Users\Viktor\Desktop\Viktor\pdfovi\ova godina\CS101-L01.pdf";
            //string stariPDF = test.ReadPDF(lokacijaStari);
            //string noviPDF = test.ReadPDF(lokacijaNovi);

            //var stariParts = stariPDF.SplitInParts(10737);
            //var noviParts = noviPDF.SplitInParts(10737);
            //List<double> calculator = new List<double>();

            //for (int i = 0; i < stariParts.Count(); i++)
            //{
            //    Console.WriteLine("Currently at " + i + "/" + stariParts.Count());
            //    string list1 = stariParts.ToList()[i];
            //    string list2 = noviParts.ToList()[i];
            //    int leven = Fastenshtein.Levenshtein.Distance(list1, list2);
            //    double ratio = ((double)leven) / (Math.Max(list1.Length, list2.Length)) * 100;
            //    calculator.Add(ratio);
            //    Console.Clear();
            //}

            //Console.WriteLine(Math.Round(calculator.Average(), 2));

            //foreach (var a in test.uniqueWords(lokacijaNovi))
            //{
            //    Console.WriteLine(a.Key + " " + a.Value);
            //}


            //MRKELA OVDE
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
  
            var nova = test.uniqueWords(lokacijaNovi);
            var stara = test.uniqueWords(lokacijaStari);

            nova = removeTopN(nova,10);
            stara = removeTopN(stara, 10);


            var distance = LevenshteinDistance.distanceMeasure(nova, stara);
            Console.WriteLine((1-distance)*100);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time: " + elapsedMs / 1000.0);
            Console.ReadLine();
        }

        public  static Dictionary<String,int> removeTopN(Dictionary<string,int> dict, int n)
        {
            var ordered = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            List<string> toRemove = new List<string>();
            int i = 0;
            foreach (var kvp in ordered)
            {
                toRemove.Add(kvp.Key);
                if (i == n)
                {
                    break;
                }
                i++;
            }
            foreach (var s in toRemove)
            {
                ordered.Remove(s);
            }

            return ordered;
        }
    }
}
