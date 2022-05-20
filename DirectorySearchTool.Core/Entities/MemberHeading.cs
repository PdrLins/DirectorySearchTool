namespace DirectorySearchTool.Core.Entities
{
    public class MemberHeading : Entity
    {
        public string Heading { get; set; }
        public Member Member { get; set; }
        public int MemberRef { get; set; }
    }
}
