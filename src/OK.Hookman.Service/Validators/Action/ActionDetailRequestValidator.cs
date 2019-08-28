using FluentValidation;
using OK.Hookman.Core.Requests.Action;

namespace OK.Hookman.Service.Validators.Action
{
    public class ActionDetailRequestValidator : AbstractValidator<ActionDetailRequest>
    {
        public ActionDetailRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}