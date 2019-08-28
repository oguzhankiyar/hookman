using FluentValidation;
using OK.Hookman.Core.Requests.Hook;

namespace OK.Hookman.Service.Validators.Hook
{
    public class HookDetailRequestValidator : AbstractValidator<HookDetailRequest>
    {
        public HookDetailRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}