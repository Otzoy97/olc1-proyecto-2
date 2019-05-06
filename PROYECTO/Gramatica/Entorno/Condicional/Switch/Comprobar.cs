using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Comprobar:Funcion, IEntorno
    {
        public LinkedList<IEntorno> Casos { get; }
        public ParseTreeNode Variable { get; }

        public Comprobar()
        {
            this.Casos = null;
        }

        public Comprobar(LinkedList<IEntorno> casos, ParseTreeNode variable)
        {
            this.Casos = casos;
            this.Variable = variable;
        }
    }
}
