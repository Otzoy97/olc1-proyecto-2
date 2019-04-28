using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Clase
    { 
        public Dictionary<string, Funcion> SubEntornos { get; }
        public Dictionary<string, Symbol> Simbolos { get; }

        public Clase()
        {
            SubEntornos = null;
            Simbolos = null;
        }

        public Clase(Dictionary<string, Funcion> subentornos, Dictionary<string, Symbol> simbolos)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
        }
        
    }
}
