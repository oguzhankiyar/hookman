using FluentValidation;
using OK.Hookman.Core.Requests.Hook;

namespace OK.Hookman.Service.Validators.Hook
{
    public class HookListRequestValidator : AbstractValidator<HookListRequest>
    {
        public HookListRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}