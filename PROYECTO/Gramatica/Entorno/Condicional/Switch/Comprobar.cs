using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Comprobar:Funcion, IEntorno
    {
        public LinkedList<IEntorno> Casos { get; }
        public ParseTreeNodeList Variable { get; }

        public Comprobar(ParseTreeNodeList variable)
        {
            this.Casos = new LinkedList<IEntorno>();
            this.Variable = variable;
        }
    }
}
