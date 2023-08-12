using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SoftDeletes.ModelTools;
using SoftDeletes.Core;
using BattleBitAPI;

namespace CommunityServerAPI.Modules.Players.Context.Models
{
    [Table("progress", Schema = "player_data")]
    public class PlayerProgress : ModelExtension
    {
        [Key]
        public Guid Id { get; set; }
        public ulong PlayerId { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<byte> ToolProgress { get; set; } = default!;

        [Column(TypeName = "jsonb")]
        public IEnumerable<byte> Achievements { get; set; } = default!;

        [Column(TypeName = "jsonb")]
        public IEnumerable<byte> Selections { get; set; } = default!;

        #region Relations
        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; }
        #endregion


        #region SoftDelete/Timestamps
        public override void LoadRelations(DbContext context) { }

        public override Task LoadRelationsAsync(DbContext context, CancellationToken cancellationToken = default) { return Task.CompletedTask; }

        public override void OnSoftDelete(DbContext context) { }

        public override Task OnSoftDeleteAsync(DbContext context, CancellationToken cancellationToken = default) { return Task.CompletedTask; }
        #endregion
    }
}
