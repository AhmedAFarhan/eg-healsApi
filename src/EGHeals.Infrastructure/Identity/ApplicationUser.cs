using BuildingBlocks.Domain.Abstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, ISystemEntity
    {        
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
