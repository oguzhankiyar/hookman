using FluentValidation;
using OK.Hookman.Core.Requests.Event;

namespace OK.Hookman.Service.Validators.Event
{
    public class EventDeleteRequestValidator : AbstractValidator<EventDeleteRequest>
    {
        public EventDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}