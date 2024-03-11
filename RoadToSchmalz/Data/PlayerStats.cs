namespace RoadToSchmalz.Data.PlayerStats
{
    public class Active
    {
        public string? active { get; set; }
    }

    public class Name
    {
        public string? playerLink { get; set; }
        public string? seoName { get; set; }
    }

    public class Prop
    {
        public Name? name { get; set; }
        public Active? active { get; set; }
        public TeamCode? team_code { get; set; }
    }

    public class PlayerStat
    {
        public Prop? prop { get; set; }
        public Row? row { get; set; }
    }

    public class Row
    {
        public string? player_id { get; set; }
        public string? name { get; set; }
        public string? active { get; set; }
        public string? position { get; set; }
        public string? jersey_number { get; set; }
        public string? team_code { get; set; }
        public string? games_played { get; set; }
        public string? game_winning_goals { get; set; }
        public string? goals { get; set; }
        public string? shots { get; set; }
        public string? assists { get; set; }
        public string? points { get; set; }
        public string? points_per_game { get; set; }
        public string? penalty_minutes { get; set; }
        public string? penalty_minutes_per_game { get; set; }
        public string? power_play_goals { get; set; }
        public string? power_play_assists { get; set; }
        public string? short_handed_goals { get; set; }
        public string? short_handed_assists { get; set; }
        public int? rank { get; set; }
        public List<string>? flags { get; set; }
    }

    public class TeamCode
    {
        public string? teamLink { get; set; }
    }




}