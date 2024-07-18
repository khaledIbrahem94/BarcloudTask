using BarcloudTask.Core;
using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;

namespace BarcloudTask.Service.Interface;

public interface IClientsService
{
    Task<IEnumerable<Client>> GetAllAsync(GridParamters gridParamters);
    Task<Client?> GetByIdAsync(int Id);
    Task<int> CreateAsync(ClientDTO clientDTO);
    Task<int> UpdateAsync(ClientDTO clientDTO);
    Task<bool> DeleteAsync(int id);

}
