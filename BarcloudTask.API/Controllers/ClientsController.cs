using BarcloudTask.Core;
using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;
using BarcloudTask.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BarcloudTask.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientsController(IClientsService _clients) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<ResultListDTO<Client>>> GetAll([FromQuery] GridRequestParamters gridParamters)
        {
            var items = await _clients.GetAllAsync(gridParamters);
            return Ok(items);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ClientDTO>> GetById(int Id)
        {
            var items = await _clients.GetByIdAsync(Id);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<SaveAction>> Create(ClientDTO client)
        {
            var saved = await _clients.CreateAsync(client);
            return Ok(saved);
        }

        [HttpPut]
        public async Task<ActionResult<SaveAction>> Update(ClientDTO entity)
        {
            var saved = await _clients.UpdateAsync(entity);
            return Ok(saved);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SaveAction>> Delete(int id)
        {
            var saved = await _clients.DeleteAsync(id);
            return Ok(saved);
        }
    }
}
