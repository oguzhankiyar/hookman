using FluentValidation;
using OK.Hookman.Core.Requests.Receiver;

namespace OK.Hookman.Service.Validators.Receiver
{
    public class ReceiverDeleteRequestValidator : AbstractValidator<ReceiverDeleteRequest>
    {
        public ReceiverDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}