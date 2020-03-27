using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Domain.Logic.Helpers;

namespace OrderingService.Domain.Logic.Services
{
    public class ReviewService : IReviewService
    {
        private IUnitOfWork Database { get; }
        private IMapper Mapper { get; }
        public ReviewService(IUnitOfWork db, IMapper mapper)
        {
            Database = db;
            Mapper = mapper;
        }
        public async Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto, CancellationToken token)
        {
            var reviewEmployee = await Database.EmployeeProfiles.GetAll()
                .SingleOrDefaultAsync(x => x.Id == reviewDto.EmployeeId, token);
            if (reviewEmployee == null)
                throw new LogicException($"Employee with id {reviewDto.EmployeeId} not found!");
            
            var reviewClient = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Id == reviewDto.ClientId, token);
            if (reviewClient == null)
                throw new LogicException($"Client with id {reviewDto.ClientId} not found!");
            
            reviewDto.Date = DateTime.Now;
            var review = Mapper.Map<Review>(reviewDto);
            Database.Reviews.Create(review); 
            await Database.SaveAsync(token);

            return Mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> DeleteAsync(int id, CancellationToken token)
        {
            var review = await Database.Reviews.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (review == null)
                throw new LogicException($"Review with id {id} not found");

            Database.Reviews.Delete(review);
            await Database.SaveAsync(token);

            return Mapper.Map<ReviewDTO>(review);
        }

        public async Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid userId, int pageSize, int pageNumber, CancellationToken token)
        {
            var query = Database.Reviews.GetAll().Where(x => x.EmployeeId == userId);

            var total = query.Count();
            query = query.Paged(pageSize, pageNumber);

            return new PagedResult<ReviewDTO>(
                await query.ProjectTo<ReviewDTO>(Mapper.ConfigurationProvider).ToListAsync(token), total, pageSize,
                pageNumber);
        }

        public void Dispose() => Database.Dispose();
    }
}
