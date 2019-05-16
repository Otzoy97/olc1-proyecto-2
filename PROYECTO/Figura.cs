using PROYECTO.Gramatica.Acciones;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace PROYECTO
{
    public partial class Figura : Form
    {
        private LinkedList<IFigura> Figuras { get; }
        public Figura(LinkedList<IFigura> figuras)
        {
            InitializeComponent();
            Figuras = figuras;
        }
        public void Dibujar()
        {
            foreach (var fig in Figuras)
            {
                switch (fig.GetType().Name)
                {
                    case "Triangulo":
                        Dibujar_Triangulo((Triangulo)fig);
                        break;
                    case "Circulo":
                        Dibujar_Circulo((Circulo)fig);
                        break;
                    case "Rectangulo":
                        Dibujar_Rectangulo((Rectangulo)fig);
                        break;
                    case "Linea":
                        Dibujar_linea((Linea)fig);
                        break;
                }
            }
        }
        /// <summary>
        /// Determina el color que se debe colocar a la figura
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Brush Brushing(string color)
        {
            switch (color.ToLower())
            {
                case "aliceblue": return new SolidBrush(Color.AliceBlue);
                case "antiquewhite": return new SolidBrush(Color.AntiqueWhite);
                case "aqua": return new SolidBrush(Color.Aqua);
                case "aquamarine": return new SolidBrush(Color.Aquamarine);
                case "azure": return new SolidBrush(Color.Azure);
                case "beige": return new SolidBrush(Color.Beige);
                case "bisque": return new SolidBrush(Color.Bisque);
                case "black": return new SolidBrush(Color.Black);
                case "blanchedalmond": return new SolidBrush(Color.BlanchedAlmond);
                case "blue": return new SolidBrush(Color.Blue);
                case "blueviolet": return new SolidBrush(Color.BlueViolet);
                case "brown": return new SolidBrush(Color.Brown);
                case "burlywood": return new SolidBrush(Color.BurlyWood);
                case "cadetblue": return new SolidBrush(Color.CadetBlue);
                case "chartreuse": return new SolidBrush(Color.Chartreuse);
                case "chocolate": return new SolidBrush(Color.Chocolate);
                case "coral": return new SolidBrush(Color.Coral);
                case "cornflowerblue": return new SolidBrush(Color.CornflowerBlue);
                case "cornsilk": return new SolidBrush(Color.Cornsilk);
                case "crimson": return new SolidBrush(Color.Crimson);
                case "cyan": return new SolidBrush(Color.Cyan);
                case "darkblue": return new SolidBrush(Color.DarkBlue);
                case "darkcyan": return new SolidBrush(Color.DarkCyan);
                case "darkgoldenrod": return new SolidBrush(Color.DarkGoldenrod);
                case "darkgray": return new SolidBrush(Color.DarkGray);
                case "darkgreen": return new SolidBrush(Color.DarkGreen);
                case "darkkhaki": return new SolidBrush(Color.DarkKhaki);
                case "darkmagenta": return new SolidBrush(Color.DarkMagenta);
                case "darkolivegreen": return new SolidBrush(Color.DarkOliveGreen);
                case "darkorange": return new SolidBrush(Color.DarkOrange);
                case "darkorchid": return new SolidBrush(Color.DarkOrchid);
                case "darkred": return new SolidBrush(Color.DarkRed);
                case "darksalmon": return new SolidBrush(Color.DarkSalmon);
                case "darkseagreen": return new SolidBrush(Color.DarkSeaGreen);
                case "darkslateblue": return new SolidBrush(Color.DarkSlateBlue);
                case "darkslategray": return new SolidBrush(Color.DarkSlateGray);
                case "darkturquoise": return new SolidBrush(Color.DarkTurquoise);
                case "darkviolet": return new SolidBrush(Color.DarkViolet);
                case "deeppink": return new SolidBrush(Color.DeepPink);
                case "deepskyblue": return new SolidBrush(Color.DeepSkyBlue);
                case "dimgray": return new SolidBrush(Color.DimGray);
                case "dodgerblue": return new SolidBrush(Color.DodgerBlue);
                case "firebrick": return new SolidBrush(Color.Firebrick);
                case "floralwhite": return new SolidBrush(Color.FloralWhite);
                case "forestgreen": return new SolidBrush(Color.ForestGreen);
                case "fuchsia": return new SolidBrush(Color.Fuchsia);
                case "gainsboro": return new SolidBrush(Color.Gainsboro);
                case "ghostwhite": return new SolidBrush(Color.GhostWhite);
                case "gold": return new SolidBrush(Color.Gold);
                case "goldenrod": return new SolidBrush(Color.Goldenrod);
                case "gray": return new SolidBrush(Color.Gray);
                case "green": return new SolidBrush(Color.Green);
                case "greenyellow": return new SolidBrush(Color.GreenYellow);
                case "honeydew": return new SolidBrush(Color.Honeydew);
                case "hotpink": return new SolidBrush(Color.HotPink);
                case "indianred": return new SolidBrush(Color.IndianRed);
                case "indigo": return new SolidBrush(Color.Indigo);
                case "ivory": return new SolidBrush(Color.Ivory);
                case "khaki": return new SolidBrush(Color.Khaki);
                case "lavender": return new SolidBrush(Color.Lavender);
                case "lavenderblush": return new SolidBrush(Color.LavenderBlush);
                case "lawngreen": return new SolidBrush(Color.LawnGreen);
                case "lemonchiffon": return new SolidBrush(Color.LemonChiffon);
                case "lightblue": return new SolidBrush(Color.LightBlue);
                case "lightcoral": return new SolidBrush(Color.LightCoral);
                case "lightcyan": return new SolidBrush(Color.LightCyan);
                case "lightgoldenrodyellow": return new SolidBrush(Color.LightGoldenrodYellow);
                case "lightgray": return new SolidBrush(Color.LightGray);
                case "lightgreen": return new SolidBrush(Color.LightGreen);
                case "lightpink": return new SolidBrush(Color.LightPink);
                case "lightsalmon": return new SolidBrush(Color.LightSalmon);
                case "lightseagreen": return new SolidBrush(Color.LightSeaGreen);
                case "lightskyblue": return new SolidBrush(Color.LightSkyBlue);
                case "lightslategray": return new SolidBrush(Color.LightSlateGray);
                case "lightsteelblue": return new SolidBrush(Color.LightSteelBlue);
                case "lightyellow": return new SolidBrush(Color.LightYellow);
                case "lime": return new SolidBrush(Color.Lime);
                case "limegreen": return new SolidBrush(Color.LimeGreen);
                case "linen": return new SolidBrush(Color.Linen);
                case "magenta": return new SolidBrush(Color.Magenta);
                case "maroon": return new SolidBrush(Color.Maroon);
                case "mediumaquamarine": return new SolidBrush(Color.MediumAquamarine);
                case "mediumblue": return new SolidBrush(Color.MediumBlue);
                case "mediumorchid": return new SolidBrush(Color.MediumOrchid);
                case "mediumpurple": return new SolidBrush(Color.MediumPurple);
                case "mediumseagreen": return new SolidBrush(Color.MediumSeaGreen);
                case "mediumslateblue": return new SolidBrush(Color.MediumSlateBlue);
                case "mediumspringgreen": return new SolidBrush(Color.MediumSpringGreen);
                case "mediumturquoise": return new SolidBrush(Color.MediumTurquoise);
                case "mediumvioletred": return new SolidBrush(Color.MediumVioletRed);
                case "midnightblue": return new SolidBrush(Color.MidnightBlue);
                case "mintcream": return new SolidBrush(Color.MintCream);
                case "mistyrose": return new SolidBrush(Color.MistyRose);
                case "moccasin": return new SolidBrush(Color.Moccasin);
                case "navajowhite": return new SolidBrush(Color.NavajoWhite);
                case "navy": return new SolidBrush(Color.Navy);
                case "oldlace": return new SolidBrush(Color.OldLace);
                case "olive": return new SolidBrush(Color.Olive);
                case "olivedrab": return new SolidBrush(Color.OliveDrab);
                case "orange": return new SolidBrush(Color.Orange);
                case "orangered": return new SolidBrush(Color.OrangeRed);
                case "orchid": return new SolidBrush(Color.Orchid);
                case "palegoldenrod": return new SolidBrush(Color.PaleGoldenrod);
                case "palegreen": return new SolidBrush(Color.PaleGreen);
                case "paleturquoise": return new SolidBrush(Color.PaleTurquoise);
                case "palevioletred": return new SolidBrush(Color.PaleVioletRed);
                case "papayawhip": return new SolidBrush(Color.PapayaWhip);
                case "peachpuff": return new SolidBrush(Color.PeachPuff);
                case "peru": return new SolidBrush(Color.Peru);
                case "pink": return new SolidBrush(Color.Pink);
                case "plum": return new SolidBrush(Color.Plum);
                case "powderblue": return new SolidBrush(Color.PowderBlue);
                case "purple": return new SolidBrush(Color.Purple);
                case "red": return new SolidBrush(Color.Red);
                case "rosybrown": return new SolidBrush(Color.RosyBrown);
                case "royalblue": return new SolidBrush(Color.RoyalBlue);
                case "saddlebrown": return new SolidBrush(Color.SaddleBrown);
                case "salmon": return new SolidBrush(Color.Salmon);
                case "sandybrown": return new SolidBrush(Color.SandyBrown);
                case "seagreen": return new SolidBrush(Color.SeaGreen);
                case "seashell": return new SolidBrush(Color.SeaShell);
                case "sienna": return new SolidBrush(Color.Sienna);
                case "silver": return new SolidBrush(Color.Silver);
                case "skyblue": return new SolidBrush(Color.SkyBlue);
                case "slateblue": return new SolidBrush(Color.SlateBlue);
                case "slategray": return new SolidBrush(Color.SlateGray);
                case "snow": return new SolidBrush(Color.Snow);
                case "springgreen": return new SolidBrush(Color.SpringGreen);
                case "steelblue": return new SolidBrush(Color.SteelBlue);
                case "tan": return new SolidBrush(Color.Tan);
                case "teal": return new SolidBrush(Color.Teal);
                case "thistle": return new SolidBrush(Color.Thistle);
                case "tomato": return new SolidBrush(Color.Tomato);
                case "transparent": return new SolidBrush(Color.Transparent);
                case "turquoise": return new SolidBrush(Color.Turquoise);
                case "violet": return new SolidBrush(Color.Violet);
                case "wheat": return new SolidBrush(Color.Wheat);
                case "white": return new SolidBrush(Color.White);
                case "whitesmoke": return new SolidBrush(Color.WhiteSmoke);
                case "yellow": return new SolidBrush(Color.Yellow);
                case "yellowgreen": return new SolidBrush(Color.YellowGreen);
                default: return new SolidBrush(System.Drawing.ColorTranslator.FromHtml(color.ToUpper()));
            }
        }
        private void Dibujar_Circulo(Circulo circulo)
        {
            SolidBrush Solido = (SolidBrush)Brushing(circulo.Color);
            Pen Linea = new Pen((SolidBrush)Brushing(circulo.Color), 1);
            Graphics formGraphics = CreateGraphics();
            if (circulo.EsSolido)
            {
                formGraphics.FillEllipse(Solido, new Rectangle(circulo.PosicionX - circulo.Radio, circulo.PosicionY - circulo.Radio, circulo.Radio * 2, circulo.Radio * 2));
            }
            else
            {
                formGraphics.DrawEllipse(Linea, new Rectangle(circulo.PosicionX - circulo.Radio, circulo.PosicionY - circulo.Radio, circulo.Radio * 2, circulo.Radio * 2));
            }
        }
        private void Dibujar_Triangulo(Triangulo triangulo)
        {
            SolidBrush Solido = (SolidBrush)Brushing(triangulo.Color);
            Pen Linea = new Pen((SolidBrush)Brushing(triangulo.Color), 1);
            Graphics formGraphics = CreateGraphics();

            PointF point1 = new PointF(triangulo.X1, triangulo.Y1);
            PointF point2 = new PointF(triangulo.X2, triangulo.Y2);
            PointF point3 = new PointF(triangulo.X3, triangulo.Y3);
            PointF[] curvePoints = { point1, point2, point3, };
            if (triangulo.Solido)
            {
                formGraphics.FillPolygon(Solido, curvePoints);
            }
            else
            {
                formGraphics.DrawPolygon(Linea, curvePoints);
            }
        }
        private void Dibujar_Rectangulo(Rectangulo rectangulo)
        {
            SolidBrush Solido = (SolidBrush)Brushing(rectangulo.Color);
            Pen Linea = new Pen((SolidBrush)Brushing(rectangulo.Color), 1);
            Graphics formGraphics = CreateGraphics();
            int x = rectangulo.CentroX - rectangulo.Ancho / 2;
            int y = rectangulo.CentroY - rectangulo.Alto / 2;
            //es solido o no
            if (rectangulo.EsSolido)
            {
                formGraphics.FillRectangle(Solido, new Rectangle(x, y, rectangulo.Ancho, rectangulo.Alto));
            }
            else
            {
                formGraphics.DrawRectangle(Linea, new Rectangle(x, y, rectangulo.Ancho, rectangulo.Alto));
            }
        }
        private void Dibujar_linea(Linea linea)
        {
            Pen Linea = new Pen((SolidBrush)Brushing(linea.Color), linea.Grosor);
            Graphics formGraphics = CreateGraphics();
            PointF punto1 = new PointF(linea.InicioX, linea.InicioY);
            PointF punto2 = new PointF(linea.FinX, linea.FinY);
            formGraphics.DrawLine(Linea, punto1, punto2);
        }
    }
}
