using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class BikeVM
    {

        public Bike Bike  { get; set; }

        public IEnumerable<Model> Models   { get; set; }
        public IEnumerable<Make>  Makes    { get; set; }
        public IEnumerable<Currency>  Currencies { get; set; }


        private List<Currency> cList = new List<Currency>();

        private List<Currency> CreateList()
        {
            cList.Add(new Currency("USD", "USD"));
            cList.Add(new Currency("INR", "INR"));
            cList.Add(new Currency("EUR", "EUR"));


            return cList;
        }


        public IFormFile File { get; set; }


        public BikeVM()
        {
            Currencies = CreateList();
        }

    }

    public class Currency
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Currency(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
