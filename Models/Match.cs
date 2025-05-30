namespace LoginApp.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Date { get; set; }

        public string? Team1 { get; set; }

        public string? Team2 { get; set; }
        public int? Score1 { get; set; }
        public int? Score2 { get; set; }

    }
}