namespace EGHeals.Domain.ValueObjects.Shared.Stuff
{
    public record TeamWorkMemberBranchId
    {
        public Guid Value { get; }

        private TeamWorkMemberBranchId(Guid value) => Value = value;

        public static TeamWorkMemberBranchId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TeamWorkMemberBranchId can not be empty");
            }

            return new TeamWorkMemberBranchId(value);
        }
    }
}
