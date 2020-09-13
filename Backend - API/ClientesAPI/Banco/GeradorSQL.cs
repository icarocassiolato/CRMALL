using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesAPI.Banco
{
    public class GeradorSQL
    {
        public string ConsultaSQL(object objeto)
        {
            var id = 
              from property in objeto.GetType().GetProperties()
              where property.Name == "ID" 
              select property.GetValue(objeto);

            return "select * from " + objeto.GetType().Name +
                ((id != null) && (id.ElementAt(0).ToString() != "0") ? " where id = " + id.ElementAt(0).ToString() : "");
        }

        public string InsertSQL(object objeto)
        {
            var properties = objeto.GetType().GetProperties();

            string nomesCampos = "";
            string valoresCampos = "";

            foreach (var property in properties)
            {
                if (property.Name == "ID")
                    continue;

                nomesCampos += property.Name + ", ";

                if (property.PropertyType.Name.Equals("DateTime"))
                    valoresCampos += "'" + Convert.ToDateTime(property.GetValue(objeto)).ToString("yyyy-MM-dd") + "', ";
                else
                    valoresCampos += "'" + property.GetValue(objeto).ToString() + "', ";
            }

            nomesCampos = nomesCampos.Remove(nomesCampos.Length - 2);
            valoresCampos = valoresCampos.Remove(valoresCampos.Length - 2);

            return "insert into " + objeto.GetType().Name + " (" + nomesCampos + ") values (" + valoresCampos + ")";
        }

        public string EditSQL(object objeto)
        {
            var properties = objeto.GetType().GetProperties();

            string campos = "";
            string id = "";

            foreach (var property in properties)
            {
                if (property.Name == "ID")
                {
                    id = property.GetValue(objeto).ToString();
                    continue;
                }

                if ((property.GetValue(objeto).ToString().Length == 0) ||
                   (property.PropertyType.Name.Equals("DateTime") && (property.GetValue(objeto).ToString() == DateTime.MinValue.ToString())) ||
                   (property.PropertyType.Name.Equals("Int32") && (property.GetValue(objeto).ToString() == "0")) ||
                   (property.PropertyType.Name.Equals("Char") && (property.GetValue(objeto).ToString() == " ")))
                    continue;

                campos += property.Name + " = ";

                if (property.PropertyType.Name.Equals("DateTime"))
                    campos += "'" + Convert.ToDateTime(property.GetValue(objeto)).ToString("yyyy-MM-dd") + "', ";
                else
                    campos += "'" + property.GetValue(objeto).ToString() + "', ";
            }

            campos = campos.Remove(campos.Length - 2);

            return "update " + objeto.GetType().Name + " set " + campos + " where id = " + id;
        }

        public string DeleteSQL(object objeto)
        {
            var id =
              from property in objeto.GetType().GetProperties()
              where property.Name == "ID"
              select property.GetValue(objeto);

            return "delete from " + objeto.GetType().Name + " where id = " + id.ElementAt(0).ToString();
        }
    }
}
