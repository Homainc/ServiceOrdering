﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IEmployeeRepository : IRepository<EmployeeProfile>
    {
        Task<bool> AnyEmployeeAsync(Expression<Func<EmployeeProfile, bool>> filter);
        Task<EmployeeProfile> GetEagerByIdOrDefaultAsync(Guid id);
        Task<IPagedResult<EmployeeProfile>> GetPagedEmployeesAsync(int pageSize, int pageNumber, string serviceName,
            decimal? maxServiceCost, int? minAverageRate);
    }
}
