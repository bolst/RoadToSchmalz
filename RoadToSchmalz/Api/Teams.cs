using System.Text.Json;

namespace RoadToSchmalz.Api
{
    public class Teams
    {
        private static Teams? instance = null;
        public Dictionary<string, string> TeamIdNameMap { get; } = new Dictionary<string, string>();

        // GetTeamIdNameMap can go in the constructor since the team ids shouldn't ever change per season
        private Teams()
        {
            TeamIdNameMap = GetTeamIdNameMap();
        }
        public static Teams Instance()
        {
            if (instance == null) instance = new Teams();
            return instance;
        }

        private Dictionary<string, string> GetTeamIdNameMap()
        {
            string fileContent = File.ReadAllText("dat/team_data.json");
            List<Data.Teams>? data = JsonSerializer.Deserialize<List<Data.Teams>>(fileContent);

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
    }
}