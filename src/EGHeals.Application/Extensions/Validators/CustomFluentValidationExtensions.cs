using System.Text.RegularExpressions;

namespace EGHeals.Application.Extensions.Validators
{
    public static class CustomFluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string?> EgyptianMobile<T>(this IRuleBuilder<T, string?> rule, bool isOptional = false)
        {
            return rule.Must(value =>
            {
                // Case 1: null or empty
                if (string.IsNullOrWhiteSpace(value))
                    return isOptional;

                // Case 2: not empty, must match regex
                return Regex.IsMatch(value, @"^(010|011|012|015)\d{8}$");
            })
            .WithMessage("Invalid mobile number");
        }
        public static IRuleBuilderOptions<T, string> EgyptianMobile<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.Matches(@"^(010|011|012|015)\d{8}$").WithMessage("Invalid mobile number");
        }
        public static IRuleBuilderOptions<T, string> NotNullOrWhitespace<T>(this IRuleBuilder<T, string> rule, string msg)
        {
            return rule.Must(value => !string.IsNullOrWhiteSpace(value)).WithMessage(msg);
        }
        public static IRuleBuilderOptions<T, string> NoWhitespacesAllowed<T>(this IRuleBuilder<T, string> rule, string msg)
        {
            return rule.Must(value => !value.Contains(" ")).WithMessage(msg);
        }
    }
}
