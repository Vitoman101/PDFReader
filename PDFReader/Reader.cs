using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PDFReader
{
    class Reader
    {
        
        public string ReadPDF(string pdfLocation)
        {
            var pdfDocument = new PdfDocument(new PdfReader(pdfLocation));
            var strategy = new LocationTextExtractionStrategy();
            StringBuilder processed = new StringBuilder();
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
            {
                var page = pdfDocument.GetPage(i);
                string text = PdfTextExtractor.GetTextFromPage(page, strategy);
                processed.Append(text);
            }
            return processed.ToString();
        }

        //MRKELA OVDE
        public Dictionary<string,int> uniqueWords(string pdfLocation)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            HashSet<string> hash = new HashSet<string>();
            var pdfDocument = new PdfDocument(new PdfReader(pdfLocation));
            var strategy = new LocationTextExtractionStrategy();
            StringBuilder processed = new StringBuilder();
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
            {
                var page = pdfDocument.GetPage(i);
                string text = PdfTextExtractor.GetTextFromPage(page, strategy);

                //uklanja simbole nepotrebne i pretvara u lower case
                text = Regex.Replace(text, @"(\s+|@|&|'|\(|\)|<|>|#|,|\.|;|\?|[0-9]|\-|\?)", " ").ToLower();
                foreach (string n in text.Split(' '))
                 {
                    if(n.Length > 1)
                    {
                        if (!hash.Contains(n))
                        {
                            hash.Add(n);
                            dict.Add(n, 1);
                        }
                        else
                        {
                            dict[n]++;
                        }
                    }
                }

            }
            return dict;
        }
    }
}
