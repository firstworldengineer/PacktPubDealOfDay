using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace PacktDealOfDay
{
    class Program
    {
        static void Main(string[] args)
        {
            //obtain html via url
            var url = "https://www.packtpub.com/packt/offers/free-learning?from=block";
            var hw = new HtmlWeb();
            var html = hw.Load(url);

            //name of html class that contains the name of the book - found by inspecting element in website
            string htmlClassWeAreSearchingFor = "product-title";
            string dotdTitle = string.Empty;

            //extract name with HTMLAgilityPack's parser
            foreach (HtmlNode node in html.DocumentNode.SelectNodes("//div"))
            {
                //search through all classes in this div
                var attribute = node.GetAttributeValue("class", null);

                //see if this class is our desired one. Not all divs necessarily contain classes, so account for nulls
                if (attribute != null && attribute == htmlClassWeAreSearchingFor)
                {
                    //this value will be centered and formatted, remove all of that stuff
                    dotdTitle = Regex.Replace(node.InnerText, @"[\t\r\n]", "");
                }
            }

            //display contents from parser to console
            if (!String.IsNullOrEmpty(dotdTitle))
            {
                Console.Write($@"Today's free eBook from Packtpub.com is {dotdTitle}");
            }
            else
            {
                Console.WriteLine("Something went terribly awry and we weren't able to find today's deal of the day from packtpub.com!");
            }
            Console.ReadKey();
        }
    }
}
