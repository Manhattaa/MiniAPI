namespace MiniAPI.Models
{
    public class Interest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Link2Interests> Link2Interests { get; set; }
    }
}
