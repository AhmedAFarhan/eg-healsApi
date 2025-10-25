using BuildingBlocks.Domain.Exceptions;

namespace EGHeals.Domain.ValueObjects.Shared.Stuff
{
    public record TeamWorkMemberId
    {
        public Guid Value { get; }

        private TeamWorkMemberId(Guid value) => Value = value;

        public static TeamWorkMemberId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TeamWorkMemberId can not be empty");
            }

            return new TeamWorkMemberId(value);
        }
    }
}
