using EGHeals.Domain.ValueObjects.Shared.Owners;
using EGHeals.Domain.ValueObjects.Shared.Stuff;

namespace EGHeals.Domain.Models.Shared.Stuff
{
    public class TeamWorkMemberBranch : Entity<TeamWorkMemberBranchId>
    {
        internal TeamWorkMemberBranch(TeamWorkMemberId teamWorkMemberId, BranchId branchId)
        {
            Id = TeamWorkMemberBranchId.Of(Guid.NewGuid());
            TeamWorkMemberId = teamWorkMemberId;
            BranchId = branchId;
        }

        public TeamWorkMemberId TeamWorkMemberId { get; private set; } = default!;
        public BranchId BranchId { get; private set; } = default!;
    }
}
