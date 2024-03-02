namespace RoadToSchmalz.Data
{
    public class Game
    {
        public string game_id { get; set; }
        public string home_team { get; set; }
        public string home_goal_count { get; set; }
        public string visiting_team { get; set; }
        public string visiting_goal_count { get; set; }
        public string status { get; set; }
        public string game_status { get; set; }
        public string date_time { get; set; }
        public string if_necessary { get; set; }
        public string game_notes { get; set; }
    }

    public class Matchup
    {
        public string series_letter { get; set; }
        public string series_name { get; set; }
        public string series_logo { get; set; }
        public string round { get; set; }
        public string active { get; set; }
        public string feeder_series1 { get; set; }
        public string feeder_series2 { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string content_en { get; set; }
        public string content_fr { get; set; }
        public string winner { get; set; }
        public List<Game> games { get; set; }
        public int team1_wins { get; set; }
        public int team2_wins { get; set; }
        public int ties { get; set; }
    }

    public class Matchups
    {
        public string round { get; set; }
        public string round_name { get; set; }
        public string season_id { get; set; }
        public string round_type_id { get; set; }
        public string round_type_name { get; set; }
        public List<Matchup> matchups { get; set; }
    }


}