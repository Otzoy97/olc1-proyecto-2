using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO.Gramatica.Acciones
{
    public interface IFigura
    {

    }
    public class Linea : IFigura
    {
        public string Color { get; }
        public int InicioX { get; }
        public int InicioY { get; }
        public int FinX { get; }
        public int FinY { get; }
        public int Grosor { get; }
        public Linea(string color, int inx, int iny, int finx, int finy, int grosor)
        {
            this.Color = color;
            this.InicioX = inx;
            this.InicioY = iny;
            this.FinX = finx;
            this.FinY = finy;
            this.Grosor = grosor;
        }
    }
    public class Circulo : IFigura
    {
        public string Color { get; }
        public int Radio { get; }
        public bool EsSolido { get; }
        public int PosicionX { get; }
        public int PosicionY { get; }
        public Circulo(string color, int radio, bool essolido, int posx, int posy)
        {
            Color = color;
            Radio = radio;
            EsSolido = essolido;
            PosicionX = posx;
            PosicionY = posy;
        }
    }
    public class Rectangulo : IFigura
    {
        public string Color { get; }
        public bool EsSolido { get; }
        public int CentroX { get; }
        public int CentroY { get; }
        public int Ancho { get; }
        public int Alto { get; }
        public Rectangulo(string color, bool essolido, int centrox, int centroy, int ancho, int alto)
        {
            Color = color;
            CentroX = centrox;
            CentroY = centroy;
            Ancho = ancho;
            Alto = alto;
        }
    }
    public class Triangulo : IFigura
    {
        public string Color { get; }
        public bool Solido { get; }
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }
        public int X3 { get; }
        public int Y3 { get; }
        public Triangulo(string color, bool esSolido, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            Color = color;
            Solido = esSolido;
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
        }
    }
    /// <summary>
    /// Maneja todas las funciones nativas del programa
    /// </summary>
    static class Nativa
    {

        public static LinkedList<IFigura> ColaFiguras = new LinkedList<IFigura>();
        /// <summary>
        /// Imprime un mensaje en la consola de salida
        /// </summary>
        /// <param name="mensaje"></param>
        public static void Print(string mensaje)
        {
            Main.TxtOutput.Text += String.Format("{0}{1}", mensaje, Environment.NewLine);
        }
        /// <summary>
        /// Despliega una ventana modal
        /// </summary>
        /// <param name="titulo">El titulo que se mostrará en el modal</param>
        /// <param name="mensaje">El mensaje que se mostrará en el modal</param>
        public static void Show(object titulo, object mensaje)
        {
            MessageBox.Show(Main.TxtOutput.TopLevelControl, mensaje.ToString(), titulo.ToString());
        }
        /// <summary>
        /// Encola una nueva figura
        /// </summary>
        /// <param name="figura"></param>
        public static void AgregarFigura(IFigura figura)
        {
            ColaFiguras.Enqueue(figura);
        }
        /// <summary>
        /// 
        /// </summary>
        public static void PintarFigura(string nombreFig)
        {
            if (ColaFiguras.Count > 0)
            {
                //Despliega el form y dibuja las figuras almacenadas en el linked list
                Figura fig = new Figura(ColaFiguras);
                fig.Dibujar();
                fig.Show();
                fig.Text = nombreFig;
                //Reinicia el buffer
                ColaFiguras = new LinkedList<IFigura>();
            }
            else
            {
                Main.Imprimir("No hay ninguna figura en el buffer");
            }
        }
    }
}
