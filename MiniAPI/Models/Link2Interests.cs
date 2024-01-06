namespace MiniAPI.Models
{
    public class Link2Interests
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public virtual Person Person { get; set; }
        public virtual Interest Interests { get; set; }
    }
}
