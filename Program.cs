using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace IpGeolocate
{
    public class Data
    {
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string postal { get; set; }
        public string org { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Ip Geolocator";

            while (true)
            {
                Console.Write("Enter IP Address: ");
                string ip = Console.ReadLine();
                string url = $"https://ipinfo.io/{ip}/json";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        Console.WriteLine("Request Sucessfully Made");

                        string responseData = await response.Content.ReadAsStringAsync();
                        Data ipInfo = JsonConvert.DeserializeObject<Data>(responseData);

                        Console.Clear();
                        Console.WriteLine($"Country: {ipInfo.country}");
                        Console.WriteLine($"City: {ipInfo.city}");
                        Console.WriteLine($"Coordinates: {ipInfo.loc}");
                        Console.WriteLine($"Region: {ipInfo.region}");
                        Console.WriteLine($"ISP: {ipInfo.org}");

                        string[] Coords = ipInfo.loc.Split(',');
                        Console.WriteLine($"Google Maps: https://www.google.com/maps/?q={Coords[0]},{Coords[1]}");

                        Console.Write("Do you want to search for a new IP? (y/n): ");
                        string choice = Console.ReadLine().ToLower();
                        if (choice != "y")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
