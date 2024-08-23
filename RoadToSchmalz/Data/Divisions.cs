namespace RoadToSchmalz.Data
{
    public class Divisions
    {
        public enum DIVISION
        {
            CARRUTHERS,
            POLLOCK,
            ORR,
            TOD,
            STOBBS,
            YECK,
            BLOOMFIELD,
            DOHERTY,
            NULL
        }

        // cause pjhl can't spell their own divisions correctly
        public static string ToString(DIVISION d)
        {
            switch (d)
            {
                case DIVISION.CARRUTHERS:
                    return "carr";
                case DIVISION.BLOOMFIELD:
                    return "bloom";
                default:
                    return d.ToString();
            }
        }

        public enum CONFERENCE
        {
            NORTH,
            SOUTH,
            EAST,
            WEST,
            NULL
        }

        public List<DIVISION> ConferenceDivisions(CONFERENCE c)
        {
            switch (c)
            {
                case CONFERENCE.NORTH:
                    return new List<DIVISION> { DIVISION.CARRUTHERS, DIVISION.POLLOCK };
                case CONFERENCE.EAST:
                    return new List<DIVISION> { DIVISION.ORR, DIVISION.TOD };
                case CONFERENCE.WEST:
                    return new List<DIVISION> { DIVISION.STOBBS, DIVISION.YECK };
                case CONFERENCE.SOUTH:
                    return new List<DIVISION> { DIVISION.BLOOMFIELD, DIVISION.DOHERTY };
                case CONFERENCE.NULL:
                default:
                    return new List<DIVISION>();
            }
        }

    }
}