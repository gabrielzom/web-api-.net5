using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoriaWEBAPI.Controllers
{
    [Route("Clientes")]
    public class ClienteController : ControllerBase
    {

        [HttpGet]
        [Route("Listar/All")]
        public List<Cliente> ListarClientes()
        {
            using var dbcontext = new Data.ApplicationContext();
            var clientes = dbcontext.Clientes.Where(cliente => cliente.Id > 0).ToList();

            return clientes;
        }

        [HttpGet]
        [Route("Listar/{Id}")]
        public List<Cliente> ListarCliente(int Id)
        {
            using var dbcontext = new Data.ApplicationContext();
            var cliente = dbcontext.Clientes.Where(cliente => cliente.Id == Id).ToList();

            return cliente;
        }


        [HttpPost]
        [Route("Incluir")]
        public IActionResult IncluirCliente([FromBody] List<Cliente> clientes)
        {
            using var dbcontext = new Data.ApplicationContext();
            try
            {
                foreach (var cliente in clientes)
                {
                    if (cliente.Nome.Equals("") || cliente.Nascimento.Equals(new DateTime()) || cliente.CPF.Equals("")) {
                        return BadRequest("Parâmetros de inserção invalidos (INSERT).\nMenssagem de erro: Not accept empty parameters.");
                    }
                }

                dbcontext.Set<Cliente>().AddRange(clientes);
                dbcontext.SaveChanges();

                return Ok("Registro incluido com sucesso !");
            }
            catch (Exception e)
            {
                return BadRequest($"Parâmetros de inserção invalidos (INSERT).\nMenssagem de erro: {e.Message}");
            }
        }

        [HttpPut]
        [Route("Atualizar/{Id}")]
        public IActionResult AtualizarCliente(int Id, [FromBody] Cliente novosDadosCliente)
        {
            using var dbcontext = new Data.ApplicationContext();
            Cliente clienteEncontrado = dbcontext.Clientes.FirstOrDefault(cliente => cliente.Id == Id);
            if (clienteEncontrado == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (UPDATE).");

            }
            else
            {
                clienteEncontrado.Nome = novosDadosCliente.Nome;
                clienteEncontrado.CPF = novosDadosCliente.CPF;
                clienteEncontrado.Nascimento = novosDadosCliente.Nascimento;

                dbcontext.SaveChanges();

                return Ok("Registro atualizado com sucesso!");
            }
        }

        [HttpDelete]
        [Route("Deletar/{Id}")]
        public IActionResult DeletarCliente(int Id)
        {
            using var dbcontext = new Data.ApplicationContext();
            var cliente = dbcontext.Clientes.Find(Id);
            if (cliente == null)
            {
                return NotFound("Não encontrei nenhum registro com esse Id (DELETE).");

            }
            else
            {

                dbcontext.Clientes.Remove(cliente);
                dbcontext.SaveChanges();

                return Ok("Registro deletado com sucesso!");
            }
        }
    }
}
