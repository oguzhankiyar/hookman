using FluentValidation;
using OK.Hookman.Core.Requests.Hook;

namespace OK.Hookman.Service.Validators.Hook
{
    public class HookCreateRequestValidator : AbstractValidator<HookCreateRequest>
    {
        public HookCreateRequestValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0)
                .When(x => x.ActionId == null && string.IsNullOrEmpty(x.ActionName));
                
            RuleFor(x => x.ActionId)
                .GreaterThan(0)
                .When(x => x.EventId == null && string.IsNullOrEmpty(x.ActionName));
                
            RuleFor(x => x.ActionName)
                .NotEmpty()
                .NotNull()
                .When(x => x.EventId == null && x.ActionId == null);

            RuleFor(x => x.SenderToken)
                .NotEmpty()
                .NotNull();
        }
    }
}