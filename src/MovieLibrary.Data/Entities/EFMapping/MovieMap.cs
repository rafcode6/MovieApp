using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieLibrary.Data.Entities.EFMapping;

internal class MovieMap : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies", "MovieLibrary");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(150)
            .UseCollation("NOCASE");  
        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.HasMany(e => e.MovieCategories)
            .WithOne(e => e.Movie)
            .HasForeignKey(e => e.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.ImdbRating)
           .HasConversion<double>();

        builder.HasIndex(u => u.Title)
            .IsUnique();
    }
}
