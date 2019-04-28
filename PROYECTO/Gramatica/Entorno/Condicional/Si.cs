using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional
{
    class Si : Funcion, IEntorno
    {
        public LinkedList<IEntorno> EntornoTrue { get; }
        public Dictionary<string, Symbol> SimbolosTrue { get; }
        public ParseTreeNode SubArbolTrue { get; }

        public LinkedList<IEntorno> EntornoFalse { get; }
        public Dictionary<string, Symbol> SimbolosFalse { get; }
        public ParseTreeNode SubArbolFalse { get; }

        public Dictionary<string, Symbol> Condicion { get; }

        public Si(LinkedList<IEntorno> entornotrue, Dictionary<string, Symbol> simbolostrue, ParseTreeNode subarboltrue, Dictionary<string, Symbol> condicion)
        {
            this.EntornoTrue = entornotrue;
            this.SimbolosTrue = simbolostrue;
            this.SubArbolTrue = subarboltrue;
            this.Condicion = condicion;
        }
        public Si(LinkedList<IEntorno> entornotrue, Dictionary<string, Symbol> simbolostrue, ParseTreeNode subarboltrue, Dictionary<string, Symbol> condicion,
            LinkedList<IEntorno> entornofalse, Dictionary<string, Symbol> simbolosfalse, ParseTreeNode subarbolfalse)
        {
            this.EntornoTrue = entornotrue;
            this.SimbolosTrue = simbolostrue;
            this.SubArbolTrue = subarboltrue;
            this.Condicion = condicion;
            this.EntornoFalse = entornofalse;
            this.SimbolosFalse = simbolosfalse;
            this.SubArbolFalse = subarbolfalse;
        }
    }
}
