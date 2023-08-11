using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SoftDeletes.ModelTools;
using SoftDeletes.Core;

namespace BBR.Community.API.Modules.Player.Context.Models
{
    [Table("stats", Schema = "player_data")]
    public class PlayerStats : ModelExtension
    {
        [Key]
        public ulong Id { get; set; }

        [Column(TypeName = "jsonb")]
        public IEnumerable<byte> ToolProgress { get; set; } = default!;

        [Column(TypeName = "jsonb")]
        public IEnumerable<byte> Achievements { get; set; } = default!;

        [Column(TypeName = "jsonb")]
        public IEnumerable<byte> Selections { get; set; } = default!;
        #region Relations

        #endregion


        #region SoftDelete/Timestamps
        public override void LoadRelations(DbContext context) { }

        public override Task LoadRelationsAsync(DbContext context, CancellationToken cancellationToken = default) { return Task.CompletedTask; }

        public override void OnSoftDelete(DbContext context) { }

        public override Task OnSoftDeleteAsync(DbContext context, CancellationToken cancellationToken = default) { return Task.CompletedTask; }
        #endregion
    }
}
