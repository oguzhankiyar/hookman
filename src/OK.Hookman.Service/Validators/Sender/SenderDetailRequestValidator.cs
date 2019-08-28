using FluentValidation;
using OK.Hookman.Core.Requests.Sender;

namespace OK.Hookman.Service.Validators.Sender
{
    public class SenderDetailRequestValidator : AbstractValidator<SenderDetailRequest>
    {
        public SenderDetailRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}