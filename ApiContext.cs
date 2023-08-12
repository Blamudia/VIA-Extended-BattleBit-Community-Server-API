using CommunityServerAPI.VIA.Modules.Players.Context.Models;
using Microsoft.EntityFrameworkCore;
using SoftDeletes.ModelTools;

namespace BBR.Community.API
{
    public class ApiContext : SoftDeletes.Core.DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            // TODO Remove this line later on
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public virtual DbSet<Player> Players { get; set; } = default!;
        public virtual DbSet<PlayerStats> PlayerStats { get; set; } = default!;
        public virtual DbSet<PlayerProgress> PlayerProgresses { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasQueryFilter(post => post.DeletedAt == null);
                entity.HasIndex(x => x.DeletedAt);
            });

            modelBuilder.Entity<PlayerStats>(entity =>
            {
                entity.HasQueryFilter(post => post.DeletedAt == null);
                entity.HasIndex(x => x.DeletedAt);
            });

            modelBuilder.Entity<PlayerProgress>(entity =>
            {
                entity.HasQueryFilter(post => post.DeletedAt == null);
                entity.HasIndex(x => x.DeletedAt);
            });
        }

        /// <summary>
        /// Will call on saving changes.
        /// </summary>
        /// <remarks>
        /// Will set new entities CreatedAt and UpdatedAt to current date and time
        /// if they implement from ITimestamps interface.
        /// </remarks>
        /// <see cref="ITimestamps"/>
        protected override void SetNewEntitiesTimestamps()
        {
            var newRecords = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added && x.Entity is ITimestamps)
                .Select(x => x.Entity as ITimestamps);

            foreach (var record in newRecords)
            {
                if (record == null)
                {
                    continue;
                }

                var nowDateTime = DateTime.UtcNow;
                record.CreatedAt = nowDateTime;
                record.UpdatedAt = nowDateTime;
            }
        }

        /// <summary>
        /// Will call on saving changes.
        /// </summary>
        /// <remarks>
        /// Will set updated entities UpdatedAt to current date and time
        /// if they implement from ITimestamps interface.
        /// </remarks>
        /// <see cref="ITimestamps"/>
        protected override void SetModifiedEntitiesTimestamps()
        {
            var updatedRecords = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified && x.Entity is ITimestamps)
                .Select(x => x.Entity);

            foreach (var record in updatedRecords)
            {
                if (record is ISoftDelete && AllowRestore == false)
                {
                    Entry(record).Property("DeletedAt").IsModified = false;
                }

                if (record is not ITimestamps timestampRecord)
                {
                    continue;
                }

                Entry(record).Property("CreatedAt").IsModified = false;
                timestampRecord.UpdatedAt = DateTime.UtcNow;
            }

            AllowRestore = false;
        }

        /// <summary>
        /// Will call on saving changes.
        /// </summary>
        /// <remarks>
        /// Will set deleted entities DeletedAt to current date and time
        /// if they implement from ISoftDelete interface and should soft delete.
        /// </remarks>
        /// <see cref="ISoftDelete"/>
        protected override void DetectSoftDeleteEntities()
        {
            var deletedRecords = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted && x.Entity is ISoftDelete)
                .Select(x => x);

            foreach (var record in deletedRecords)
            {
                if (((ISoftDelete)record.Entity).IsForceDelete())
                {
                    continue;
                }

                record.State = EntityState.Modified;
                ((ISoftDelete)record.Entity).DeletedAt = DateTime.UtcNow;
            }
        }
    }
}
