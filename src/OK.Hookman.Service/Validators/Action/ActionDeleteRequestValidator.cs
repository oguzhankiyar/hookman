using FluentValidation;
using OK.Hookman.Core.Requests.Action;

namespace OK.Hookman.Service.Validators.Action
{
    public class ActionDeleteRequestValidator : AbstractValidator<ActionDeleteRequest>
    {
        public ActionDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}