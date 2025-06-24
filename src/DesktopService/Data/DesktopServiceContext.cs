namespace DesktopService.Data;
using Microsoft.EntityFrameworkCore;
using DesktopService.Entity;

public class DesktopServiceContext : DbContext
{
    public DesktopServiceContext(DbContextOptions<DesktopServiceContext> options) : base(options)
    {
    }

    public virtual DbSet<ForeignSentence> ForeignSentences { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ForeignSentence>().ToTable("ForeignSentences");
        modelBuilder.Entity<ForeignSentence>()
            .HasKey(key => key.Id);
        modelBuilder.Entity<ForeignSentence>()
            .Property(p=>p.Id)
            .HasColumnName("id")
            .UseSequence("foreign_sentence_id_seq")
            .IsRequired();
        modelBuilder.Entity<ForeignSentence>()
            .Property(p => p.Sentence)
            .HasColumnName("sentence")
            .IsRequired();
        base.OnModelCreating(modelBuilder);
    }
}