using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PixelCoworking.Api.Models;

namespace PixelCoworking.Api.Data;

public partial class PixelCoworkingDbContext : DbContext
{
    public PixelCoworkingDbContext()
    {
    }

    public PixelCoworkingDbContext(DbContextOptions<PixelCoworkingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Space> Spaces { get; set; }
       
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Space>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
