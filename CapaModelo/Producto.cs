using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Producto
    {
        public int id_Producto { get; set; }

        public string Nombre_Producto { get; set; }

        public string Descripcion { get; set; }
        public Boolean Estado { get; set; }

        public int id_Categoria { get; set; }   
        public Categoria objCategoria { get; set; } 
    }
}
