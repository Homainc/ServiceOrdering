using System;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Extensions;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class ReviewService : AbstractService, IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository,
            IEmployeeRepository employeeRepository, IMapper mapper, ISaveProvider saveProvider)
            : base(mapper, saveProvider)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<ReviewDto> CreateAsync(ReviewCreateDto reviewDto)
        {
            if(!await _userRepository.AnyUserAsync(x => x.Id == reviewDto.ClientId))
                throw new NotFoundLogicException($"Client with id {reviewDto.ClientId} not found!", nameof(reviewDto.ClientId));
            if(!await _employeeRepository.AnyEmployeeAsync(x => x.Id == reviewDto.EmployeeId))
                throw new NotFoundLogicException($"Employee with id {reviewDto.EmployeeId} not found!", nameof(reviewDto.EmployeeId));

            var review = Mapper.Map<Review>(reviewDto);
            review.Date = DateTime.Now;

            _reviewRepository.Create(review);
            await SaveProvider.SaveAsync();

            return Mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> DeleteAsync(int id)
        {
            var review = await GetReviewByIdOrThrowAsync(id);

            _reviewRepository.Delete(review);
            await SaveProvider.SaveAsync();

            return Mapper.Map<ReviewDto>(review);
        }

        public async Task<IPagedResult<ReviewDto>> GetPagedReviewsAsync(Guid employeeId, int pageSize, int pageNumber) =>
            (await _reviewRepository.GetPagedEmployeeReviewsAsync(employeeId, pageSize, pageNumber))
                .ToPagedDto<ReviewDto, Review>(Mapper);

        private async Task<Review> GetReviewByIdOrThrowAsync(int id) =>
            await _reviewRepository.GetByIdOrDefaultAsync(id) ??
            throw new NotFoundLogicException($"Review with id {id} not found!", nameof(id));
    }
}
