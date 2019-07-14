using System.Xml.Linq;
using System.Linq;
using System;
using System.Json;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace TrabalhoFinal
{
    public class Manager
    {
        protected XDocument xmlFile;

        public double total = 0;
        public List<Product> produtos = new List<Product>(); 
        public Manager(string filePath) 
        {
            this.xmlFile = XDocument.Load(filePath);
        }

        public void search(string searchString) 
        {
            searchString = searchString.ToLower();
            this.searchItems(searchString);
            this.calculatePrice(searchString);
            this.generateJsonFile();
        }

        protected double calculatePrice(string searchString)
        {
            return (from t in this.xmlFile.Element("Products").Elements("Product")
                        where t.Element("Product_name").Value.ToLower().Contains(searchString)
                        select t).Sum(c => (double) c.Element("Product_price"));  
        }

        protected void searchItems(string searchString)
        {
            var items = from t in this.xmlFile.Element("Products").Elements("Product")
                where t.Element("Product_name").Value.ToLower().Contains(searchString)
                select t;

            foreach (var item in items) {
                Product prod = new Product();
                prod.Product_id = Convert.ToInt32(item.Element("Product_id").Value);
                prod.Product_name = item.Element("Product_name").Value;
                prod.Product_price = Convert.ToDouble(item.Element("Product_price").Value);
                this.produtos.Add(prod);
            }
        }

        protected void generateJsonFile() 
        {

            string json = JsonConvert.SerializeObject(this);

            File.WriteAllText("pesquisa.json", json);

        }
    }
}