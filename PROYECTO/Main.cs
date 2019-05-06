using FastColoredTextBoxNS;
using Irony.Parsing;
using PROYECTO.Archivo;
using PROYECTO.Gramatica;
using System;
using System.IO;
using System.Windows.Forms;

namespace PROYECTO
{
    public partial class Main : Form
    {
        private int tabContador;
        //Servirá para guardar o abrir un archivo
        private SaveFileDialog saveDialog;
        private OpenFileDialog openDialog;
        public Main()
        {
            InitializeComponent();           
            openDialog = new OpenFileDialog
            {
                //Establece el directorio inicial
                InitialDirectory = "C:\\Users\\otzoy\\Desktop",
                //Especifica qué tipo de archivos se aceptarán
                Filter = "Documento de Text (*.txt) | *.txt",
                FilterIndex = 0,
                //Indica que deberá recordar el directorio anterior
                RestoreDirectory = true,
                //Evita que se seleccione más de un archivo a la vez
                Multiselect = false
            };
            saveDialog = new SaveFileDialog
            {
                //Establece el directorio inicial
                InitialDirectory = "C:\\Users\\otzoy\\Desktop",
                //Se crea y agrega un filtro para los archivos
                Filter = "Documento de Texto (*.txt) |*.txt",
                FilterIndex = 0,
                //Deja el directorio en el directorio en el que se cerro la 
                //última vez que se abrio el cuadro de dialogo durante 
                //el tiempzo de ejecuacion
                RestoreDirectory = true,
            };
            tabContador = 0;
            //NewTab("Untitled" + tabContador, "Untitled" + tabContador++);
        }
        /// <summary>
        /// Despliega un OpenDialogFile para seleccionar una archivo *.txt
        /// Determina si ya existe un tab con la dirección seleccionada
        /// Crea un nuevo tab (si no existe otro con la misma información) y escribe
        /// el texto del archivo dentro del FastTextBox que se encuentra dentro
        /// del nuevo tab creados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemAbrir_Click(object sender, EventArgs e)
        {
            //flag que servirá para evitar que se abrán dos archivos 
            //con una misma referencia
            bool flag = false;
            //Hace visible el dialogo
            //Si se presiona OK se selecciona un archivo y ejecuta la instrucción dentro del if
            if (openDialog.ShowDialog()==DialogResult.OK && !String.IsNullOrEmpty(openDialog.FileName))
            {
                //Crea un nuevo tab y agrega el contenido al txtbox, a menos que la dirección ya exista previamente
                foreach (TabPage tb in TabInput.Controls)
                {
                    //Verifica que el nombre (dirección) no exista ya en los
                    //tabs abiertos
                    if (tb.Name.Equals(openDialog.FileName))
                    {
                        flag = true;
                        tb.Focus();
                        break;
                    }
                }
                //Si no existe ningun tab que ya posea la dirección abierta
                //Se creará un nuevo tab
                if (!flag)
                {
                    //Crea el tab, especifica su ID como el AbsPath del archivo leído
                    //y el nombre como el nombre del archivo
                    var txtBox = NewTab(openDialog.FileName, Path.GetFileNameWithoutExtension(openDialog.FileName));
                    //Agrega el texto leído al txt que se encuentra
                    //dentro del tab recién creado
                    txtBox.Text = new ES().Leer(openDialog.FileName);
                }
            }
        }
        /// <summary>
        /// Crea un nuevo tab con una referencia "Untitled"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemNuevo_Click(object sender, EventArgs e)
        {
            NewTab("Untitled"+tabContador, "Untitled" + tabContador++);
        }
        /// <summary>
        /// Ejecuta las acciones de SaveFileLike
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemGuardarComo_Click(object sender, EventArgs e)
        {
            this.SaveFileLike();
        }
        /// <summary>
        /// Detecta si el tab actual posee alguna entrada en la lista de archivos
        /// si no posee , procede a un guardar como, si lo posee solo guarda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemGuardar_Click(object sender, EventArgs e)
        {
            //Detecta el tab seleccionado
            var tabSelected = TabInput.SelectedTab;
            //Realiza un "focus" al tab
            tabSelected.Focus();
            //Determina si el texto del tab aún no ha sido guardado
            if (tabSelected.Name.Contains("Untitled"))
            {
                //Se dirige a SaveFileLike
                SaveFileLike();
            } else
            {
                //Se dirige a SaveFile
                var Txt = (FastColoredTextBox)tabSelected.Controls[0];
                SaveFile(tabSelected.Name, Txt.Text);
                tabSelected.Text = Path.GetFileNameWithoutExtension(tabSelected.Name);
            }
        }
        /// <summary>
        /// Despliega el dialogo para guardar el texto de la pestaña seleccionada
        /// Recupera la referencia de la pestaña y procede a escribir un nuevo archivo
        /// </summary>
        private void SaveFileLike()
        {
            //Detecta el tab seleccionado
            var tabSelected = TabInput.SelectedTab;
            //Realiza un "focus" al tab
            tabSelected.Focus();
            //Despliega el dialogo para guardar un archivo
            if (saveDialog.ShowDialog()==DialogResult.OK)
            {
                //Establece la nueva referencia de la dirección del saveDialog 
                //al tab seleccionado
                if (!String.IsNullOrEmpty(saveDialog.FileName))
                {
                    //Recupera la referencia anterior del Tab
                    String tempName = tabSelected.Name;
                    //Actualiza la referencia y el nombre del Tab
                    tabSelected.Name = saveDialog.FileName;
                    tabSelected.Text = Path.GetFileNameWithoutExtension(saveDialog.FileName);
                    try
                    {
                        //Recupera el contenido del texto del Tab, con la referencia "tempName"
                        var Txt = (FastColoredTextBox) tabSelected.Controls[0];
                        SaveFile(saveDialog.FileName, Txt.Text);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// Guarda un archivo
        /// </summary>
        /// <param name="direccionArchivo"></param>
        /// <param name="contenidoArchivo"></param>
        /// <param name="extensionArchivo"></param>
        private void SaveFile(String direccionArchivo, String contenidoArchivo)
        {
            if (!String.IsNullOrEmpty(direccionArchivo))
            {
                StreamWriter outputFile = null;
                try
                {
                    using (outputFile = new StreamWriter(direccionArchivo))
                    {
                        outputFile.Write(contenidoArchivo);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    //Cierra el archivo
                    try
                    {
                        if (outputFile != null)
                        {
                            outputFile.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        /// <summary>
        /// Crea un nuevo tab, asigna un ID y un texto que se mostrará en la pestaña
        /// </summary>
        /// <param name="IDTab"></param>
        /// <param name="TextTab"></param>
        /// <returns>Devuelve el <code>FastColoredTextBox</code> que se insertó dentro del Tab recién creado</returns>
        private FastColoredTextBox NewTab(String IDTab, String TextTab)
        {
            //Instancia un nuevo tab y un cuadro de texto
            TabPage tabPage = new TabPage
            {
                Text = TextTab,
                Name = IDTab
            };
            FastColoredTextBox textBox = new FastColoredTextBox
            {
                Name = "txt" + IDTab,
                ShowScrollBars = true,
                Dock = DockStyle.Fill,
            };
            textBox.TextChanged += TextBox_TextChanged; //new EventHandler(TextBox_TextChanged);
            //Añade el textbox al tab
            tabPage.Controls.Add(textBox);
            //Añada el tab al control de Tab
            this.TabInput.Controls.Add(tabPage);
            //Hace focusa la pestaña recién creada
            tabPage.Focus();
            return textBox;
        }
        /// <summary>
        /// Método que se agrega al evento TextChanged del fasttext para identificar los cambios dentro de él
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var txtSender = (FastColoredTextBox)sender;
            var parentTab = (TabPage) txtSender.Parent;
            if (!parentTab.Text.Substring(0,1).Equals("*"))
            {
                parentTab.Text= "*" + parentTab.Text;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemCompilar_Click(object sender, EventArgs e)
        {
            /*
            Parser p = new Parser(new LanguageData(new Sintactico()));
            ParseTree arbol = p.Parse(((FastColoredTextBox)TabInput.SelectedTab.Controls[0]).Text);
            if (arbol.Root != null)
            {
                var ASTGraph = new ASTHtml(arbol.Root);
                
                this.SaveFile("ASTGraph.html", ASTGraph.GenerarHTML());
                new Recorrido().CrearClase(arbol.Root);
            } else
            {
                try {
                    //var Errores = new ERRHtml();.
                    foreach (var msg in arbol.ParserMessages)
                    {
                        Console.WriteLine(String.Format("{0} - {1}, ({2},{3}), {4}",msg.Level,msg.Message,msg.Location.Line,msg.Location.Column,msg.ParserState));
                    }
                    //this.SaveFile("Errores.html",Errores.GenerarHTML());
                }
                 catch (Exception) { }
            }*/
            int flag;
            Console.WriteLine(flag);
        }

        private void ItemErrores_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Errores.html");
            } catch (Exception ex)
            {
                MessageBox.Show(this, "Nada que mostrar. " + ex.Message, "Archivo no existe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
