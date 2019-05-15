using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony;
using Irony.Parsing;

namespace PROYECTO.Archivo
{
    static class ERRHtml
    {
        private static string Log = "";
        public static string ParserMessageHTML(LogMessageList Tokens)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>").AppendLine("<html lang=\"es-GT\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendFormat("<head>{0}{1}</head>", "<meta charset=\"utf-8\">", String.Format("<title>{0}</title>", "Reporte de errores")).AppendLine();
            sb.AppendLine("<style type=\"text/css\"> .tg  {border-collapse:collapse;border-spacing:0;border-color:#aabcfe;margin:0px auto;} .tg td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:#aabcfe;color:#669;background-color:#e8edff;} .tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:#aabcfe;color:#039;background-color:#b9c9fe;} .tg .tg-bf8s{background-color:#ffffff;color:#000000;border-color:#330001;text-align:left;vertical-align:top} .tg .tg-hlej{background-color:#c0c0c0;color:#000000;border-color:#330001;text-align:center;vertical-align:top} .tg .tg-pjo9{background-color:#9b9b9b;font-weight:bold;color:#000000;border-color:#330001;text-align:left;vertical-align:top} .tg .tg-qlpz{background-color:#ffffff;color:#000000;border-color:#330001;text-align:right;vertical-align:top} .tg .tg-ujlt{background-color:#ffffff;color:#000000;border-color:#330001;text-align:left;vertical-align:top} .tg .tg-q2qy{background-color:#ffffff;color:#000000;border-color:#330001;text-align:right;vertical-align:top} </style>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div>");
            sb.AppendLine("<table class=\"tg\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td class=\"tg-hlej\" colspan=\"4\">Mensajes del analizador</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th class=\"tg-pjo9\">Tipo</th><th td class=\"tg-pjo9\">Mensaje</th><th td class=\"tg-pjo9\">Fila</th><th td class=\"tg-pjo9\">Columna</th>");
            sb.AppendLine("</tr>");
            foreach (var token in Tokens)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td class=\"tg-bf8s\">{0}</td><td class=\"tg-bf8s\">{1}</td><td class=\"tg-bf8s\">{2}</td><td class=\"tg-bf8s\">{3}</td>", token.Level, token.Message, token.Location.Line+1, token.Location.Column+1);
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table><br><br><br>");
            sb.AppendLine("<table class=\"tg\">>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th class=\"tg-hlej\" >Mensajes del compilador</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine(Log);
            sb.AppendLine("</table>");
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }
        public static void AddMessageLog(string mensaje)
        {
            Log += String.Format("<tr><td class=\"tg-bf8s\" >{0}</td></tr>", mensaje);
        }
    }
}
