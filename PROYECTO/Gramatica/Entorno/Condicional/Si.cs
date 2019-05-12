using Irony.Parsing;
using System.Collections.Generic;
using PROYECTO.Gramatica.Acciones.Operaciones;

namespace PROYECTO.Gramatica.Entorno.Condicional
{
    class Si : Funcion, IEntorno
    {
        public LinkedList<IEntorno> IfEnt { get; }
        public Dictionary<string, Simbolo> IfSym { get; }
        public ParseTreeNode IfTree { get; }
        public ParseTreeNode ElseTree { get; }

        public ParseTreeNode Condicion { get; }

        public Si(ParseTreeNode condicion, ParseTreeNode accionesTrue)
        {
            this.IfEnt = new LinkedList<IEntorno>();
            this.IfSym = new Dictionary<string, Simbolo>();
            this.IfTree = accionesTrue;
            this.Condicion = condicion;
        }
        public Si(ParseTreeNode condicion, ParseTreeNode accionesTrue, ParseTreeNode accionesFalse)
        {
            this.IfEnt = new LinkedList<IEntorno>();
            this.IfSym = new Dictionary<string, Simbolo>();
            this.IfTree = accionesTrue;
            this.Condicion = condicion;
            this.ElseTree = accionesFalse;
        }

        public new Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.IfSym.ContainsKey(nombreVar) ? this.IfSym[nombreVar] : base.BuscarSimbolo(nombreVar);
        }

        public new void Ejecutar()
        {
            //Opera la condición del if
            Simbolo varBool = new Operar(this).Interpretar(Condicion);
            //Verifica que el valor devuelto sea un BOOLEANO
            if (varBool.TipoDato == Tipo.BOOLEAN && varBool.Dato != null)
            {

            }
            else
            {
                //Solo se aceptan valore booleanos como condicion en un if
            }
        }
    }
}
