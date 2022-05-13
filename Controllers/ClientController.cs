using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoriaWEBAPI.Controllers
{
    [Route("Clients")]
    public class ClientController : ControllerBase
    {

        [HttpGet]
        [Route("List")]
        public List<Client> ListClients()
        {
            using var dbcontext = new Data.ApplicationContext();
            var clientes = dbcontext.Clients.Where(cliente => cliente.ClientId > 0).ToList();

            return clientes;
        }

        [HttpGet]
        [Route("List/{Id}")]
        public List<Client> ListClients(int Id)
        {
            using var dbcontext = new Data.ApplicationContext();
            var cliente = dbcontext.Clients.Where(cliente => cliente.ClientId == Id).ToList();

            return cliente;
        }


        [HttpPost]
        [Route("Save")]
        public IActionResult SaveClient([FromBody] List<Client> clients)
        {
            using var dbcontext = new Data.ApplicationContext();
            try
            {
                foreach (var client in clients)
                {
                    if (client.NameAndSurname.Equals("") || client.DateOfBorn.Equals(new DateTime()) || client.RegisterOfPhysicalPerson.Equals("")) {
                        return BadRequest("Parâmetros de inserção invalidos (INSERT).\nMenssagem de erro: Not accept empty parameters.");
                    }
                }

                dbcontext.Set<Client>().AddRange(clients);
                dbcontext.SaveChanges();

                return Ok("Registro incluido com sucesso !");
            }
            catch (Exception e)
            {
                return BadRequest($"Parâmetros de inserção invalidos (INSERT).\nMenssagem de erro: {e.Message}");
            }
        }

        [HttpPut]
        [Route("Update/{Id}")]
        public IActionResult UpdateClient(int Id, [FromBody] Client novosDadosCliente)
        {
            using var dbcontext = new Data.ApplicationContext();
            Client clienteEncontrado = dbcontext.Clients.FirstOrDefault(client => client.ClientId == Id);
            if (clienteEncontrado == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (UPDATE).");

            }
            else
            {
                clienteEncontrado.NameAndSurname = novosDadosCliente.NameAndSurname;
                clienteEncontrado.RegisterOfPhysicalPerson = novosDadosCliente.RegisterOfPhysicalPerson;
                clienteEncontrado.DateOfBorn = novosDadosCliente.DateOfBorn;

                dbcontext.SaveChanges();

                return Ok("Registro atualizado com sucesso!");
            }
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public IActionResult DeleteClient(int Id)
        {
            using var dbcontext = new Data.ApplicationContext();
            var cliente = dbcontext.Clients.Find(Id);
            if (cliente == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (DELETE).");

            }
            else
            {
                dbcontext.Clients.Remove(cliente);
                dbcontext.SaveChanges();

                return Ok("Registro deletado com sucesso!");
            }
        }
    }
}
