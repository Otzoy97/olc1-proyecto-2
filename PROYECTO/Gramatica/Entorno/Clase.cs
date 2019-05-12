using System.Collections.Generic;
using Irony.Parsing;

namespace PROYECTO.Gramatica.Entorno
{
    class Clase : IEntorno
    { 
        /// <summary>
        /// Almacenará todo los subentorno (funciones/metodos) de la clase
        /// </summary>
        public Dictionary<string, Funcion> ClaseEnt { get; set; }
        /// <summary>
        /// Almacenará todo tipo de dato que se declare (int,string,bool,char,double,clases)
        /// </summary>
        public Dictionary<string, Simbolo> ClaseSym { get; set; }
        /// <summary>
        /// Almacenará cualquier otras clases que se hayan importado
        /// -Esto se especificará en la segunda pasada-
        /// </summary>
        public Dictionary<string, Clase> ClaseImp { get; set; }
        /// <summary>
        /// Servirá de auxilio para determinar cuales son las clases que se hayan importado
        /// -Servirá en la primera pasada-
        /// </summary>
        public ParseTreeNode ClaseImpTree { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Clase()
        {
            ClaseEnt = new Dictionary<string, Funcion>();
            ClaseSym = new Dictionary<string, Simbolo>();
            ClaseImp = null;
            ClaseImpTree = null;
        }


        public Simbolo BuscarSimbolo(string nombreVar)
        {
            Simbolo retorno = new Simbolo();


            return retorno;
        }

        public void Ejecutar()
        {

        }

    }
}
