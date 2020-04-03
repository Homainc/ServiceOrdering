using System.Linq.Expressions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Data.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRepository<User> _users;
        private readonly IRepository<Role> _roles;
        private readonly IRepository<EmployeeProfile> _employees;
        private readonly IRepository<ServiceType> _serviceTypes;

        public UserService(IRepository<User> users, ISaveProvider saveProvider, IMapper mapper,
            IRepository<Role> roles, IPasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator,
            IRepository<EmployeeProfile> employees, IRepository<ServiceType> serviceTypes)
            : base(mapper, saveProvider)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _users = users;
            _roles = roles;
            _employees = employees;
            _serviceTypes = serviceTypes;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await _users.GetAll()
                .SingleOrDefaultAsync(x => x.Email == userDto.Email, token);
            if (user != null)
                throw new LogicException("User with this email already exists");

            user = _mapper.Map<User>(userDto);
            user.HashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.RoleId = (await _roles.GetAll().SingleOrDefaultAsync(x => x.Name == userDto.Role, token)).Id;
            _users.Create(user);

            await _saveProvider.SaveAsync(token);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AuthenticateAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await FindUserOrThrowAsync(x => x.Email == userDto.Email, token, "Incorrect email or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new LogicException("Incorrect email or password");

            userDto = _mapper.Map<UserDTO>(user);
            userDto.Token = _tokenGenerator.GenerateUserToken(user);
            return userDto;
        }

        public async Task<UserDTO> SignUpAsync(UserDTO userDto, CancellationToken token)
        {
            userDto.Role = "user";
            await CreateAsync(userDto, token);
            return await AuthenticateAsync(userDto, token);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken token) => 
            _mapper.Map<UserDTO>(await FindUserOrThrowAsync(x => x.Id == id, token));

        public async Task<UserDTO> UpdateProfileAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await FindUserOrThrowAsync(x => x.Id == userDto.Id, token);

            _mapper.Map(userDto, user);
            
            await _saveProvider.SaveAsync(token);
            return userDto;
        }

        private async Task<User> FindUserOrThrowAsync(Expression<Func<User, bool>> selector, CancellationToken token = default, string customExceptionMessage = null)
        {
            var user = await (
                from u in _users.GetAll()
                join r in _roles.GetAll() on u.RoleId equals r.Id
                join e in _employees.GetAll() on u.Id equals e.UserId into eGrouping
                from e in eGrouping.DefaultIfEmpty() 
                join st in _serviceTypes.GetAll() on e.ServiceTypeId equals st.Id into stGrouping
                from st in stGrouping.DefaultIfEmpty()
                select new User
                {
                    Email = u.Email,
                    EmployeeProfile = e != null ? new EmployeeProfile {
                        Id = e.Id,
                        ServiceType = st,
                        ServiceCost = e.ServiceCost,
                        Description = e.Description,
                        ServiceTypeId = e.ServiceTypeId
                    }: null,
                    FirstName = u.FirstName,
                    Id = u.Id,
                    ImageUrl = u.ImageUrl,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Role = r,
                    RoleId = r.Id,
                    HashedPassword = u.HashedPassword
                })
                .Where(selector)
                .FirstOrDefaultAsync(token);
            if (user == null)
                throw new LogicNotFoundException(customExceptionMessage ?? $"User not found");
            return user;
        }
    }
}
