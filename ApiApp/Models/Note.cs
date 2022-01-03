namespace ApiApp.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public User Owner { get; set; }
    }
}
