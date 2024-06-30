using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace E2EMantis.Utils
{
    internal class GenerateData
    {
        public static string GenerateIssueName()
        {
            var faker = new Faker();
            return faker.Commerce.ProductName();
        }

        public static string GenerateRandomNumber()
        {
            Random random = new Random();
            string randomString = random.Next(1, 1001).ToString();
            return randomString; 
        }
    }
}
