using System.Text.Json;
using System.Text.Json.Nodes;
using RoadToSchmalz.Data;
using RoadToSchmalz.Data.PlayerStats;


namespace RoadToSchmalz.Api
{
    using StatType = List<Data.PlayerStats.PlayerStat>;

    public class Players : AbstractPlayer
    {
        private static Players? instance = null;
        public Players() { }
        public static Players Instance()
        {
            if (instance == null) instance = new Players();
            return instance;
        }

        public StatType? GetStats()
        {
            using (var stream = new StreamReader("dat/player_data.json"))
            {
                string fileContent = stream.ReadToEnd();
                StatType? data = JsonSerializer.Deserialize<StatType>(fileContent);
                stream.Close();
                return data;
            }
        }

        public async Task FetchData()
        {
            string url = "https://lscluster.hockeytech.com/feed/index.php?feed=statviewfeed&view=players&season=67&team=all&position=skaters&rookies=0&statsType=standard&rosterstatus=undefined&site_id=2&first=0&limit=20&sort=points&league_id=1&lang=en&division=-1&key=54ad32ee30e379ad&client_code=pjhlon&league_id=1&callback=angular.callbacks._2";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Get data and format to JSON
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    content = content.Split('(', 2)[1].TrimEnd(')');
                    JsonDocument data = JsonDocument.Parse(content);

                    // Get player data
                    var playerData = data.RootElement.EnumerateArray()
                    .ElementAt(0)
                    .GetProperty("sections")
                    .EnumerateArray()
                    .ElementAt(0)
                    .GetProperty("data");
                    using (var playerDataFile = System.IO.File.CreateText("dat/player_data.json"))
                    {
                        await JsonSerializer.SerializeAsync(playerDataFile.BaseStream, playerData);
                        playerDataFile.Close();
                    }

                    Console.WriteLine("Got player data");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error in getting player data");
                Console.WriteLine(exc + "\n\n");
            }
        }
    }
}