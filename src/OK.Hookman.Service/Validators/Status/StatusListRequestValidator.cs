using FluentValidation;
using OK.Hookman.Core.Requests.Status;

namespace OK.Hookman.Service.Validators.Status
{
    public class StatusListRequestValidator : AbstractValidator<StatusListRequest>
    {
        public StatusListRequestValidator()
        {
        }
    }
}