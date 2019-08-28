using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OK.Hookman.Core.Exceptions;

namespace OK.Hookman.Service.Base
{
    public abstract class BaseService
    {
        private readonly IServiceProvider _serviceProvider;

        public BaseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected virtual async Task ValidateRequestAsync<TRequest>(TRequest request) where TRequest : class
        {
            var validator = _serviceProvider.GetService(typeof(IValidator<TRequest>)) as IValidator<TRequest>;
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new RequestNotValidatedException(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
            }
        }
    }
}