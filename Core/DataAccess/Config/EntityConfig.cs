using Blazor.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazor.Core.DataAccess.Config;

public class LivroConfig : IEntityTypeConfiguration<LivroEntity>
{
    private const string titulo = nameof(LivroEntity.Titulo);
    private const string idAutor = "IdAutor";
    private const string idSerie = "IdSerie";
    private const string ordem = nameof(LivroEntity.Ordem);
    private const string ixLivroUnico = "IX_Livros_Unique";

    public void Configure(EntityTypeBuilder<LivroEntity> builder)
    {
        builder.ToTable("Livros");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Titulo).IsRequired().HasMaxLength(50);

        builder
            .HasOne(e => e.Autor)
                .WithOne()
                .HasForeignKey<LivroEntity>(idAutor)
                .IsRequired();

        builder
            .HasOne(e => e.Serie)
                .WithOne()
                .HasForeignKey<LivroEntity>(idSerie)
                .IsRequired(false);

        builder.HasIndex([titulo, idAutor, idSerie, ordem], ixLivroUnico).IsUnique();
    }
}

public class AutorConfig : IEntityTypeConfiguration<AutorEntity>
{
    public void Configure(EntityTypeBuilder<AutorEntity> builder)
    {
        builder.ToTable("Autores");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Nome).IsRequired().HasMaxLength(50);
        builder.HasIndex(e => e.Nome).IsUnique();
    }
}

public class SerieConfig : IEntityTypeConfiguration<SerieEntity>
{
    public void Configure(EntityTypeBuilder<SerieEntity> builder)
    {
        builder.ToTable("Series");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Nome).IsRequired().HasMaxLength(50);
        builder.HasIndex(e => e.Nome).IsUnique();
    }
}
