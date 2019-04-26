using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Clase
    {
        public LinkedList<IEntorno> SubEntornos { get; }
        public Dictionary<string, Symbol> Simbolos { get; }
        //public ParseTreeNode SubArbol { get; }

        public Clase()
        {
            SubEntornos = null;
            Simbolos = null;
            //SubArbol = null;
        }

        public Clase(LinkedList<IEntorno> subentornos, Dictionary<string, Symbol> simbolos)//, ParseTreeNode subArbol)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            //this.SubArbol = subArbol;
        }
        
    }
}
