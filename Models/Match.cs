namespace LoginApp.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }

        public int Team1Id { get; set; }
        public Teams? Team1 { get; set; }

        public int Team2Id { get; set; }
        public Teams? Team2 { get; set; }

        public int? Score1 { get; set; }
        public int? Score2 { get; set; }
    }
}