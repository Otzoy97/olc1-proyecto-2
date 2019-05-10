using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Funcion : Clase, IEntorno
    {
        //Subentornos
        public LinkedList<IEntorno> FuncEnt { get; }
        //Simbolos
        public Dictionary<string, Simbolo> FuncSym { get; }
        //Subarbol de acciones
        public ParseTreeNode FuncTree { get; set; }
        //Determina si la función es privada
        public bool EsPrivado { get; set; }
        //Determina el tipo de dato que debe devolver
        //public Tipo DataType { get; set; }
        //Especifica el simbolo a retornar
        public Simbolo ReturnData { get; set; }
        //Es sobrecarga
        public bool Override { get; set; }
        //Establece la lista de parametros
        public ParseTreeNode ParTree { get; set; }

        public Funcion()
        {
            FuncEnt = new LinkedList<IEntorno>();
            FuncSym = new Dictionary<string, Simbolo>();
            ReturnData = new Simbolo();
            //DataType = Tipo.VOID;
            EsPrivado = false;            
            FuncTree = null;
            ParTree = null;           
            Override = false;
        }
        /*
        public Funcion(bool esprivado, LinkedList<IEntorno> subentornos, ParseTreeNode subArbol)
        {
            this.FuncEnt = subentornos;
            this.FuncSym = simbolos;
            this.FuncTree = subArbol;
            this.EsPrivado = esprivado;
        }*/
    }
}
