using Microsoft.EntityFrameworkCore;

namespace RecordAPI.Models;
public class RecordContext : DbContext
{
    public RecordContext(DbContextOptions<RecordContext> options) : base(options)
    {
    }

    public DbSet<RecordItem> RecordItems { get; set; } = null!;
}