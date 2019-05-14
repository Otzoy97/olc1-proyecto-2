namespace PROYECTO.Gramatica.Entorno
{
    interface IEntorno 
    {
        Simbolo BuscarSimbolo(string nombreVar);
        void Ejecutar();
    }
}
