using System.Text.Json;
using System.Text.Json.Nodes;
using RoadToSchmalz.Data;

namespace RoadToSchmalz.Api
{
    public abstract class AbstractPlayer
    {
        public static string GetHeadshotSource(string player_id)
        {
            return $"https://assets.leaguestat.com/pjhlon/240x240/{player_id}.jpg";
        }
    }
}