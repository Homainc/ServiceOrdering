using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public IResponse<ReviewDTO> Create(ReviewDTO reviewDto)
        {
            var reviewEmployee = Database.EmployeeProfiles.GetAll().SingleOrDefault(x => x.Id == reviewDto.EmployeeId);
            if (reviewEmployee == null)
            {
                var result = Response<ReviewDTO>.ValidationError($"Employee with id {reviewDto.EmployeeId} not found!");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            var reviewClient = Database.UserProfiles.GetAll().SingleOrDefault(x => x.Id == reviewDto.ClientId);
            if (reviewClient == null)
            {
                var result = Response<ReviewDTO>.ValidationError($"Client with id {reviewDto.ClientId} not found!");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            reviewDto.Date = DateTime.Now;
            var review = Mapper.Map<Review>(reviewDto);
            Database.Reviews.Create(review);
            Database.Save();

            Logger.LogInformation($"Review to user with id {reviewDto.EmployeeId} was added");
            return Response<ReviewDTO>.Success(Mapper.Map<ReviewDTO>(review));

        }

        public IResponse<ReviewDTO> Delete(int id)
        {
            var review = Database.Reviews.GetAll().SingleOrDefault(x => x.Id == id);
            if (review == null)
            {
                var result = Response<ReviewDTO>.NotFound($"Review with id {id} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            Database.Reviews.Delete(review);
            Database.Save();

            Logger.LogInformation($"Review with id {id} was deleted");
            return Response<ReviewDTO>.Success(Mapper.Map<ReviewDTO>(review));
        }

        public IResponse<IEnumerable<ReviewDTO>> GetUserReviews(string userId) =>
            Response<IEnumerable<ReviewDTO>>.Success(
                Database.Reviews.GetAll().Where(x => x.EmployeeId == userId)
                    .ProjectTo<ReviewDTO>(Mapper.ConfigurationProvider));

        public void Dispose() => Database.Dispose();
    }
}
