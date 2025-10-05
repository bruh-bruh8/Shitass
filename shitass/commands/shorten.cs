using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace shitass
{
    class ShortenCommand : ICommand
    {
        public string Name => "shorten";
        public string[] Aliases => new string[] { "short" };
        public string Description => "Shortens a link using is.gd or v.gd";
        public string Usage => "shorten [url] (-alt)";

        public async Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Usage: shorten <url>");
                return;
            }
            if (args.Length >= 3)
            {
                if (args[2] == "-alt")
                {
                    try
                    {
                        await Shorten(args[1], true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {

                try
                {
                    await Shorten(args[1], false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return;
        }
        public static async Task<string> Shorten(string url, bool alt)
        {
            string baseUrl = alt ? "https://v.gd/create.php" : "https://is.gd/create.php";
            string uri = $"{baseUrl}?format=simple&url={Uri.EscapeDataString(url)}&logstats=1";

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string response = await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error: {responseMessage.StatusCode} - {response}");

                Console.WriteLine($"\n{response}\n");
                return response;
            }
        }
    }
}
