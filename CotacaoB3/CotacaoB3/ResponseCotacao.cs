namespace CotacaoB3
{
    public class ResponseCotacao
    {

        public Result[] results { get; set; }
        public DateTime requestedAt { get; set; }
        public string took { get; set; }

        public class Result
        {
            public string currency { get; set; }
            public string shortName { get; set; }
            public string longName { get; set; }
            public float regularMarketChange { get; set; }
            public float regularMarketChangePercent { get; set; }
            public DateTime regularMarketTime { get; set; }
            public float regularMarketPrice { get; set; }
            public float regularMarketDayHigh { get; set; }
            public string regularMarketDayRange { get; set; }
            public float regularMarketDayLow { get; set; }
            public float regularMarketVolume { get; set; }
            public float regularMarketPreviousClose { get; set; }
            public float regularMarketOpen { get; set; }
            public string fiftyTwoWeekRange { get; set; }
            public float fiftyTwoWeekLow { get; set; }
            public float fiftyTwoWeekHigh { get; set; }
            public string symbol { get; set; }
            public string usedInterval { get; set; }
            public string usedRange { get; set; }
            public Historicaldataprice[] historicalDataPrice { get; set; }
            public string[] validRanges { get; set; }
            public string[] validIntervals { get; set; }
            public float priceEarnings { get; set; }
            public float earningsPerShare { get; set; }
            public string logourl { get; set; }
        }

        public class Historicaldataprice
        {
            public float date { get; set; }
            public float open { get; set; }
            public float high { get; set; }
            public float low { get; set; }
            public float close { get; set; }
            public float volume { get; set; }
            public float adjustedClose { get; set; }
        }
    }
}
