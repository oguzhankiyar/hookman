using FluentValidation;
using OK.Hookman.Core.Requests.Action;

namespace OK.Hookman.Service.Validators.Action
{
    public class ActionListRequestValidator : AbstractValidator<ActionListRequest>
    {
        public ActionListRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}