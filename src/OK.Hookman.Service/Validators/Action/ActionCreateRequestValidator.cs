using FluentValidation;
using OK.Hookman.Core.Requests.Action;

namespace OK.Hookman.Service.Validators.Action
{
    public class ActionCreateRequestValidator : AbstractValidator<ActionCreateRequest>
    {
        public ActionCreateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}