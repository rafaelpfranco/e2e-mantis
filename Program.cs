using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace E2EMantis
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }

        static void Main(string[] args)
        {
            string pathFixtures = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            // Configurar o carregamento do appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(pathFixtures + "/Fixtures/error_message.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Usar a URL base do appsettings.json
            string baseUrl = Configuration["Environment:BaseUrl"];


            // Inicializar o ChromeDriver
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(path + @"\drivers\", options);

            try
            {
                // Navegar para a URL base
                driver.Navigate().GoToUrl(baseUrl);
            }
            finally
            {
                // Fechar o navegador
                driver.Quit();
            }
        }
    }
}
