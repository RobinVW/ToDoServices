using Microsoft.EntityFrameworkCore;
using WEB4_ToDoServices.Models;
using WEB4_ToDoServices.Models.Base;

namespace WEB4_ToDoServices.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Column> Columns { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Board>().Navigation(c => c.Columns).AutoInclude();
			modelBuilder.Entity<Column>().Navigation(c => c.Cards).AutoInclude();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var insertedEntries = this.ChangeTracker.Entries()
								   .Where(x => x.State == EntityState.Added)
								   .Select(x => x.Entity);
			foreach (var insertedEntry in insertedEntries)
			{
				var auditableEntity = insertedEntry as Auditable;
				//If the inserted object is an Auditable. 
				if (auditableEntity != null)
				{
					auditableEntity.DateCreated = DateTimeOffset.Now;
				}
			}
			var modifiedEntries = this.ChangeTracker.Entries()
					   .Where(x => x.State == EntityState.Modified)
					   .Select(x => x.Entity);
			foreach (var modifiedEntry in modifiedEntries)
			{
				//If the inserted object is an Auditable. 
				var auditableEntity = modifiedEntry as Auditable;
				if (auditableEntity != null)
				{
					auditableEntity.DateUpdated = DateTimeOffset.Now;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
