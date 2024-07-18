using AutoMapper;
using BarcloudTask.Core;
using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;
using BarcloudTask.Service.Interface;

namespace BarcloudTask.Service.Implementation;

public class ClientsService(IRepository<Client> _repository, IMapper _mapper) : IClientsService
{

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Client?> GetByIdAsync(int Id)
    {
        return await _repository.GetByIdAsync(Id);
    }

    public async Task<int> CreateAsync(ClientDTO clientDTO)
    {
        Client client = _mapper.Map<Client>(clientDTO);
        await _repository.InsertAsync(client);
        return client.Id;
    }

    public async Task<int> UpdateAsync(ClientDTO clientDTO)
    {
        Client client = _mapper.Map<Client>(clientDTO);
        await _repository.UpdateAsync(client);
        return client.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id) > 0;
    }

    public Task<IEnumerable<Client>> GetAllAsync(GridParamters gridParamters)
    {
        throw new NotImplementedException();
    }
}
