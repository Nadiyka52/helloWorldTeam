﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1_Currency
{
    /// <summary>
    /// Additional class for storing currency
    /// </summary>
    public class CurrencyStorage : IReadable
    {
        private List<Currency> _storage;

        public CurrencyStorage()
        {
            _storage = new List<Currency>();
        }

        /// <summary>
        /// method for reading data from file
        /// </summary>
        /// <param name="pathfileName"></param>
        public void Read(string pathfileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("doc.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] vars = line.Split(' ');
                Currency obj = new Currency(vars[1], Convert.ToInt32(vars[0]));
                _storage.Add(obj);
            }

            file.Close();
        }

        static public void WriteInFile(List<float> l, string currencyName, string path)
        {
            try
            {
                /// <summary>
                /// method for writing data in file
                /// </summary>
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    foreach (float element in l)
                    {
                        sw.WriteLine(element.ToString() + " " + currencyName);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SelectUAH()
        {
            IEnumerable<Currency> selectedCurr = _storage.Where(x => x.CurrencyName == "UAH");
            if (selectedCurr.Count() == 0) { Console.WriteLine("No matches found"); }
            else
            {
                Console.WriteLine("SELECTED UAH: ");
                foreach (Currency x in selectedCurr)
                {
                    Console.WriteLine(x);

                }
            }
        }

        public Dictionary<string, float> TotalNumAndCurrName(string pathfileName)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            var selected = from s in _storage
                           group s by s.CurrencyName into grp
                           select new
                           {
                               name = grp.Key,
                               total_sum = grp.Sum(x => x.Amount)
                           };
            foreach (var v in selected)
            {
                Console.WriteLine(v);
                dict.Add(v.name, v.total_sum);
            }

            System.IO.File.WriteAllText(@pathfileName, string.Empty);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(pathfileName), true))
            {
                foreach (var v in dict)
                {
                    outputFile.WriteLine(v.Key + " ----------------- " + v.Value);
                }
                outputFile.Close();
            }
            return dict;
        }


        public void output()
        {
            foreach (Currency c in _storage)
            {
                Console.WriteLine(c);
            }
        }

       
    }
}
        
    

