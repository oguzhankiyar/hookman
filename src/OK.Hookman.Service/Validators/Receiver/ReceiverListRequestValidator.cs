using FluentValidation;
using OK.Hookman.Core.Requests.Receiver;

namespace OK.Hookman.Service.Validators.Receiver
{
    public class ReceiverListRequestValidator : AbstractValidator<ReceiverListRequest>
    {
        public ReceiverListRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}