using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OK.Hookman.Core.Models;
using OK.Hookman.Core.Requests.Stat;
using OK.Hookman.Core.Responses.Stat;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.Base;

namespace OK.Hookman.Service.Implementations
{
    public class StatService : BaseService, IStatService
    {
        private readonly IHookRepository _hookRepository;
        private readonly IMapper _mapper;

        public StatService(IHookRepository hookRepository, IMapper mapper, IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _hookRepository = hookRepository ?? throw new ArgumentNullException(nameof(hookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StatTopActionListResponse> GetTopActionListAsync(StatTopActionListRequest request)
        {
            await ValidateRequestAsync(request);

            var response = new StatTopActionListResponse();

            var monthNames = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            switch (request.Period)
            {
                case "this_year":
                    {
                        var startDate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                        var endDate = startDate.AddYears(1).AddSeconds(-1);

                        var months = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

                        response.Data = _hookRepository
                            .FindMany(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                            .GroupBy(x => x.Event.Action.Name)
                            .OrderByDescending(x => x.Count())
                            .Take(10)
                            .ToList()
                            .Select(x =>
                            {
                                var action = x.Key;
                                var values = x
                                    .GroupBy(y => y.CreatedDate.Month)
                                    .OrderBy(y => y.Key)
                                    .ToDictionary(k => k.Key, v => v.Count());

                                return new StatTopActionModel()
                                {
                                    Action = action,
                                    Values = months.Select(y => new StatTopActionDateValueModel()
                                    {
                                        Date = $"{startDate.Year} {monthNames[y - 1]}",
                                        Value = (values.ContainsKey(y) ? values[y] : 0).ToString()
                                    })
                                    .ToList()
                                };
                            })
                            .ToList();
                    }
                    break;
                case "this_month":
                    {
                        var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                        var endDate = startDate.AddMonths(1).AddSeconds(-1);

                        var days = new List<int>();
                        for (int i = startDate.Day; i <= endDate.Day; i++)
                        {
                            days.Add(i);
                        }

                        response.Data = _hookRepository
                            .FindMany(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                            .GroupBy(x => x.Event.Action.Name)
                            .OrderByDescending(x => x.Count())
                            .Take(10)
                            .ToList()
                            .Select(x =>
                            {
                                var action = x.Key;
                                var values = x
                                    .GroupBy(y => y.CreatedDate.Day)
                                    .OrderBy(y => y.Key)
                                    .ToDictionary(k => k.Key, v => v.Count());

                                return new StatTopActionModel()
                                {
                                    Action = action,
                                    Values = days.Select(y => new StatTopActionDateValueModel()
                                    {
                                        Date = $"{monthNames[startDate.Month - 1]} {y}",
                                        Value = (values.ContainsKey(y) ? values[y] : 0).ToString()
                                    })
                                    .ToList()
                                };
                            })
                            .ToList();
                    }
                    break;
                case "this_week":
                    {
                        var diff = (7 + (DateTime.UtcNow.DayOfWeek - DayOfWeek.Monday)) % 7;
                        var startDate = DateTime.UtcNow.AddDays(-1 * diff).Date;
                        var endDate = startDate.AddDays(7).AddSeconds(-1);

                        var days = new List<DateTime>();
                        var tempDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
                        while (tempDate <= endDate.Date)
                        {
                            days.Add(tempDate.Date);
                            tempDate = tempDate.AddDays(1);
                        }

                        response.Data = _hookRepository
                            .FindMany(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                            .GroupBy(x => x.Event.Action.Name)
                            .OrderByDescending(x => x.Count())
                            .Take(10)
                            .ToList()
                            .Select(x =>
                            {
                                var action = x.Key;
                                var values = x
                                    .GroupBy(y => y.CreatedDate.Date)
                                    .OrderBy(y => y.Key)
                                    .ToDictionary(k => k.Key, v => v.Count());

                                return new StatTopActionModel()
                                {
                                    Action = action,
                                    Values = days.Select(y => new StatTopActionDateValueModel()
                                    {
                                        Date = $"{monthNames[y.Month - 1]} {y.Day}",
                                        Value = (values.ContainsKey(y) ? values[y] : 0).ToString()
                                    })
                                    .ToList()
                                };
                            })
                            .ToList();
                    }
                    break;
                case "yesterday":
                    {
                        var startDate = DateTime.UtcNow.Date.AddDays(-1);
                        var endDate = startDate.AddDays(1).AddSeconds(-1);

                        var hours = new List<int>();
                        for (int i = startDate.Hour; i <= endDate.Hour; i++)
                        {
                            hours.Add(i);
                        }

                        response.Data = _hookRepository
                            .FindMany(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                            .GroupBy(x => x.Event.Action.Name)
                            .OrderByDescending(x => x.Count())
                            .Take(10)
                            .ToList()
                            .Select(x =>
                            {
                                var action = x.Key;
                                var values = x
                                    .GroupBy(y => y.CreatedDate.Hour)
                                    .OrderBy(y => y.Key)
                                    .ToDictionary(k => k.Key, v => v.Count());

                                return new StatTopActionModel()
                                {
                                    Action = action,
                                    Values = hours.Select(y => new StatTopActionDateValueModel()
                                    {
                                        Date = $"{y.ToString().PadLeft(2, '0')}:00",
                                        Value = (values.ContainsKey(y) ? values[y] : 0).ToString()
                                    })
                                    .ToList()
                                };
                            })
                            .ToList();
                    }
                    break;
                case "today":
                    {
                        var startDate = DateTime.UtcNow.Date;
                        var endDate = startDate.AddDays(1).AddSeconds(-1);

                        var hours = new List<int>();
                        for (int i = startDate.Hour; i <= endDate.Hour; i++)
                        {
                            hours.Add(i);
                        }

                        response.Data = _hookRepository
                            .FindMany(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                            .GroupBy(x => x.Event.Action.Name)
                            .OrderByDescending(x => x.Count())
                            .Take(10)
                            .ToList()
                            .Select(x =>
                            {
                                var action = x.Key;
                                var values = x
                                    .GroupBy(y => y.CreatedDate.Hour)
                                    .OrderBy(y => y.Key)
                                    .ToDictionary(k => k.Key, v => v.Count());

                                return new StatTopActionModel()
                                {
                                    Action = action,
                                    Values = hours.Select(y => new StatTopActionDateValueModel()
                                    {
                                        Date = $"{y.ToString().PadLeft(2, '0')}:00",
                                        Value = (values.ContainsKey(y) ? values[y] : 0).ToString()
                                    })
                                    .ToList()
                                };
                            })
                            .ToList();
                    }
                    break;
            }

            return response;
        }
    }
}