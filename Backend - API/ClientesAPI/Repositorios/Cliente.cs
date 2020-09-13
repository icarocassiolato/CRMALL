using ClientesAPI.Banco;
using ClientesAPI.Modelos;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesAPI.Repositorios
{
    public sealed class Cliente : ICliente
    {
        private readonly string _stringConexao;
        private ContextoBD _contexto;

        public Cliente(IConfiguration configuration)
        {
            _stringConexao = configuration.GetConnectionString("DefaultConnection");
            _contexto = new ContextoBD(_stringConexao);
        }

        private List<Modelos.Cliente> PopularListaClientes(MySqlDataReader dataReader)
        {
            List<Modelos.Cliente> listaDeRetorno = new List<Modelos.Cliente>();
            Modelos.Cliente cliente;
            while (dataReader.Read())
            {
                cliente = new Modelos.Cliente();

                cliente.ID = Convert.ToInt64(dataReader["ID"]);
                cliente.Nome = Convert.ToString(dataReader["Nome"]);             
                cliente.Nascimento = Convert.ToDateTime(dataReader["Nascimento"]);
                cliente.Sexo = Convert.ToChar(dataReader["Sexo"]);
                cliente.CEP = Convert.ToString(dataReader["CEP"]);
                cliente.Endereco = Convert.ToString(dataReader["Endereco"]);
                cliente.Numero = Convert.ToInt32(dataReader["Numero"]);
                cliente.Complemento = Convert.ToString(dataReader["Complemento"]);
                cliente.Bairro = Convert.ToString(dataReader["Bairro"]);
                cliente.Cidade = Convert.ToString(dataReader["Cidade"]);
                cliente.Estado = Convert.ToString(dataReader["Estado"]);

                listaDeRetorno.Add(cliente);
            }
            
            return listaDeRetorno;
        }

        public IEnumerable<Modelos.Cliente> Listar(Modelos.Cliente cliente)
        {
            MySqlDataReader dataReader = _contexto.Consultar(cliente);

            return PopularListaClientes(dataReader);
        }

        public int Inserir(Modelos.Cliente cliente)
        {
            MySqlDataReader dataReader = _contexto.Inserir(cliente);

            return dataReader.RecordsAffected;
        }

        public int Editar(Modelos.Cliente cliente)
        {
            MySqlDataReader dataReader = _contexto.Editar(cliente);

            return dataReader.RecordsAffected;
        }
        
        public int Deletar(Modelos.Cliente cliente)
        {
            MySqlDataReader dataReader = _contexto.Deletar(cliente);

            return dataReader.RecordsAffected;
        }
    };
}
