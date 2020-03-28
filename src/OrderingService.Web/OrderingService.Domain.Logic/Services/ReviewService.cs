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
    public class ReviewService : AbstractService, IReviewService
    {
        private readonly IRepository<Review> _reviews;
        private readonly IRepository<EmployeeProfile> _employees;
        private readonly IRepository<User> _users;
        public ReviewService(IRepository<Review> reviews, IRepository<EmployeeProfile> employees, 
            IRepository<User> users, IMapper mapper, ISaveProvider saveProvider)
            :base(mapper, saveProvider)
        {
            _employees = employees;
            _reviews = reviews;
            _users = users;
        }
        public async Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto, CancellationToken token)
        {
            var reviewEmployee = await _employees.GetAll()
                .SingleOrDefaultAsync(x => x.Id == reviewDto.EmployeeId, token);
            if (reviewEmployee == null)
                throw new LogicException($"Employee with id {reviewDto.EmployeeId} not found!");
            
            var reviewClient = await _users.GetAll().SingleOrDefaultAsync(x => x.Id == reviewDto.ClientId, token);
            if (reviewClient == null)
                throw new LogicException($"Client with id {reviewDto.ClientId} not found!");
            
            reviewDto.Date = DateTime.Now;
            var review = _mapper.Map<Review>(reviewDto);
            _reviews.Create(review); 
            await _saveProvider.SaveAsync(token);

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> DeleteAsync(int id, CancellationToken token)
        {
            var review = await _reviews.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (review == null)
                throw new LogicException($"Review with id {id} not found");

            _reviews.Delete(review);
            await _saveProvider.SaveAsync(token);

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid userId, int pageSize, int pageNumber, CancellationToken token)
        {
            var query = _reviews.GetAll().Where(x => x.EmployeeId == userId);

            var total = query.Count();
            query = query.Paged(pageSize, pageNumber);

            return new PagedResult<ReviewDTO>(
                await query.ProjectTo<ReviewDTO>(_mapper.ConfigurationProvider).ToListAsync(token), total, pageSize,
                pageNumber);
        }
    }
}
