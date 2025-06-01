namespace LoginApp.Models
{
    public class Teams
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Captain { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public int? Points { get; set; }

        public ICollection<Players>? Players { get; set; }
        public ICollection<Coach>? Coaches { get; set; }
        public ICollection<Match>? HomeMatches { get; set; }
        public ICollection<Match>? AwayMatches { get; set; }
    }
}