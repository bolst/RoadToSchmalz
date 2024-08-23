using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RoadToSchmalz.Api;

namespace RoadToSchmalz.Pages
{
    public partial class Index
    {

        [Inject] private HttpClient httpClient {get; set;}
        private const int TOTAL_PREFINAL_ROUNDS = 5;

        private int RoundViewLevel { get; set; } = 0;
        private int CurrentRound { get; set; } = 0;

        private FigureSize PjhlLogoSize = FigureSize.Is128x128;

        private List<Data.Matchups>? north_matchups { get; set; } = null;
        private List<Data.Matchups>? east_matchups { get; set; } = null;
        private List<Data.Matchups>? west_matchups { get; set; } = null;
        private List<Data.Matchups>? south_matchups { get; set; } = null;

        private bool IsMobile { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await RoadToSchmalz.Api.Matchups.Instance().FetchData();
            try
            {
                IsMobile = await JSRuntime.InvokeAsync<bool>("isDevice");
            }
            catch (JSDisconnectedException exc)
            {
                Console.WriteLine("Warning: JS Disconnect");
                Console.WriteLine(exc.StackTrace);
                IsMobile = false;
            }

            await SetMatchups();
            SetRoundView();
            SetPjhlLogoSize();
        }

        private async Task SetMatchups()
        {
            await SetCurrentRound();

            var Inst = Matchups.Instance();

            var north_carruthers = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.CARRUTHERS, CurrentRound);
            var north_pollock = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.POLLOCK, CurrentRound);

            var east_orr = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.ORR, CurrentRound);
            var east_tod = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.TOD, CurrentRound);

            var west_yeck = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.YECK, CurrentRound);
            var west_stobbs = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.STOBBS, CurrentRound);

            var south_bloomfield = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.BLOOMFIELD, CurrentRound);
            var south_doherty = await Inst.GetMatchups(httpClient, Data.Divisions.DIVISION.DOHERTY, CurrentRound);

            if (north_carruthers != null && north_pollock != null)
            {
                north_matchups = north_carruthers;
                for (int round = 0; round < north_pollock.Count(); round++)
                {
                    var matchupToAdd = north_pollock[round].matchups.Where(x => int.Parse(x.round) < 4);
                    north_matchups[round].matchups.AddRange(matchupToAdd);
                }
            }

            if (east_orr != null && east_tod != null)
            {
                east_matchups = east_orr;
                for (int round = 0; round < east_tod.Count(); round++)
                {
                    var matchupToAdd = east_tod[round].matchups.Where(x => int.Parse(x.round) < 4);
                    east_matchups[round].matchups.AddRange(matchupToAdd);
                }
            }

            if (west_yeck != null && west_stobbs != null)
            {
                west_matchups = west_yeck;
                for (int round = 0; round < west_stobbs.Count(); round++)
                {
                    var matchupToAdd = west_stobbs[round].matchups.Where(x => int.Parse(x.round) < 4);
                    west_matchups[round].matchups.AddRange(matchupToAdd);
                }
            }

            if (south_bloomfield != null && south_doherty != null)
            {
                south_matchups = south_bloomfield;
                for (int round = 0; round < south_doherty.Count(); round++)
                {
                    var matchupToAdd = south_doherty[round].matchups.Where(x => int.Parse(x.round) < 4);
                    south_matchups[round].matchups.AddRange(matchupToAdd);
                }
            }
        }

        private void SetRoundView()
        {
            Uri uri = new Uri(navManager.Uri);
            string? rvStr = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("rv");
            int roundViewLevel = CurrentRound - 1;
            if (!string.IsNullOrEmpty(rvStr) && int.TryParse(rvStr, out roundViewLevel))
            {
                // must be between 0 and 5
                roundViewLevel = Math.Min(roundViewLevel, TOTAL_PREFINAL_ROUNDS);
                roundViewLevel = Math.Max(roundViewLevel, 0);
            }
            RoundViewLevel = roundViewLevel;
        }

        private async Task SetCurrentRound()
        {
            /*
            int n = north_matchups == null ? 1 : north_matchups.Count();
            int e = east_matchups == null ? 1 : east_matchups.Count();
            int s = south_matchups == null ? 1 : south_matchups.Count();
            int w = west_matchups == null ? 1 : west_matchups.Count();

            var rounds = new[] { n, e, s, w };
            CurrentRound = rounds.Min();
            */
            CurrentRound = await Api.Matchups.Instance().GetCurrentRound(httpClient);
        }

        private void SetPjhlLogoSize()
        {
            PjhlLogoSize = IsMobile ? FigureSize.Is96x96 : FigureSize.Is128x128;
        }
    }
}
