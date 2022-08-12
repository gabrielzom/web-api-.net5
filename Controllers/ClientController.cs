using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MonitoriaWEBAPI.Controllers
{
    [Route("Clients")]
    public class ClientController : ControllerBase
    {

        [HttpGet]
        [Route("List")]
        public List<Client> GetClients()
        {
            using var dbcontext = new Data.ApplicationContext();
            var clients = dbcontext.Clients.Where(client => client.ClientId > 0).ToList();

            return clients;
        }

        [HttpGet]
        [Route("List/{clientId}")]
        public Client GetClientById(int clientId)
        {
            using var dbContext = new Data.ApplicationContext();
            var client = dbContext.Clients.FirstOrDefault(client => client.ClientId == clientId);
            return client;
        }


        [HttpGet]
        [Route("List/FilterById/")]
        public List<Client> GetFilteredClientsByIds([Required][FromQuery(Name = "initial")] int initialId, [Required][FromQuery(Name = "include")] Include include, [Required][FromQuery(Name = "final")] int finalId)
        {
            var clients = new List<Client>();
            using var dbContext = new Data.ApplicationContext();
            switch (include)
            {
                case Include.INITIAL:
                    clients = dbContext.Clients.Where(client => (client.ClientId >= initialId && client.ClientId < finalId)).ToList();
                    break;
                case Include.FINAL:
                    clients = dbContext.Clients.Where(client => (client.ClientId > initialId && client.ClientId <= finalId)).ToList();
                    break;
                case Include.BETWEEN:
                    clients = dbContext.Clients.Where(client => (client.ClientId > initialId && client.ClientId < finalId)).ToList();
                    break;
                case Include.ALL: 
                    clients = dbContext.Clients.Where(client => (client.ClientId >= initialId && client.ClientId <= finalId)).ToList();
                    break;
            }
            
            
            return clients;
        }
        
        [HttpPost]
        [Route("Save")]
        public IActionResult CreateClient([FromBody] Client client)
        {
            using var dbContext = new Data.ApplicationContext();
            try
            {
                if (client.SomeParameterIsNull()) 
                {
                    return BadRequest("Parâmetros de inserção invalidos (INSERT).\nMenssagem de erro: Not accept empty parameters.");
                }

                dbContext.Set<Client>().Add(client);
                dbContext.SaveChanges();

                return Ok("Registro incluido com sucesso !");
            }
            catch (Exception e)
            {
                return BadRequest($"Parâmetros de inserção invalidos (INSERT).\nMenssagem de erro: {e.Message}");
            }
        }

        [HttpPut]
        [Route("Update/{clientTd}")]
        public IActionResult UpdateClient(int clientTd, [FromBody] Client newDataOfClient)
        {
            using var dbContext = new Data.ApplicationContext();
            Client clientFound = GetClientById(clientTd);
            if (clientFound == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (UPDATE).");
            }
            else
            {
                clientFound.NameAndSurname = newDataOfClient.NameAndSurname;
                clientFound.RegisterOfPhysicalPerson = newDataOfClient.RegisterOfPhysicalPerson;
                clientFound.DateOfBorn = newDataOfClient.DateOfBorn;

                if (newDataOfClient.Genre == Convert.ToString(Genre.FEMALE) ||
                    newDataOfClient.Genre == Convert.ToString(Genre.MALE)   ||
                    newDataOfClient.Genre == Convert.ToString(Genre.OTHER)) 
                {
                    clientFound.Genre = newDataOfClient.Genre;
                }
                
                else 
                {
                    clientFound.Genre = Convert.ToString(Genre.UNDEFINED);
                }
                
                dbContext.SaveChanges();

                return Ok("Registro atualizado com sucesso!");
            }
        }



        [HttpPatch]
        [Route("Update/NameAndSurname/{clientTd}")]
        public IActionResult UpdateClientNameAndSurname(int clientTd, [FromBody] String nameAndSurname)
        {
            using var dbContext = new Data.ApplicationContext();
            Client clientFound = GetClientById(clientTd);
            if (clientFound == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (UPDATE).");
            }
            else if (nameAndSurname == null) 
            {
                return BadRequest("Nome e sobrenome devem ser preenchidos");
            }
            else
            {
                clientFound.NameAndSurname = nameAndSurname;
                dbContext.SaveChanges();

                return Ok("Registro atualizado com sucesso!");
            }
        }


        [HttpDelete]
        [Route("Delete/{clientTd}")]
        public IActionResult DeleteClient(int clientTd)
        {
            using var dbContext = new Data.ApplicationContext();
            var client = GetClientById(clientTd);
            if (client == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (DELETE).");
            }
            else
            {
                dbContext.Clients.Remove(client);
                dbContext.SaveChanges();

                return Ok("Registro deletado com sucesso!");
            }
        }
    }
}
