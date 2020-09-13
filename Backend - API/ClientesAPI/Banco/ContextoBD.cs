using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Linq;

namespace ClientesAPI.Banco
{
    public enum Operacao { OpInsert, OpEdit, OpDelete, OpQuery }

    public class ContextoBD : DbContext
    {
        private string _stringConexao;
        private Operacao _operacao;

        public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
        {
        }

        public ContextoBD(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        private MySqlDataReader Executar(object objeto)
        {
            GeradorSQL geradorSQL = new GeradorSQL();
            MySqlConnection Conexao = new MySqlConnection(_stringConexao);
            MySqlCommand query = new MySqlCommand();
            query.Connection = Conexao;
            
            switch (_operacao)
            {
                case Operacao.OpInsert:
                    query.CommandText = geradorSQL.InsertSQL(objeto);
                    break;
                case Operacao.OpEdit:
                    query.CommandText = geradorSQL.EditSQL(objeto);
                    break;
                case Operacao.OpDelete:
                    query.CommandText = geradorSQL.DeleteSQL(objeto);
                    break;
                case Operacao.OpQuery:
                    query.CommandText = geradorSQL.ConsultaSQL(objeto);
                    break;
            }
            
            Conexao.Open();
            MySqlDataReader resultado = query.ExecuteReader();

            return resultado;
        }

        public MySqlDataReader Consultar(object objeto)
        {
            _operacao = Operacao.OpQuery;

            return Executar(objeto);
        }
        public MySqlDataReader Inserir(object objeto)
        {
            _operacao = Operacao.OpInsert;

            return Executar(objeto);
        }

        public MySqlDataReader Editar(object objeto)
        {
            _operacao = Operacao.OpEdit;

            return Executar(objeto);
        }

        public MySqlDataReader Deletar(object objeto)
        {
            _operacao = Operacao.OpDelete;

            return Executar(objeto);
        }
    }
}
