using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OK.Hookman.Core.Requests.Action;
using OK.Hookman.Core.Requests.Event;
using OK.Hookman.Core.Requests.Hook;
using OK.Hookman.Core.Requests.Receiver;
using OK.Hookman.Core.Requests.Sender;
using OK.Hookman.Core.Requests.Stat;
using OK.Hookman.Core.Requests.Status;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.HostedServices;
using OK.Hookman.Service.Implementations;
using OK.Hookman.Service.Validators.Action;
using OK.Hookman.Service.Validators.Event;
using OK.Hookman.Service.Validators.Hook;
using OK.Hookman.Service.Validators.Receiver;
using OK.Hookman.Service.Validators.Sender;
using OK.Hookman.Service.Validators.Stat;
using OK.Hookman.Service.Validators.Status;

namespace OK.Hookman.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHookmanService(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ActionListRequest>, ActionListRequestValidator>();
            services.AddTransient<IValidator<ActionDetailRequest>, ActionDetailRequestValidator>();
            services.AddTransient<IValidator<ActionCreateRequest>, ActionCreateRequestValidator>();
            services.AddTransient<IValidator<ActionEditRequest>, ActionEditRequestValidator>();
            services.AddTransient<IValidator<ActionDeleteRequest>, ActionDeleteRequestValidator>();

            services.AddTransient<IValidator<EventListRequest>, EventListRequestValidator>();
            services.AddTransient<IValidator<EventDetailRequest>, EventDetailRequestValidator>();
            services.AddTransient<IValidator<EventCreateRequest>, EventCreateRequestValidator>();
            services.AddTransient<IValidator<EventEditRequest>, EventEditRequestValidator>();
            services.AddTransient<IValidator<EventDeleteRequest>, EventDeleteRequestValidator>();

            services.AddTransient<IValidator<HookListRequest>, HookListRequestValidator>();
            services.AddTransient<IValidator<HookDetailRequest>, HookDetailRequestValidator>();
            services.AddTransient<IValidator<HookCreateRequest>, HookCreateRequestValidator>();
            services.AddTransient<IValidator<HookDeleteRequest>, HookDeleteRequestValidator>();

            services.AddTransient<IValidator<ReceiverListRequest>, ReceiverListRequestValidator>();
            services.AddTransient<IValidator<ReceiverDetailRequest>, ReceiverDetailRequestValidator>();
            services.AddTransient<IValidator<ReceiverCreateRequest>, ReceiverCreateRequestValidator>();
            services.AddTransient<IValidator<ReceiverEditRequest>, ReceiverEditRequestValidator>();
            services.AddTransient<IValidator<ReceiverDeleteRequest>, ReceiverDeleteRequestValidator>();

            services.AddTransient<IValidator<SenderListRequest>, SenderListRequestValidator>();
            services.AddTransient<IValidator<SenderDetailRequest>, SenderDetailRequestValidator>();
            services.AddTransient<IValidator<SenderCreateRequest>, SenderCreateRequestValidator>();
            services.AddTransient<IValidator<SenderEditRequest>, SenderEditRequestValidator>();
            services.AddTransient<IValidator<SenderDeleteRequest>, SenderDeleteRequestValidator>();

            services.AddTransient<IValidator<StatTopActionListRequest>, StatTopActionListRequestValidator>();
            services.AddTransient<IValidator<StatusListRequest>, StatusListRequestValidator>();

            services.AddTransient<IActionService, ActionService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IHookService, HookService>();
            services.AddTransient<IReceiverService, ReceiverService>();
            services.AddTransient<ISenderService, SenderService>();
            services.AddTransient<IStatService, StatService>();
            services.AddTransient<IStatusService, StatusService>();

            services.AddAutoMapper();

            services.AddHostedService<RequesterHostedService>();
            services.AddSingleton<IRequesterQueue, RequesterQueue>();

            return services;
        }
    }
}