namespace testAPI.Extention.Utils
{
    public class Utils
    {
        public static string GetConfig(string code)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                                                                         .Build();

            var value = configuration[code];
            return value;
        }
    }
}
