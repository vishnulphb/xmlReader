using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace xmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,string> items = IndexRules();

            

            foreach(KeyValuePair<string,string> pair in items)
            {
                Console.WriteLine(pair.Key + ":" + pair.Value);
            }
           
        }

        private static Dictionary<string,string> IndexRules()
        {
            Dictionary<string, string> index = new Dictionary<string, string>();

            XDocument xDoc = XDocument.Load("rules.xml");
            string indexName = null;
            string indexValue = null;
            var feedDetails = from feed in xDoc.Descendants("Index")
                              select new
                              {
                                  IndexInfo = feed.Descendants("IndexInfo").Descendants(),
                                  IndexRules = feed.Descendants("IndexRules").Descendants()
                              };

            foreach (var feed in feedDetails)
            {
                foreach (XElement xe in feed.IndexInfo)
                {
                    //Console.WriteLine(xe.Name + "=" + xe.Value);
                    indexName = xe.Value;
                    
                    foreach (XElement xe1 in feed.IndexRules)
                    {
                        //Console.WriteLine(xe1.Attribute("name").Name + "=" + xe1.Attribute("name").Value);

                        indexValue = indexValue + xe1.Attribute("Attribute").Value + "|" + xe1.Attribute("NoOfChars").Value + ",";

                    }
                    

                    indexValue = indexValue.Remove(indexValue.Length - 1);
                    index.Add(indexName, indexValue);
                    //Console.WriteLine(indexName + " = " + indexValue);
                    indexValue = null;
                }


            }
            return index;
        }
    }
}
