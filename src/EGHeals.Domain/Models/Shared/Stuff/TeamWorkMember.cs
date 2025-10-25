using EGHeals.Domain.ValueObjects.Shared.Allowances;
using EGHeals.Domain.ValueObjects.Shared.Owners;
using EGHeals.Domain.ValueObjects.Shared.Stuff;

namespace EGHeals.Domain.Models.Shared.Stuff
{
    public class TeamWorkMember : Aggregate<TeamWorkMemberId>
    {
        private readonly List<TeamWorkMemberAllowance> _allowances = new();
        private readonly List<TeamWorkMemberBranch> _branches = new();
        public IReadOnlyList<TeamWorkMemberAllowance> Allowances => _allowances.AsReadOnly();
        public IReadOnlyList<TeamWorkMemberBranch> Branches => _branches.AsReadOnly();

        public string FullName { get; private set; } = default!;
        public string? NationalId { get; private set; } = default!;
        public string Mobile { get; private set; } = default!;
        public StuffTitle StuffTitle { get; private set; } = StuffTitle.UNKNOWN;
        public StuffSalaryType StuffSalaryType { get; private set; } = StuffSalaryType.DAILY;
        public decimal Salary { get; private set; }
        public SystemUserId? UserMemberId { get; private set; } = default!;

        public static TeamWorkMember Create(string fullName, string? nationalId, string mobile, StuffTitle stuffTitle, StuffSalaryType stuffSalaryType, decimal salary, SystemUserId? userMemberId)
        {
            Validation(fullName, nationalId, mobile, stuffTitle, stuffSalaryType, salary);

            var teamWorkMember = new TeamWorkMember
            {
                Id = TeamWorkMemberId.Of(Guid.NewGuid()),
                FullName = fullName,
                NationalId = nationalId,
                Mobile = mobile,
                StuffTitle = stuffTitle,
                StuffSalaryType = stuffSalaryType,
                Salary = salary,
                UserMemberId = userMemberId
            };

            return teamWorkMember;
        }
        public void Update(string fullName, string? nationalId, string mobile, StuffTitle stuffTitle, StuffSalaryType stuffSalaryType, decimal salary, SystemUserId? userMemberId)
        {
            Validation(fullName, nationalId, mobile, stuffTitle, stuffSalaryType, salary);

            FullName = fullName;
            NationalId = nationalId;
            Mobile = mobile;
            StuffTitle = stuffTitle;
            StuffSalaryType = stuffSalaryType;
            Salary = salary;
            UserMemberId = userMemberId;
        }

        public void AddAllowance(AllowanceId allowanceId, decimal cost)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cost);

            var allowance = new TeamWorkMemberAllowance(Id, allowanceId, cost);

            _allowances.Add(allowance);
        }
        public void UpdateAllowance(AllowanceId allowanceId, decimal cost)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cost);

            var allowance = _allowances.FirstOrDefault(x => x.AllowanceId == allowanceId);

            if (allowance is not null)
            {
                allowance.Cost = cost;
            }
        }
        public void RemoveAllowance(AllowanceId allowanceId)
        {
            var allowance = _allowances.FirstOrDefault(x => x.AllowanceId == allowanceId);

            if (allowance is not null)
            {
                _allowances.Remove(allowance);
            }
        }

        public void AddBranch(BranchId branchId)
        {
            var branch = new TeamWorkMemberBranch(Id, branchId);

            _branches.Add(branch);
        }
        public void RemoveBranch(BranchId branchId)
        {
            var branch = _branches.FirstOrDefault(x => x.BranchId == branchId);

            if (branch is not null)
            {
                _branches.Remove(branch);
            }
        }

        private static void Validation(string fullName, string? nationalId, string mobile, StuffTitle stuffTitle, StuffSalaryType stuffSalaryType, decimal salary)
        {
            ArgumentException.ThrowIfNullOrEmpty(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(fullName.Length, 150);

            if(nationalId is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(nationalId);
                ArgumentOutOfRangeException.ThrowIfNotEqual(nationalId.Length, 14);
            }

            ArgumentException.ThrowIfNullOrEmpty(mobile);
            ArgumentException.ThrowIfNullOrWhiteSpace(mobile);
            ArgumentOutOfRangeException.ThrowIfNotEqual(mobile.Length, 11);

            if (!Enum.IsDefined(stuffTitle))
            {
                throw new DomainException("stuffTitle value is out of range");
            }

            if (!Enum.IsDefined(stuffSalaryType))
            {
                throw new DomainException("stuffSalaryType value is out of range");
            }

            ArgumentOutOfRangeException.ThrowIfNegative(salary);
        }
    }
}   
