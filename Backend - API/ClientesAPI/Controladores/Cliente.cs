using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClientesAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace ClientesAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cliente : ControllerBase
    {
        private readonly ILogger<Cliente> _logger;
        private readonly ICliente _clienteRepositorio;

        [FromQuery(Name = "ID")]
        public string IDQuery { get; set; }

        /*
         * Poderia usar anottations para tornar o campo obrigatório, porém todas as requisições obrigariam a ter os campos citados
         * [Required(ErrorMessage = "Nome é obrigatório.")]
         */
        [FromQuery(Name = "Nome")]
        public string NomeQuery { get; set; }

        [FromQuery(Name = "Nascimento")]
        public string NascimentoQuery { get; set; }

        [FromQuery(Name = "Sexo")]
        public string SexoQuery { get; set; }

        [FromQuery(Name = "CEP")]
        public string CEPQuery { get; set; }

        [FromQuery(Name = "Endereco")]
        public string EnderecoQuery { get; set; }

        [FromQuery(Name = "Numero")]
        public string NumeroQuery { get; set; }

        [FromQuery(Name = "Complemento")]
        public string ComplementoQuery { get; set; }

        [FromQuery(Name = "Bairro")]
        public string BairroQuery { get; set; }

        [FromQuery(Name = "Cidade")]
        public string CidadeQuery { get; set; }

        [FromQuery(Name = "Estado")]
        public string EstadoQuery { get; set; }

        private string ValidarCamposObrigatorios(Modelos.Cliente cliente)
        {
            /*
             * Poderia usar anottations para tornar o campo obrigatório, porém todas as requisições obrigariam a ter os campos citados
             * [Required(ErrorMessage = "Nome é obrigatório.")]
             */
            if (cliente.Nome.Length == 0)
                return "Nome é obrigatório";

            if (cliente.Nascimento == DateTime.MinValue)
                return "Data de nascimento é obrigatória";

            if (cliente.Sexo.Equals(" "))
                return "sexo é obrigatório";

            return "";
        }

        private Modelos.Cliente PopularCliente()
        {
            return new Modelos.Cliente
            {
                ID = IDQuery != null ? Convert.ToInt32(IDQuery) : 0,
                Nome = NomeQuery != null ? NomeQuery : "",
                Nascimento = NascimentoQuery != null ? Convert.ToDateTime(NascimentoQuery) : DateTime.MinValue,
                Sexo = SexoQuery != null ? Convert.ToChar(SexoQuery) : Convert.ToChar(" "),
                CEP = CEPQuery != null ? CEPQuery : "",
                Endereco = EnderecoQuery != null ? EnderecoQuery : "",
                Numero = NumeroQuery != null ? Convert.ToInt32(NumeroQuery) : 0,
                Complemento = ComplementoQuery != null ? ComplementoQuery : "",
                Bairro = BairroQuery != null ? BairroQuery : "",
                Cidade = CidadeQuery != null ? CidadeQuery : "",
                Estado = EstadoQuery != null ? EstadoQuery : ""
            };
        }

        public Cliente(ILogger<Cliente> logger, ICliente clienteRepositorio)
        {
            _logger = logger;
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                Modelos.Cliente cliente = PopularCliente();
                var data = _clienteRepositorio.Listar(cliente);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao listar todos os clientes: " + e.Message);
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Inserir()
        {
            try
            {
                Modelos.Cliente cliente = PopularCliente();

                string erroCampoObrigatorio = ValidarCamposObrigatorios(cliente);
                if (erroCampoObrigatorio.Length > 0)
                    throw new Exception(erroCampoObrigatorio);

                var data = _clienteRepositorio.Inserir(cliente);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao inserir cliente" + e.Message);
                return StatusCode(403, e.Message);
            }

        }

        [HttpPut]
        public IActionResult Editar()
        {
            try
            {
                Modelos.Cliente cliente = PopularCliente();

                string erroCampoObrigatorio = ValidarCamposObrigatorios(cliente);
                if (erroCampoObrigatorio.Length > 0)
                    throw new Exception(erroCampoObrigatorio);

                var data = _clienteRepositorio.Editar(cliente);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao editar cliente" + e.Message);
                return StatusCode(403, e.Message);
            }

        }

        [HttpDelete]
        public IActionResult Deletar()
        {
            try
            {
                Modelos.Cliente cliente = PopularCliente();
                var data = _clienteRepositorio.Deletar(cliente);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao deletar cliente" + e.Message);
                return StatusCode(403, e.Message);
            }

        }
    }
}
