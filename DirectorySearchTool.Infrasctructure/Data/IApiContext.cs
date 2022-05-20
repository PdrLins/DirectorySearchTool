using DirectorySearchTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DirectorySearchTool.Infrasctructure.Data
{
    public interface IApiContext
    {
        DbSet<Member> Members { get; set; }
        //DbSet<MemberHeading> MemberHeadings { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        DbSet<ShortLink> ShortLinks { get; set; }
        Task<int> SaveChangesAsync();
    }
}