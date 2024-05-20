namespace PlayASetlist.Library.Votes.Text
{
    public class Options
    {
        public string Album { get; set; } = "\"#VAL\" Album song";
        public string Artist { get; set; } = "Song by \"#VAL\"";
        public string Charter { get; set; } = "Charted by \"#VAL\"";
        public string Genre { get; set; } = "A \"#VAL\" style song";
        public string Modchart { get; set; } = "Stunning visual effects";
        public string Name { get; set; } = "\"#NAME\" by \"#ARTIST\"";
        public string Year { get; set; } = "Song from #VAL";
    }
}