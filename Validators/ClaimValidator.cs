using CMCS.Web.Models;
using FluentValidation;

namespace CMCS.Web.Validators
{
    public class ClaimValidator : AbstractValidator<Claim>
    {
        public ClaimValidator()
        {
            RuleFor(x => x.LecturerId).GreaterThan(0);
            RuleFor(x => x.HoursWorked).GreaterThan(0).WithMessage("Hours Worked must be greater than 0.");
            RuleFor(x => x.HourlyRate).GreaterThan(0).WithMessage("Hourly Rate must be greater than 0.");
            RuleFor(x => x.Notes).MaximumLength(500);
        }
    }
}