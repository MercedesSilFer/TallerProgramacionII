using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidades;

namespace Capa_Logica
{
    public class ClassZonaLogica
    {
        //obtener Zona por preventista(nombre_usuario)
        private ClassZona zona = new ClassZona();
        public List<string> ErroresValidacion => zona.ErroresValidacion;

        public int BuscarZonaPorPreventista(string nombre_usuario)
        {   //devolver id_zona
            return zona.BuscarZonaPorPreventista(nombre_usuario);
        }
        public ZONA ObtenerZonaPorId(int id_zona)
        {
            return ClassZona.ObtenerZonaPorId(id_zona);
        }
    }
}
