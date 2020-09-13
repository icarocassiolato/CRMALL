using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesAPI.Modelos
{
    public class Cliente
    {
        public long ID { get; set; }
        
        public string Nome { get; set; }
        
        public DateTime Nascimento { get; set; }
        
        public char Sexo { get; set; }
        
        public string CEP { get; set; }
        
        public string Endereco { get; set; }
        
        public int Numero { get; set; }
        
        public string Complemento { get; set; }
        
        public string Bairro { get; set; }

/* IMPORTANTE!!!
 * O correto seria colocar os campos abaixo em uma tabela/entidade separada afim de normalizar o banco de dados,
 * porém, no enunciado do exercício pede para que fique na entidade Cliente
 */
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
