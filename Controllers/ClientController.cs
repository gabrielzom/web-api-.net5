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
            var clients = dbcontext.Clients.Where(client => client.ClientId > 0).ToList();

            return clients;
        }

        [HttpGet]
        [Route("List/{Id}")]
        public List<Client> ListClients(int Id)
        {
            using var dbcontext = new Data.ApplicationContext();
            var client = dbcontext.Clients.Where(client => client.ClientId == Id).ToList();

            return client;
        }


        [HttpGet]
        [Route("List/BetweenAndIncludingThem/{InitialId}&{FinalId}")]
        public List<Client> ListBetweenAndIncludingThem(int InitialId, int FinalId)
        {
            using var dbcontext = new Data.ApplicationContext();
            var clients = dbcontext.Clients.Where(client => (client.ClientId >= InitialId && client.ClientId <= FinalId)).ToList();

            return clients;
        }


        [HttpGet]
        [Route("List/BetweenAndIncludingOnlyFinal/{InitialId}&{FinalId}")]
        public List<Client> ListBetweenAndIncludingOnlyFinal(int InitialId, int FinalId)
        {
            using var dbcontext = new Data.ApplicationContext();
            var clients = dbcontext.Clients.Where(client => (client.ClientId > InitialId && client.ClientId <= FinalId)).ToList();

            return clients;
        }


        [HttpGet]
        [Route("List/BetweenAndIncludingOnlyInitial/{InitialId}&{FinalId}")]
        public List<Client> ListBetweenAndIncludingOnlyInitial(int InitialId, int FinalId)
        {
            using var dbcontext = new Data.ApplicationContext();
            var clients = dbcontext.Clients.Where(client => (client.ClientId >= InitialId && client.ClientId < FinalId)).ToList();

            return clients;
        }



        [HttpGet]
        [Route("List/BetweenOnly/{InitialId}&{FinalId}")]
        public List<Client> ListBetweenOnly(int InitialId, int FinalId)
        {
            using var dbcontext = new Data.ApplicationContext();
            var clients = dbcontext.Clients.Where(client => (client.ClientId > InitialId && client.ClientId < FinalId)).ToList();

            return clients;
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
        public IActionResult UpdateClient(int Id, [FromBody] Client newDataOfClient)
        {
            using var dbcontext = new Data.ApplicationContext();
            Client clientFound = dbcontext.Clients.FirstOrDefault(client => client.ClientId == Id);
            if (clientFound == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (UPDATE).");
            }
            else
            {
                clientFound.NameAndSurname = newDataOfClient.NameAndSurname;
                clientFound.RegisterOfPhysicalPerson = newDataOfClient.RegisterOfPhysicalPerson;
                clientFound.DateOfBorn = newDataOfClient.DateOfBorn;

                dbcontext.SaveChanges();

                return Ok("Registro atualizado com sucesso!");
            }
        }



        [HttpPatch]
        [Route("Update/NameAndSurname/{Id}")]
        public IActionResult UpdateClientNameAndSurname(int Id, [FromBody] String NameAndSurname)
        {
            using var dbcontext = new Data.ApplicationContext();
            Client clientFound = dbcontext.Clients.FirstOrDefault(client => client.ClientId == Id);
            if (clientFound == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (UPDATE).");
            }
            else if (NameAndSurname == null) {
                return BadRequest("Nome e sobrenome devem ser preenchidos");
            }
            else
            {
                clientFound.NameAndSurname = NameAndSurname;
                dbcontext.SaveChanges();

                return Ok("Registro atualizado com sucesso!");
            }
        }


        [HttpDelete]
        [Route("Delete/{Id}")]
        public IActionResult DeleteClient(int Id)
        {
            using var dbcontext = new Data.ApplicationContext();
            var client = dbcontext.Clients.Find(Id);
            if (client == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (DELETE).");

            }
            else
            {
                dbcontext.Clients.Remove(client);
                dbcontext.SaveChanges();

                return Ok("Registro deletado com sucesso!");
            }
        }
    }
}
