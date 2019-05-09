using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Comprobar:Funcion, IEntorno
    {
        public LinkedList<IEntorno> Casos { get; }
        public ParseTreeNode Variable { get; }

        public Comprobar(ParseTreeNode variable)
        {
            this.Casos = new LinkedList<IEntorno>();
            this.Variable = variable;
        }

        public Comprobar()
        {
            this.Casos = new LinkedList<IEntorno>();
            this.Variable = null;
        }
    }
}
