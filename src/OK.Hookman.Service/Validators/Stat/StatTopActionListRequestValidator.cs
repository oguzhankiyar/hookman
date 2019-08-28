using FluentValidation;
using OK.Hookman.Core.Requests.Stat;

namespace OK.Hookman.Service.Validators.Stat
{
    public class StatTopActionListRequestValidator : AbstractValidator<StatTopActionListRequest>
    {
        public StatTopActionListRequestValidator()
        {
        }
    }
}