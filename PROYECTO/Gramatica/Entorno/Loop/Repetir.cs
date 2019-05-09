using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Repetir : Funcion, IEntorno
    {
        public LinkedList<IEntorno> RepeatEnt { get; }
        public Dictionary<string, Simbolo> RepeatSym { get; }
        public ParseTreeNodeList RepeatTree { get; }

        public ParseTreeNodeList Iteracion { get; }

        public Repetir(ParseTreeNodeList subArbol, ParseTreeNodeList iteracion)
        {
            this.RepeatEnt = new LinkedList<IEntorno>();
            this.RepeatSym = new Dictionary<string, Simbolo>();
            this.RepeatTree = subArbol;
            this.Iteracion = iteracion;
        }
    }
}
