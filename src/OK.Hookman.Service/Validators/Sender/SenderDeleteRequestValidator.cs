using FluentValidation;
using OK.Hookman.Core.Requests.Sender;

namespace OK.Hookman.Service.Validators.Sender
{
    public class SenderDeleteRequestValidator : AbstractValidator<SenderDeleteRequest>
    {
        public SenderDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}