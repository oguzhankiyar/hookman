using FluentValidation;
using OK.Hookman.Core.Requests.Action;

namespace OK.Hookman.Service.Validators.Action
{
    public class ActionEditRequestValidator : AbstractValidator<ActionEditRequest>
    {
        public ActionEditRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}