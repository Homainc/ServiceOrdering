using System;
using System.Linq;
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
        private readonly IEmployeeRepository _employees;
        private readonly IUserRepository _users;
        public ReviewService(IRepository<Review> reviews, IEmployeeRepository employees, 
            IUserRepository users, IMapper mapper, ISaveProvider saveProvider)
            :base(mapper, saveProvider)
        {
            _employees = employees;
            _reviews = reviews;
            _users = users;
        }
        public async Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto)
        {
            if (!(await  _employees.AnyEmployeeAsync(x => x.Id == reviewDto.EmployeeId)))
                throw new LogicException($"Employee with id {reviewDto.EmployeeId} not found!");
            
            if (!(await _users.AnyUserAsync(x => x.Id == reviewDto.ClientId)))
                throw new LogicException($"Client with id {reviewDto.ClientId} not found!");
            
            reviewDto.Date = DateTime.Now;
            var review = _mapper.Map<Review>(reviewDto);
            _reviews.Create(review); 
            await _saveProvider.SaveAsync();

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> DeleteAsync(int id)
        {
            // TODO: Remove EF Core dependency
            var review = await _reviews.GetAll().SingleOrDefaultAsync(x => x.Id == id);
            if (review == null)
                throw new LogicNotFoundException($"Review with id {id} not found");

            _reviews.Delete(review);
            await _saveProvider.SaveAsync();

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid userId, int pageSize, int pageNumber)
        {
            var query = _reviews.GetAll().Where(x => x.EmployeeId == userId);

            var total = query.Count();
            query = query.Paged(pageSize, pageNumber);

            // TODO: Remove EF Core dependency
            return new PagedResult<ReviewDTO>(
                await query.ProjectTo<ReviewDTO>(_mapper.ConfigurationProvider).ToListAsync(), total, pageSize,
                pageNumber);
        }
    }
}
