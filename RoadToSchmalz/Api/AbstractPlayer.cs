namespace RoadToSchmalz.Api
{
    using PlayerStatType = Data.PlayerStats.PlayerStat;
    using GoalieStatType = Data.GoalieStats.GoalieStat;

    public abstract class AbstractPlayer
    {

        private const int MIN_GAMES = 4;

        public static string GetHeadshotSource(string player_id)
        {
            return $"https://assets.leaguestat.com/pjhlon/240x240/{player_id}.jpg";
        }

        public static List<PlayerStatType> FilterRemaining(List<PlayerStatType>? data)
        {
            List<PlayerStatType> retval = new List<PlayerStatType>();
            if (data == null) return retval;

            foreach (PlayerStatType playerStat in data)
            {
                #region filter out players with less games than MIN_GAMES
                string? strGamesPlayed = playerStat.row?.games_played;
                if (strGamesPlayed != null)
                {
                    int gamesPlayed = int.Parse(strGamesPlayed);
                    if (gamesPlayed > MIN_GAMES) retval.Add(playerStat);
                }
                #endregion

            }

            return retval;
        }

        public static List<PlayerStatType> FilterFinalsPlayers(List<PlayerStatType>? data, string teamCode)
        {
            List<PlayerStatType> retval = new List<PlayerStatType>();
            if (data == null) return retval;

            foreach (PlayerStatType playerStat in data)
            {
                #region filter out players that don't match team code
                string? strTeamCode = playerStat.row?.team_code;
                if (strTeamCode != null)
                {
                    if (strTeamCode.Equals(teamCode)) retval.Add(playerStat);
                }
                #endregion
            }

            return retval;
        }

        public static List<GoalieStatType> FilterRemaining(List<GoalieStatType>? data)
        {
            List<GoalieStatType> retval = new List<GoalieStatType>();
            if (data == null) return retval;

            foreach (GoalieStatType goalieStat in data)
            {
                #region filter out players with less games than MIN_GAMES
                string? strGamesPlayed = goalieStat.row?.games_played;
                if (strGamesPlayed != null)
                {
                    int gamesPlayed = int.Parse(strGamesPlayed);
                    if (gamesPlayed > MIN_GAMES) retval.Add(goalieStat);
                }
                #endregion

            }

            return retval;
        }

        public static List<GoalieStatType> FilterFinalsGoalies(List<GoalieStatType>? data, string teamCode)
        {
            List<GoalieStatType> retval = new List<GoalieStatType>();
            if (data == null) return retval;

            foreach (GoalieStatType goalieStat in data)
            {
                #region filter out players that don't match team code
                string? strTeamCode = goalieStat.row?.team_code;
                if (strTeamCode != null)
                {
                    if (strTeamCode.Equals(teamCode)) retval.Add(goalieStat);
                }
                #endregion
            }

            return retval;
        }
    }
}