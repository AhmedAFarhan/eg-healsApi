using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGHeals.Domain.ValueObjects.Shared.Patients
{
    public record GuardianId
    {
        public Guid Value { get; }

        private GuardianId(Guid value) => Value = value;

        public static GuardianId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("GuardianId can not be empty");
            }

            return new GuardianId(value);
        }
        public static GuardianId? OfNullable(Guid? value)
        {
            if (value is null)
            {
                return null;
            }

            if (value == Guid.Empty)
            {
                //Throw custom exception
                throw new DomainException("GuardianId can not be empty");
            }

            return new GuardianId(value.Value);
        }
    }
}
