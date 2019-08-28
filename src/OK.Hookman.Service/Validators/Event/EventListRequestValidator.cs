using FluentValidation;
using OK.Hookman.Core.Requests.Event;

namespace OK.Hookman.Service.Validators.Event
{
    public class EventListRequestValidator : AbstractValidator<EventListRequest>
    {
        public EventListRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}