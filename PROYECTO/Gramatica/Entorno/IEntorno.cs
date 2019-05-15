namespace PROYECTO.Gramatica.Entorno
{
    interface IEntorno 
    {
        Simbolo BuscarSimbolo(string nombreVar);
        Clase BuscarClasePadre();
        Funcion BuscarFuncionPadre();
        bool Ejecutar();
        bool EsLoop();
    }
}
