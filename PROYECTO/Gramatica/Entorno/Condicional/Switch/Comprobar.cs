using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Comprobar:Funcion, IEntorno
    {
        public LinkedList<IEntorno> Casos { get; }
        public Dictionary<string, Symbol> Variable { get; }

        public Comprobar()
        {
            this.Casos = null;
        }

        public Comprobar(LinkedList<IEntorno> casos, Dictionary<string, Symbol> variable)
        {
            this.Casos = casos;
            this.Variable = variable;
        }
    }
}
