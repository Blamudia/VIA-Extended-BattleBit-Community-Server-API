using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SoftDeletes.ModelTools;
using SoftDeletes.Core;

namespace CommunityServerAPI.VIA.Modules.Players.Context.Models
{
    [Table("player", Schema = "player_data")]
    public class Player : ModelExtension
    {
        [Key]
        public ulong SteamId { get; set; }
        public string Name { get; set; }
        public bool IsAPenguin { get; set; }

        #region Relations
        [InverseProperty(nameof(PlayerStats.Player))]
        public virtual PlayerStats Stats { get; set; } = default!;

        [InverseProperty(nameof(PlayerProgress.Player))]
        public virtual PlayerStats Progress { get; set; } = default!;
        #endregion


        #region SoftDelete/Timestamps
        public override void LoadRelations(DbContext context) { }

        public override Task LoadRelationsAsync(DbContext context, CancellationToken cancellationToken = default) { return Task.CompletedTask; }

        public override void OnSoftDelete(DbContext context) { }

        public override Task OnSoftDeleteAsync(DbContext context, CancellationToken cancellationToken = default) { return Task.CompletedTask; }
        #endregion
    }
}
