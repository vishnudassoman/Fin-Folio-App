using FinFolio.PortFolio.DTO;

namespace FinFolio.PortFolioCore.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(int id);
        Task<UserDto> GetUserAsync(Guid oid);
        Task<UserDto> AddUserAsync(UserDto userDto);
        Task<UserDto> UpdateUserAsync(UserDto userDto);
    }
}
