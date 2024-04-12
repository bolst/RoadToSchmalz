using System.Text.Json;
using System.Text.Json.Nodes;
using RoadToSchmalz.Data;

namespace RoadToSchmalz.Api
{
    using StatType = List<Data.GoalieStats.GoalieStat>;

    public class Goalies : AbstractPlayer
    {

        private static Goalies? instance = null;
        public Goalies() { }
        public static Goalies Instance()
        {
            if (instance == null) instance = new Goalies();
            return instance;
        }

        public StatType? GetStats()
        {
            string fileContent = File.ReadAllText("dat/goalie_data.json");
            StatType data = JsonSerializer.Deserialize<StatType>(fileContent);
            return data;
        }

        public async Task FetchData()
        {
            string url = "https://lscluster.hockeytech.com/feed/index.php?feed=statviewfeed&view=players&season=67&team=all&position=goalies&rookies=0&statsType=standard&rosterstatus=undefined&site_id=2&first=0&limit=20&sort=gaa&league_id=1&lang=en&division=-1&qualified=qualified&key=54ad32ee30e379ad&client_code=pjhlon&league_id=1&callback=angular.callbacks._2";

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
                    await using (var goalieDataFile = System.IO.File.CreateText("dat/goalie_data.json"))
                    {
                        await JsonSerializer.SerializeAsync(goalieDataFile.BaseStream, playerData);
                    }

                    Console.WriteLine("Got goalie data");
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