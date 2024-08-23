namespace RoadToSchmalz.Data
{
    public static class FilterRule
    {
        public static bool DivisionMatches(Data.Matchup matchup, Divisions.DIVISION div, int round)
        {
            string strDiv = Divisions.ToString(div).ToLower();
            string strSeriesName = matchup.series_name.ToLower();

            bool retval = strSeriesName.Contains(strDiv);

            if (retval) { return retval; }
            else if (matchup.round.Equals("4") || matchup.round.Equals("5") /*|| matchup.round.Equals("6")*/)
            {
                switch (div)
                {
                    #region north
                    case Divisions.DIVISION.POLLOCK:
                    case Divisions.DIVISION.CARRUTHERS:
                        retval = strSeriesName.Contains("north");
                        break;
                    #endregion
                    #region east
                    case Divisions.DIVISION.TOD:
                    case Divisions.DIVISION.ORR:
                        retval = strSeriesName.Contains("east");
                        break;
                    #endregion
                    #region south
                    case Divisions.DIVISION.DOHERTY:
                    case Divisions.DIVISION.BLOOMFIELD:
                        retval = strSeriesName.Contains("south");
                        break;
                    #endregion
                    #region west
                    case Divisions.DIVISION.YECK:
                    case Divisions.DIVISION.STOBBS:
                        retval = strSeriesName.Contains("west");
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            else if (matchup.round.Equals("6")) // finals
            {
                switch (div)
                {
                    case Divisions.DIVISION.ORR:
                    case Divisions.DIVISION.STOBBS:
                    case Divisions.DIVISION.YECK:
                    case Divisions.DIVISION.TOD:
                        return strSeriesName.Contains("pjhl final");
                }
            }


            return retval;
        }
    }
}
