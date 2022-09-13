using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class BookAuthorShipConfiguration : IEntityTypeConfiguration<BookAuthorShip>
    {
        public void Configure(EntityTypeBuilder<BookAuthorShip> builder)
        {
            builder.HasKey(x => new { x.BookId, x.AuthorId, x.AuthorshipRoleId });
            //builder.HasNoKey();
            builder.HasOne(x => x.book).WithMany(x => x.bookAuthorShips).HasForeignKey(x => x.BookId);
            builder.HasOne(x => x.author).WithMany(x => x.bookAuthorShips).HasForeignKey(x => x.AuthorId);
            builder.HasOne(x => x.authorshipRole).WithMany(x => x.bookAuthorShips).HasForeignKey(x => x.AuthorshipRoleId);
        }
    }
}
