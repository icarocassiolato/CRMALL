using ClientesAPI.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesAPI.Repositorios
{
    public interface ICliente
    {
        public IEnumerable<Modelos.Cliente> Listar(Modelos.Cliente cliente);
        public int Inserir(Modelos.Cliente cliente);
        public int Editar(Modelos.Cliente cliente);
        public int Deletar(Modelos.Cliente cliente);
    }
}
