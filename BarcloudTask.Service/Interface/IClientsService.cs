using BarcloudTask.Core;
using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;

namespace BarcloudTask.Service.Interface;

public interface IClientsService
{
    Task<ResultListDTO<Client>> GetAllAsync(GridRequestParamters gridParamters);
    Task<Client?> GetByIdAsync(int Id);
    Task<SaveAction> CreateAsync(ClientDTO clientDTO);
    Task<SaveAction> UpdateAsync(ClientDTO clientDTO);
    Task<SaveAction> DeleteAsync(int id);

}
