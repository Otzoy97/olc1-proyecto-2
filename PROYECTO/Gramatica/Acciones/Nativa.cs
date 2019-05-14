using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO.Gramatica.Acciones
{
    /// <summary>
    /// Maneja todas las funciones nativas del programa
    /// </summary>
    static class Nativa
    {
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
    }
}
