using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional
{
    class Si : Funcion, IEntorno
    {
        public LinkedList<IEntorno> EntornoTrue { get; }
        public Dictionary<string, Simbolo> SimbolosTrue { get; }
        public ParseTreeNode SubArbolTrue { get; }

        public LinkedList<IEntorno> EntornoFalse { get; }
        public Dictionary<string, Simbolo> SimbolosFalse { get; }
        public ParseTreeNode SubArbolFalse { get; }

        public ParseTreeNode Condicion { get; }

        public Si(LinkedList<IEntorno> entornotrue, Dictionary<string, Simbolo> simbolostrue, ParseTreeNode subarboltrue, ParseTreeNode condicion)
        {
            this.EntornoTrue = entornotrue;
            this.SimbolosTrue = simbolostrue;
            this.SubArbolTrue = subarboltrue;
            this.Condicion = condicion;
        }
        public Si(LinkedList<IEntorno> entornotrue, Dictionary<string, Simbolo> simbolostrue, ParseTreeNode subarboltrue, ParseTreeNode condicion,
            LinkedList<IEntorno> entornofalse, Dictionary<string, Simbolo> simbolosfalse, ParseTreeNode subarbolfalse)
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
