using Irony.Parsing;
using System.Collections.Generic;
using PROYECTO.Gramatica.Acciones.Operaciones;

namespace PROYECTO.Gramatica.Entorno.Condicional
{
    class Si : IEntorno
    {
        /// <summary>
        /// Diccionario local
        /// </summary>
        public Dictionary<string, Simbolo> IfSym { get; }
        /// <summary>
        /// Nodo de acciones si verdadero
        /// </summary>
        public ParseTreeNode IfTree { get; }
        /// <summary>
        /// Nodo de acciones si falso
        /// </summary>
        public ParseTreeNode ElseTree { get; }
        /// <summary>
        /// Nodo de condición
        /// </summary>
        public ParseTreeNode Condicion { get; }
        /// <summary>
        /// Entorno inmediato superior
        /// </summary>
        private IEntorno EntornoPadre { get; set; }

        public Si(ParseTreeNode condicion, ParseTreeNode accionesTrue, IEntorno entornoPadre)
        {
            this.IfSym = new Dictionary<string, Simbolo>();
            this.IfTree = accionesTrue;
            this.Condicion = condicion;
            this.EntornoPadre = entornoPadre;
        }
        public Si(ParseTreeNode condicion, ParseTreeNode accionesTrue, ParseTreeNode accionesFalse, IEntorno entornoPadre)
        {
            this.IfSym = new Dictionary<string, Simbolo>();
            this.IfTree = accionesTrue;
            this.Condicion = condicion;
            this.ElseTree = accionesFalse;
            this.EntornoPadre = entornoPadre;
        }
        /// <summary>
        /// Busca un simbolo en el diccionario local o en el diccionario del entorno padre
        /// </summary>
        /// <param name="nombreVar"></param>
        /// <returns></returns>
        public Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.IfSym.ContainsKey(nombreVar) ? this.IfSym[nombreVar] : EntornoPadre.BuscarSimbolo(nombreVar);
        }

        public bool Ejecutar()
        {
            return false;
        }

        /// <summary>
        /// Busca la clase padre
        /// </summary>
        /// <returns></returns>
        public Clase BuscarClasePadre()
        {
            return EntornoPadre.BuscarClasePadre();
        }
        /// <summary>
        /// Busca la función padre
        /// </summary>
        /// <returns></returns>
        public Funcion BuscarFuncionPadre()
        {
            return EntornoPadre.BuscarFuncionPadre();
        }

        /// <summary>
        /// Devuelve si algun padre superior es un loop
        /// </summary>
        /// <returns></returns>
        public bool EsLoop()
        {
            return EntornoPadre.EsLoop();
        }
    }
}
