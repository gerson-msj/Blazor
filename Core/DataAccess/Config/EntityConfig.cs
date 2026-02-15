using System;
using Blazor.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazor.Core.DataAccess.Config;

public class LicroConfig : IEntityTypeConfiguration<LivroEntity>
{
    public void Configure(EntityTypeBuilder<LivroEntity> builder)
    {
        builder.ToTable("Livro");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Titulo).IsRequired().HasMaxLength(50);
    }
}
