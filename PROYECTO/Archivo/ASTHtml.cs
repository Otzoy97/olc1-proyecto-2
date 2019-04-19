using Irony.Parsing;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace PROYECTO.Archivo
{
    class ASTHtml
    {
        private ParseTreeNode raiz;
        private int contador = 1;
        /// <summary> 
        /// </summary>
        /// <param name="raiz">Nodo raíz del AST devuelto por Irony</param>
        public ASTHtml(ParseTreeNode raiz)
        {
            this.raiz = raiz;
        }
        /// <summary>
        /// Genera una cadena que representa el contenido
        /// de un archivo HTML con la imagen AST en BASE64
        /// </summary>
        /// <returns></returns>
        public string GenerarHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>").AppendLine("<html lang=\"es-GT\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendFormat("<head>{0}{1}</head>", "<meta charset=\"utf-8\">", String.Format("<title>{0}</title>","Arbol sintáctico")).AppendLine();
            sb.AppendLine("<body>");
            sb.AppendLine("<div>");

            using (var ms = new MemoryStream())
            {
                using(Bitmap bitmap = GenerarDOT(ObtenerDOT()))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var base64 = Convert.ToBase64String(ms.GetBuffer());
                    sb.AppendFormat("<img style=\"display:block;\" alt=\"ASTGraph\" src=\"data:image/jpg;base64,{0}\"/>", base64);
                }
            }

            sb.AppendLine("</div>");
            sb.AppendLine("</body>").AppendLine("</html>");
            return sb.ToString();
        }
        /// <summary>
        /// Dada una cadena de entrada genera una imagen con Graphviz
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        private Bitmap GenerarDOT(string dot)
        {
            string executable = @".\External\dot.exe";
            string output = @".\External\tempgraph";
            File.WriteAllText(output, dot);

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = executable;
            process.StartInfo.Arguments = string.Format(@"{0} -Tjpg -O", output);

            // Go
            process.Start();
            // and wait dot.exe to complete and exit
            process.WaitForExit();
            Bitmap bitmap = null; ;
            using (Stream bmpStream = System.IO.File.Open(output + ".jpg", System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                bitmap = new Bitmap(image);
            }
            File.Delete(output);
            File.Delete(output + ".jpg");
            return bitmap;
        }
        /// <summary>
        /// Método recursivo que servira para recorrer
        /// todo el AST creando a su paso una cadena
        /// que servirá para generar el archivo DOT
        /// </summary>
        /// <param name="padre"></param>
        /// <param name="hijos"></param>
        /// <returns></returns>
        private String RecorrerAST(String padre, ParseTreeNode hijos)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {
                String nombreHijo = String.Format("Nodo{0}", contador);
                sb.AppendFormat(nombreHijo).AppendFormat("[label=\"{0}\"];",Escapar(hijo.ToString())).AppendLine();
                sb.AppendFormat("{0}->{1};",padre, nombreHijo).AppendLine();
                contador++;
                sb.Append(RecorrerAST(nombreHijo, hijo));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Inicia el reocorrido por el AST
        /// </summary>
        /// <returns></returns>
        private String ObtenerDOT()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph G{");
            sb.AppendLine("graph[dpi=200]");
            sb.AppendLine("node[shape=\"box\"];");
            sb.AppendFormat("Nodo0[label=\"{0}\"];", Escapar(raiz.ToString())).AppendLine();
            sb.Append(RecorrerAST("Nodo0",raiz));
            sb.AppendLine("}");
            return sb.ToString();
        }
        /// <summary>
        /// Evita que ciertos caracteres de escape
        /// aparezcan en el archivo final
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        private String Escapar(String entrada)
        {
            entrada = entrada.Replace("\\","\\\\");
            entrada = entrada.Replace("\"", "\\\"");
            return entrada;
        }
    }
}
