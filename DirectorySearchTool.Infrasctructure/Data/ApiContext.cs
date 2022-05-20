using DirectorySearchTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectorySearchTool.Infrasctructure.Data
{
    public class ApiContext : DbContext, IApiContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberHeading> MemberHeadings { get; set; }
        public DbSet<ShortLink> ShortLinks { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                        .HasMany(x => x.Headings)
                        .WithOne();

            modelBuilder.Entity<Member>()
                        .HasOne(o => o.ShortLink)
                        .WithOne(o => o.Member)
                        .HasForeignKey<ShortLink>(s => s.MemberRef);

            modelBuilder.Entity<Friendship>()
                        .HasKey(k => new { k.FriendshipFromId, k.FriendshipToId });

            modelBuilder.Entity<Friendship>()
                        .HasOne(f => f.FriendshipFrom)
                        .WithMany(wm => wm.FriendshipFromMe)
                        .HasForeignKey(f => f.FriendshipFromId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                        .HasOne(f => f.FriendshipTo)
                        .WithMany(wm => wm.FriendshipFromFriend)
                        .HasForeignKey(f => f.FriendshipToId);

            modelBuilder.Entity<MemberHeading>()
                        .HasOne(x => x.Member)
                        .WithMany(o => o.Headings);

            modelBuilder.Entity<ShortLink>()
                        .HasOne(o => o.Member)
                        .WithOne(o => o.ShortLink);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
