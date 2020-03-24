using System;
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
        public async Task<IResult<ReviewDTO>> CreateAsync(ReviewDTO reviewDto, CancellationToken token)
        {
            var reviewEmployee = await Database.EmployeeProfiles.GetAll()
                .SingleOrDefaultAsync(x => x.Id == reviewDto.EmployeeId, token);
            if (reviewEmployee == null)
            {
                var result = new Result<ReviewDTO>($"Employee with id {reviewDto.EmployeeId} not found!");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            var reviewClient = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Id == reviewDto.ClientId, token);
            if (reviewClient == null)
            {
                var result = new Result<ReviewDTO>($"Client with id {reviewDto.ClientId} not found!");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            reviewDto.Date = DateTime.Now;
            var review = Mapper.Map<Review>(reviewDto);
            await Database.Reviews.CreateAsync(review, token); 
            await Database.SaveAsync(token);

            Logger.LogInformation($"Review to user with id {reviewDto.EmployeeId} was added");
            return new Result<ReviewDTO>(Mapper.Map<ReviewDTO>(review));

        }

        public async Task<IResult<ReviewDTO>> DeleteAsync(int id, CancellationToken token)
        {
            var review = await Database.Reviews.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (review == null)
            {
                var result = new Result<ReviewDTO>($"Review with id {id} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            Database.Reviews.Delete(review);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Review with id {id} was deleted");
            return new Result<ReviewDTO>(Mapper.Map<ReviewDTO>(review));
        }

        public async Task<IPagedResult<ReviewDTO>> GetUserReviewsAsync(Guid userId, CancellationToken token) =>
            new PagedResult<ReviewDTO>(await Database.Reviews.GetAll().Where(x => x.EmployeeId == userId)
                .ProjectTo<ReviewDTO>(Mapper.ConfigurationProvider).ToListAsync(token));

        public void Dispose() => Database.Dispose();
    }
}
