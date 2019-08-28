using FluentValidation;
using OK.Hookman.Core.Requests.Receiver;

namespace OK.Hookman.Service.Validators.Receiver
{
    public class ReceiverEditRequestValidator : AbstractValidator<ReceiverEditRequest>
    {
        public ReceiverEditRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
                
            RuleFor(x => x.Url)
                .NotEmpty()
                .NotNull();
        }
    }
}