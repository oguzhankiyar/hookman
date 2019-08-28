using FluentValidation;
using OK.Hookman.Core.Requests.Sender;

namespace OK.Hookman.Service.Validators.Sender
{
    public class SenderCreateRequestValidator : AbstractValidator<SenderCreateRequest>
    {
        public SenderCreateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}