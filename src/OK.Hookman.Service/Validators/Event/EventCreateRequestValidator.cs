using System.Linq;
using FluentValidation;
using OK.Hookman.Core.Requests.Event;

namespace OK.Hookman.Service.Validators.Event
{
    public class EventCreateRequestValidator : AbstractValidator<EventCreateRequest>
    {
        public EventCreateRequestValidator()
        {
            RuleFor(x => x.SenderId)
                .GreaterThan(0)
                .When(x => x.SenderId != null);

            RuleFor(x => x.ReceiverId)
                .GreaterThan(0);
                
            RuleFor(x => x.ActionId)
                .GreaterThan(0);
                
            RuleFor(x => x.Method)
                .Must((value) => { return new string[] { "GET", "POST", "PUT", "DELETE" }.Contains(value); })
                .WithMessage("Supported methods: GET, POST, PUT, DELETE");

            RuleFor(x => x.RetryCount)
                .GreaterThanOrEqualTo(0);
        }
    }
}