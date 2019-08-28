using FluentValidation;
using OK.Hookman.Core.Requests.Receiver;

namespace OK.Hookman.Service.Validators.Receiver
{
    public class ReceiverCreateRequestValidator : AbstractValidator<ReceiverCreateRequest>
    {
        public ReceiverCreateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Url)
                .NotEmpty()
                .NotNull();
        }
    }
}