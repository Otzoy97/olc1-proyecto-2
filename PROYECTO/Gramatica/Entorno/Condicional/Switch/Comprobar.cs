using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Comprobar: IEntorno
    {
        public ParseTreeNode Variable { get; }
        private IEntorno EntornoPadre { get; set; }
        public ParseTreeNode CaseNode { get; }

        public Comprobar()
        {

        }

        public Comprobar(ParseTreeNode variable, ParseTreeNode casos)
        {
            this.CaseNode = casos;
            this.Variable = variable;
        }

        public Simbolo BuscarSimbolo(string nombreVar)
        {
            return EntornoPadre.BuscarSimbolo(nombreVar);
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
