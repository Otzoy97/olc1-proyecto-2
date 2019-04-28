using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;


namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    interface IOperacion
    {
        void Interpretar(Stack<Symbol> resultado);
    }
}
