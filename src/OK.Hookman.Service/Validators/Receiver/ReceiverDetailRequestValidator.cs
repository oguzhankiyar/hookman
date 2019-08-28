using FluentValidation;
using OK.Hookman.Core.Requests.Receiver;

namespace OK.Hookman.Service.Validators.Receiver
{
    public class ReceiverDetailRequestValidator : AbstractValidator<ReceiverDetailRequest>
    {
        public ReceiverDetailRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}