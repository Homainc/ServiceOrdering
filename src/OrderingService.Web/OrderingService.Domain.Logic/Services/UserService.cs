using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrderingService.Data.Code.Constants;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Constants;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPictureService _pictureService;

        public UserService(IPictureService pictureService, IUserRepository userRepository, ISaveProvider saveProvider, IMapper mapper,
            IRoleRepository roleRepository, IPasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator)
            : base(mapper, saveProvider)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _pictureService = pictureService;
        }

        public async Task<UserAuthDto> CreateAsync(UserCreateDto userDto)
        {
            if (await _userRepository.AnyUserAsync(x => x.Email == userDto.Email))
                throw new FieldLogicException($"User with email {userDto.Email} already exists!",
                    nameof(userDto.Email));

            var user = Mapper.Map<User>(userDto);
            user.HashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.RoleId = await _roleRepository.GetRoleIdByNameAsync(userDto.Role);

            _userRepository.Create(user);
            await SaveProvider.SaveAsync();

            await _pictureService.ChangeImageTagAsync(user.ImagePublicId, CloudinaryTagDefaults.Employee);
            await _pictureService.DeleteTemporaryImagesAsync();

            return Mapper.Map<UserAuthDto>(user);
        }

        public async Task<UserAuthDto> AuthenticateAsync(string email, string password)
        {
            var user = await GetUserByEmailOrThrowAsync(email);

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);
            if (result == PasswordVerificationResult.Failed)
                throw new FieldLogicException("Incorrect email or password", nameof(password));

            var userAuthDto = Mapper.Map<UserAuthDto>(user);
            userAuthDto.Token = _tokenGenerator.GenerateUserToken(user);
            return userAuthDto;
        }

        public async Task<UserAuthDto> SignUpAsync(UserCreateDto userDto)
        {
            userDto.Role = RoleDefaults.User;
            await CreateAsync(userDto);
            return await AuthenticateAsync(userDto.Email, userDto.Password);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id) =>
            Mapper.Map<UserDto>(await GetUserByIdOrThrowAsync(id));

        public async Task<UserDto> UpdateProfileAsync(UserDto userDto)
        {
            var user = await GetUserByIdOrThrowAsync(userDto.Id);
            var oldImagePublicId = user.ImagePublicId;

            Mapper.Map(userDto, user);
            _userRepository.Update(user);
            await SaveProvider.SaveAsync();

            if (user.ImagePublicId != oldImagePublicId)
            {
                await _pictureService.DeleteImageAsync(oldImagePublicId);
                await _pictureService.ChangeImageTagAsync(user.ImagePublicId, CloudinaryTagDefaults.Employee);
            }

            await _pictureService.DeleteTemporaryImagesAsync();

            return userDto;
        }

        private async Task<User> GetUserByIdOrThrowAsync(Guid id) =>
            await _userRepository.EagerSingleOrDefaultAsync(x => x.Id == id) ??
            throw new NotFoundLogicException($"User with id {id} not found!", nameof(id));

        private async Task<User> GetUserByEmailOrThrowAsync(string email) =>
            await _userRepository.EagerSingleOrDefaultAsync(x => x.Email == email) ??
            throw new NotFoundLogicException($"User with email {email} not found!", nameof(email));
    }
}
