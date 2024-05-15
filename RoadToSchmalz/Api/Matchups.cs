using System.Text.Json;
using System.Text.Json.Nodes;
using RoadToSchmalz.Data;

namespace RoadToSchmalz.Api
{
    public class Matchups
    {
        private static Matchups? instance = null;
        private Matchups() { }
        public static Matchups Instance()
        {
            if (instance == null) instance = new Matchups();
            return instance;
        }

        public int GetCurrentRound()
        {
            string fileContent = File.ReadAllText("wwwroot/dat/matchups.json");
            List<Data.Matchups>? data = JsonSerializer.Deserialize<List<Data.Matchups>>(fileContent);

            string strRound = data.MaxBy(x => int.Parse(x.round)).round;
            return int.Parse(strRound);
        }

        public List<Data.Matchups>? GetMatchups(Divisions.DIVISION division = Divisions.DIVISION.NULL, int currentRound = 0)
        {
            string fileContent = File.ReadAllText("wwwroot/dat/matchups.json");
            List<Data.Matchups>? data = JsonSerializer.Deserialize<List<Data.Matchups>>(fileContent);
            data = FilterByDivision(data, division, currentRound);
            return data;
        }

        public List<Data.Matchups>? FilterByDivision(List<Data.Matchups>? data, Divisions.DIVISION division, int currentRound)
        {
            if (data == null || division == Divisions.DIVISION.NULL) return data;

            List<Data.Matchups> retval = data;
            List<Data.Matchup> filtered_matchups;

            // each round
            for (int i = 0; i < retval.Count(); i++)
            {
                filtered_matchups = new List<Matchup>();
                Data.Matchups round = retval[i];
                List<Data.Matchup> matchups = round.matchups;

                // each matchup
                foreach (Data.Matchup matchup in matchups)
                {
                    // cause pjhl can't spell their own divisions correctly
                    string strDiv = Divisions.ToString(division).ToLower();
                    //if (matchup.series_name.ToLower().Contains(strDiv))
                    if (FilterRule.DivisionMatches(matchup, division, currentRound))
                    {
                        filtered_matchups.Add(matchup);
                    }
                }

                retval[i].matchups = filtered_matchups;
            }

            return retval;
        }

        public Data.Matchup? GetMatchupBySeriesLetter(string seriesLetter)
        {
            List<Data.Matchups>? matchups = GetMatchups();
            if (matchups == null) return null;

            // Each round
            foreach (Data.Matchups round in matchups)
            {
                // Each matchup
                foreach (Data.Matchup matchup in round.matchups)
                {
                    if (matchup.series_letter == seriesLetter)
                    {
                        return matchup;
                    }
                }

            }

            return null;
        }

        public async Task FetchData()
        {
            string url = "https://lscluster.hockeytech.com/feed/index.php?feed=modulekit&view=brackets&fmt=json&season_id=67&key=54ad32ee30e379ad&client_code=pjhlon&site_id=2&lang=en&league_id=&callback=angular.callbacks._1";

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

                    // Get team data
                    var teamData = data.RootElement.GetProperty("SiteKit").GetProperty("Brackets").GetProperty("teams");
                    var teamList = new JsonArray();
                    foreach (var team in teamData.EnumerateObject())
                    {
                        teamList.Add(team.Value);
                    }
                    await using (var teamFile = System.IO.File.CreateText("wwwroot/dat/team_data.json"))
                    {
                        await JsonSerializer.SerializeAsync(teamFile.BaseStream, teamList);
                    }

                    // Get matchups
                    var matchupsData = data.RootElement.GetProperty("SiteKit").GetProperty("Brackets").GetProperty("rounds");
                    await using (var matchupsFile = System.IO.File.CreateText("wwwroot/dat/matchups.json"))
                    {
                        await JsonSerializer.SerializeAsync(matchupsFile.BaseStream, matchupsData);
                    }

                    Console.WriteLine("Got matchup data");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error in fetching matchup data");
                Console.WriteLine(exc + "\n\n");
            }
        }


    }
}