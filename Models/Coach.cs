namespace LoginApp.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        // FK
        public int TeamId { get; set; }
        public Teams? Team { get; set; }
    }
}