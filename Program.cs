using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace E2EMantis
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }

        public static void Main(string[] args)
        {
            ConfigureSettings(); // Chama o método estático de configuração

            var options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(@"c:\path\to\chromedriver\", options);

            try
            {
                string baseUrl = Configuration["Environment:BaseUrl"];
                driver.Navigate().GoToUrl(baseUrl);
            }
            finally
            {
                // Fechar o navegador ao finalizar
                driver.Quit();
                driver.Dispose();
            }
        }

        public static void ConfigureSettings()
        {
            string pathFixtures = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(pathFixtures, "Fixtures", "return_message.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(pathFixtures, "Fixtures", "users.json"), optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }
    }
}
