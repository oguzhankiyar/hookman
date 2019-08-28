using FluentValidation;
using OK.Hookman.Core.Requests.Sender;

namespace OK.Hookman.Service.Validators.Sender
{
    public class SenderEditRequestValidator : AbstractValidator<SenderEditRequest>
    {
        public SenderEditRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}