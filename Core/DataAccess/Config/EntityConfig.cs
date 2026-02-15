using System;
using Blazor.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazor.Core.DataAccess.Config;

public class LivroConfig : IEntityTypeConfiguration<LivroEntity>
{
    public void Configure(EntityTypeBuilder<LivroEntity> builder)
    {
        builder.ToTable("Livros");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Titulo).IsRequired().HasMaxLength(50);

        builder
            .HasOne(e => e.Autor)
            .WithOne()
            .HasForeignKey<LivroEntity>("IdAutor")
            .IsRequired();

        builder
            .HasOne(e => e.Serie)
            .WithOne()
            .HasForeignKey<LivroEntity>("IdSerie")
            .IsRequired(false);

        builder.HasIndex(e => new
        {
            e.Titulo,
            e.Autor,
            e.Serie,
            e.Ordem
        }).IsUnique();
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
