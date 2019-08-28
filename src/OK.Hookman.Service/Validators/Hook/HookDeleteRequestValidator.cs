using FluentValidation;
using OK.Hookman.Core.Requests.Hook;

namespace OK.Hookman.Service.Validators.Hook
{
    public class HookDeleteRequestValidator : AbstractValidator<HookDeleteRequest>
    {
        public HookDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}