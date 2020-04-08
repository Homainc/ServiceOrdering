using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Domain.Logic.Helpers;

namespace OrderingService.Domain.Logic.Services
{
    public class ReviewService : AbstractService, IReviewService
    {
        private readonly IReviewRepository _reviews;

        public ReviewService(IReviewRepository reviews, IMapper mapper, ISaveProvider saveProvider) 
            : base(mapper, saveProvider) => _reviews = reviews;
        
        public async Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto)
        {
            reviewDto.Date = DateTime.Now;
            var review = _mapper.Map<Review>(reviewDto);
            
            _reviews.Create(review);
            await _saveProvider.SaveAsync();

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> DeleteAsync(int id)
        {
            var review = await _reviews.GetByIdAsync(id);

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

        public async Task<bool> AnyReviewByIdAsync(int id) =>
            await _reviews.AnyReviewByIdAsync(id);
    }
}
