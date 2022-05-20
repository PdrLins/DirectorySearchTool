namespace DirectorySearchTool.Core.Entities
{
    public class ShortLink : Entity
    {
        public string Url { get; set; }
        public int MemberRef { get; set; }
        public Member Member { get; set; }
    }
}
