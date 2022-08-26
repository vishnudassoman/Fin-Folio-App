using FinFolio.PortFolio.DTO;
using FinFolio.PortFolioCore.Interfaces;
using FinFolio.PortFolioRepository.Entities;
using FinFolio.PortFolioRepository.Interfaces;

namespace FinFolio.PortFolioCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            try
            {
                User newUser = new User
                {//TODO: Add Automapper
                    Id = userDto.Id,
                    ObjectIdentifier = userDto.ObjectIdentifier
                };

                newUser = await _userRepository.AddUserAsync(newUser);
                if (newUser != null)
                {
                    return new UserDto
                    {
                        Id = newUser.Id,
                        ObjectIdentifier = newUser.ObjectIdentifier
                    };
                }
            }
            catch (Exception)
            {//TODO: Implement logging 
                throw;
            }
            return null;
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            try
            {
                User existingUser = await _userRepository.GetUserAsync(id);
                if (existingUser != null)
                {
                    return new UserDto
                    {//TODO: Use automapper
                        Id = existingUser.Id,
                        ObjectIdentifier = existingUser.ObjectIdentifier
                    };
                }
            }
            catch (Exception)
            {//TODO: log exception
                throw;
            }
            return null;
        }

        public async Task<UserDto> GetUserAsync(Guid oid)
        {
            try
            {
                User existingUser = await _userRepository.GetUserAsync(oid);
                if (existingUser != null)
                {
                    return new UserDto
                    {//TODO: Use automapper
                        Id = existingUser.Id,
                        ObjectIdentifier = existingUser.ObjectIdentifier
                    };
                }
            }
            catch (Exception)
            {//TODO: log exception
                throw;
            }
            return null;
        }

        public async Task<UserDto> UpdateUserAsync(UserDto userDto)
        {
            try
            {
                User existingUser = new User
                {//TODO: Use Automapper
                    Id = userDto.Id,
                    ObjectIdentifier = userDto.ObjectIdentifier
                };
                existingUser = await _userRepository.UpdateUserAsync(existingUser);
            }
            catch (Exception)
            {
                throw;
            }
            return userDto;
        }
    }
}
