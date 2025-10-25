using EGHeals.Domain.ValueObjects.Shared.Allowances;
using EGHeals.Domain.ValueObjects.Shared.Stuff;

namespace EGHeals.Domain.Models.Shared.Stuff
{
    public class TeamWorkMemberAllowance : Entity<TeamWorkMemberAllowanceId>
    {
        internal TeamWorkMemberAllowance(TeamWorkMemberId teamWorkMemberId, AllowanceId allowanceId, decimal cost)
        {
            Id = TeamWorkMemberAllowanceId.Of(Guid.NewGuid());
            TeamWorkMemberId = teamWorkMemberId;
            AllowanceId = allowanceId;
            Cost = cost;
        }

        public TeamWorkMemberId TeamWorkMemberId { get; private set; } = default!;
        public AllowanceId AllowanceId { get; private set; } = default!;
        public decimal Cost { get; set; } = default!;
    }
}
