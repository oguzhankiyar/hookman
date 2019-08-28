using FluentValidation;
using OK.Hookman.Core.Requests.Sender;

namespace OK.Hookman.Service.Validators.Sender
{
    public class SenderListRequestValidator : AbstractValidator<SenderListRequest>
    {
        public SenderListRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}