﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Exceptions;
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

        public async Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto)
        {
            if(!await _userRepository.AnyUserAsync(x => x.Id == reviewDto.ClientId))
                throw new LogicNotFoundException($"Client with id {reviewDto.ClientId} not found!");
            if(!await _employeeRepository.AnyEmployeeAsync(x => x.Id == reviewDto.EmployeeId))
                throw new LogicNotFoundException($"Employee with id {reviewDto.EmployeeId} not found!");

            reviewDto.Date = DateTime.Now;
            var review = _mapper.Map<Review>(reviewDto);
            
            _reviewRepository.Create(review);
            await _saveProvider.SaveAsync();

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> DeleteAsync(int id)
        {
            var review = await GetReviewByIdOrThrowAsync(id);

            _reviewRepository.Delete(review);
            await _saveProvider.SaveAsync();

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid employeeId, int pageSize, int pageNumber) =>
            (await _reviewRepository.GetPagedEmployeeReviewsAsync(employeeId, pageSize, pageNumber))
                .ToPagedDto<ReviewDTO, Review>(_mapper);

        private async Task<Review> GetReviewByIdOrThrowAsync(int id) =>
            await _reviewRepository.GetByIdOrDefaultAsync(id) ??
            throw new LogicNotFoundException($"Review with id {id} not found!");
    }
}
