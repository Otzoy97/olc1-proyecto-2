using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace PROYECTO.Archivo
{
    class ERRHtml
    {
        private TokenList Tokens;
        public ERRHtml(TokenList Tokens)
        {
            this.Tokens = Tokens;
        }
        public string GenerarHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>").AppendLine("<html lang=\"es-GT\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendFormat("<head>{0}{1}</head>", "<meta charset=\"utf-8\">", String.Format("<title>{0}</title>", "Errores léxicos y sintácticos")).AppendLine();
            sb.AppendLine("<body>");
            sb.AppendLine("<div>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Token</th><th>Lexema</th><th>Fila</th><th>Columna</th><th>Observaciones</th>");
            sb.AppendLine("</tr>");
            foreach (var token in Tokens)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>", token.KeyTerm, token.Text, token.Location.Line, token.Location.Column, token.ValueString);
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }
    }
}
