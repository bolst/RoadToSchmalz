using System.Net.Http.Json;
using System.Text.Json;


namespace RoadToSchmalz.Api
{
    public class Teams
    {
        private static Teams? instance = null;
        private Dictionary<string, string>? teamIdNameMap = null;

        // GetTeamIdNameMap can go in the constructor since the team ids shouldn't ever change per season
        private Teams()
        {
        }
        public static Teams Instance()
        {
            if (instance == null) instance = new Teams();
            return instance;
        }

        public async Task<Dictionary<string, string>> TeamIdNameMap(HttpClient client)
        {
            if (teamIdNameMap is null)
            {
                teamIdNameMap = await GetTeamIdNameMap(client);
            }
            return teamIdNameMap;
        }

        private async Task<Dictionary<string, string>> GetTeamIdNameMap(HttpClient client)
        {
            try
            {
                var response = await client.GetAsync("dat/team_data.json");
                response.EnsureSuccessStatusCode();
                List<Data.Teams>? data = await response.Content.ReadFromJsonAsync<List<Data.Teams>>();

                if (data == null) return new Dictionary<string, string>();

                Dictionary<string, string> retval = new Dictionary<string, string>();
                foreach (Data.Teams teamdata in data)
                {
                    if (teamdata.id != null && teamdata.name != null)
                    {
                        string team_id = teamdata.id;
                        string team_name = teamdata.name;
                        retval[team_id] = team_name;
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't get team id name map");
                return new();
            }
        }
    }
}