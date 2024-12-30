using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FML.Familiares.API.Data.Mappings
{
    public class RelativeMapping : IEntityTypeConfiguration<Relative>
    {
        public void Configure(EntityTypeBuilder<Relative> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.FirstName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(r => r.LastName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(r => r.BirthDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(r => r.DeathDate)
                .HasColumnType("datetime");

            builder.Property(r => r.Gender)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(r => r.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(r => r.FamilyId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(r => r.HouseId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(r => r.FatherId)
                .HasColumnType("uniqueidentifier");

            builder.Property(r => r.MotherId)
                .HasColumnType("uniqueidentifier");

            builder.Property(r => r.Email)
                .HasColumnType("varchar(100)");

            builder.Property(r => r.Phone)
                .HasColumnType("varchar(20)");

            builder.Property(r => r.Patriarch)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(r => r.Matriarch)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(r => r.IsAlive)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(r => r.LinkName)
                .HasColumnType("varchar(100)");

            builder.Property(r => r.SpouseObj)
                .HasColumnType("uniqueidentifier");

            //fotoId and FotoUrl
            builder.Property(r => r.FotoId)
                .HasColumnType("uniqueidentifier");

            builder.Property(r => r.SecretSanta)
                .IsRequired()
                .HasColumnType("bit");

            builder.HasOne(r => r.House)
                .WithMany(h => h.Residents)
                .HasForeignKey(r => r.HouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Family)
                .WithMany(f => f.Relatives)
                .HasForeignKey(r => r.FamilyId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.ToTable("Relatives");
        }
    }
}
