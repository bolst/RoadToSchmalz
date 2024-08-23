namespace RoadToSchmalz.Data.GoalieStats
{
    public class Active
    {
        public string active { get; set; }
    }

    public class Name
    {
        public string playerLink { get; set; }
        public string seoName { get; set; }
    }

    public class Prop
    {
        public Name name { get; set; }
        public Active active { get; set; }
        public TeamName team_name { get; set; }
        public TeamCode team_code { get; set; }
    }

    public class GoalieStat
    {
        public Prop prop { get; set; }
        public Row row { get; set; }
    }

    public class Row
    {
        public string player_id { get; set; }
        public string name { get; set; }
        public string active { get; set; }
        public string jersey_number { get; set; }
        public string team_name { get; set; }
        public string team_code { get; set; }
        public string games_played { get; set; }
        public int minutes_played { get; set; }
        public string saves { get; set; }
        public string shots { get; set; }
        public string save_percentage { get; set; }
        public string goals_against { get; set; }
        public string shutouts { get; set; }
        public string wins { get; set; }
        public string losses { get; set; }
        public string ot_losses { get; set; }
        public string ties { get; set; }
        public string goals_against_average { get; set; }
        public int rank { get; set; }
        public List<string> flags { get; set; }
    }

    public class TeamCode
    {
        public string teamLink { get; set; }
    }

    public class TeamName
    {
        public string teamLink { get; set; }
    }




}