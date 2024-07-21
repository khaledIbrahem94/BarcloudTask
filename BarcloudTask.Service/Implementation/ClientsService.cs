using AutoMapper;
using BarcloudTask.Core;
using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;
using BarcloudTask.Service.Interface;
using System.Linq.Expressions;

namespace BarcloudTask.Service.Implementation;

public class ClientsService(IRepository<Client> _repository, ICommonService _commonService, IMapper _mapper) : IClientsService
{

    public async Task<ResultListDTO<Client>> GetAllAsync(GridRequestParamters gridParamters)
    {
        Expression<Func<Client, bool>> where = x => true;

        if (!string.IsNullOrEmpty(gridParamters.Filter))
            where = x => x.FirstName.Contains(gridParamters.Filter) || x.LastName.Contains(gridParamters.Filter) ||
            x.Email.Contains(gridParamters.Filter) || (string.IsNullOrEmpty(x.PhoneNumber) ? true : x.PhoneNumber.Contains(gridParamters.Filter));

        var (total, res) = await _repository.GetAllByCondition(where, gridParamters);
        return _commonService.ResultList<Client>(total, res, gridParamters);
    }

    public async Task<Client?> GetByIdAsync(int Id)
    {
        return await _repository.GetByIdAsync(Id);
    }

    public async Task<SaveAction> CreateAsync(ClientDTO clientDTO)
    {
        await EmailExists(clientDTO.Email);
        Client client = _mapper.Map<Client>(clientDTO);
        await _repository.InsertAsync(client);
        return _commonService.Success("Inserted");
    }

    public async Task<SaveAction> UpdateAsync(ClientDTO clientDTO)
    {
        await EmailExists(clientDTO.Email);
        Client client = _mapper.Map<Client>(clientDTO);
        await _repository.UpdateAsync(client);
        return _commonService.Success("Deleted");
    }

    public async Task<SaveAction> DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        return _commonService.Success("Deleted");

    }

    async Task EmailExists(string email)
    {
        if ((await _repository.GetFirstByCondition(x => x.Email.Trim() == email.Trim())) != null)
        {
            throw new Exception("Email already exists");
        }
    }
}
