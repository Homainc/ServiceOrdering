using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class ReviewService : IReviewService
    {
        private IUnitOfWork Database { get; }
        private ILogger<ReviewService> Logger { get; }
        private IMapper Mapper { get; }
        public ReviewService(IUnitOfWork db, ILogger<ReviewService> logger, IMapper mapper)
        {
            Database = db;
            Logger = logger;
            Mapper = mapper;
        }
        public async Task<IResponse<ReviewDTO>> CreateAsync(ReviewDTO reviewDto, CancellationToken token)
        {
            var reviewEmployee = await Database.EmployeeProfiles.GetAll()
                .SingleOrDefaultAsync(x => x.Id == reviewDto.EmployeeId, token);
            if (reviewEmployee == null)
            {
                var result = Response<ReviewDTO>.ValidationError($"Employee with id {reviewDto.EmployeeId} not found!");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            var reviewClient = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Id == reviewDto.ClientId, token);
            if (reviewClient == null)
            {
                var result = Response<ReviewDTO>.ValidationError($"Client with id {reviewDto.ClientId} not found!");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            reviewDto.Date = DateTime.Now;
            var review = Mapper.Map<Review>(reviewDto);
            await Database.Reviews.CreateAsync(review, token); 
            await Database.SaveAsync(token);

            Logger.LogInformation($"Review to user with id {reviewDto.EmployeeId} was added");
            return Response<ReviewDTO>.Success(Mapper.Map<ReviewDTO>(review));

        }

        public async Task<IResponse<ReviewDTO>> DeleteAsync(int id, CancellationToken token)
        {
            var review = await Database.Reviews.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (review == null)
            {
                var result = Response<ReviewDTO>.NotFound($"Review with id {id} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            Database.Reviews.Delete(review);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Review with id {id} was deleted");
            return Response<ReviewDTO>.Success(Mapper.Map<ReviewDTO>(review));
        }

        public async Task<IResponse<IEnumerable<ReviewDTO>>>
            GetUserReviewsAsync(Guid userId, CancellationToken token) =>
            Response<IEnumerable<ReviewDTO>>.Success(await Database.Reviews.GetAll().Where(x => x.EmployeeId == userId)
                .ProjectTo<ReviewDTO>(Mapper.ConfigurationProvider).ToListAsync(token));

        public void Dispose() => Database.Dispose();
    }
}
