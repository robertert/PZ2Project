namespace LoginApp.Models
{
    public class Players
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? Captain { get; set; }

        public int? Goals { get; set; }

        public string? Team { get; set; }
    }
}