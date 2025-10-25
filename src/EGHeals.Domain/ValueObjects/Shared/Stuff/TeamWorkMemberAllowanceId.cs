namespace EGHeals.Domain.ValueObjects.Shared.Stuff
{
    public record TeamWorkMemberAllowanceId
    {
        public Guid Value { get; }

        private TeamWorkMemberAllowanceId(Guid value) => Value = value;

        public static TeamWorkMemberAllowanceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TeamWorkMemberAllowanceId can not be empty");
            }

            return new TeamWorkMemberAllowanceId(value);
        }
    }
}
