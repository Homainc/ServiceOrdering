using System;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Interfaces;

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

        public async Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid employeeId, int pageSize, int pageNumber) =>
            (await _reviews.GetPagedEmployeeReviewsAsync(employeeId, pageSize, pageNumber))
                .ToPagedDto<ReviewDTO, Review>(_mapper);

        public async Task<bool> AnyReviewByIdAsync(int id) =>
            await _reviews.AnyReviewByIdAsync(id);
    }
}
