using FluentValidation;
using OK.Hookman.Core.Requests.Event;

namespace OK.Hookman.Service.Validators.Event
{
    public class EventDetailRequestValidator : AbstractValidator<EventDetailRequest>
    {
        public EventDetailRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}