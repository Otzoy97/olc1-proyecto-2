using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PROYECTO
{
    public partial class Main : Form
    {
        //Servirá para llevar un mejor control de las tab generadas
        private LinkedList<TabPage> tabPages;
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
                //el tiempo de ejecuacion
                RestoreDirectory = true,
            };
            tabPages = new LinkedList<TabPage>();
            tabContador = 0;
        }

        private void ItemAbrir_Click(object sender, EventArgs e)
        {
            //Hace visible el dialogo
            //Si se presiona OK se selecciona un archivo y ejecuta la instrucción dentro del if
            if (openDialog.ShowDialog()==DialogResult.OK && !String.IsNullOrEmpty(openDialog.FileName))
            {
                //Crea un nuevo tab y agrega el contenido al txtbox, a menos que la dirección 
                //del fileName ya exista en la lista de Nombres

            }
        }
        private void ItemNuevo_Click(object sender, EventArgs e)
        {
            //Tabpage y FastTextBox
            TabPage tabPage = new TabPage
            {
                Text = "Untitled"+tabContador,
                Name = "TabPage" + tabContador++
            };
            FastColoredTextBox textBox = new FastColoredTextBox
            {
                Name = "TextBox",
                ShowScrollBars = true,
                Dock = DockStyle.Fill
                /*Height = 223,
                Width = 625*/
            };
            tabPage.Controls.Add(textBox);
            tabPages.AddLast(tabPage);
            this.TabInput.Controls.Add(tabPage);
            //this.TabInput.Controls.Add(tabPage.Controls.Add(textBox));
        }

        private void ItemGuardarComo_Click(object sender, EventArgs e)
        {
            //this.SaveFileLike();
        }

        private void ItemGuardar_Click(object sender, EventArgs e)
        {
            //Detecta si el tab actual posee alguna entrada en la lista de archivos
            //si no posee , procede a un guardar como, si lo posee solo guarda
        }

        private void SaveFileLike()
        { 
            //Despliega el dialogo para guardar un archivo
            if (saveDialog.ShowDialog()==DialogResult.OK)
            {
                //Detecta el tab al cual se le tiene focus y procede a guardar el archivo
                //SaveFile(saveDialog.FileName,);
            }
        }
        
        private void SaveFile(String direccionArchivo, String contenidoArchivo, String extensionArchivo)
        {
            if (!String.IsNullOrEmpty(direccionArchivo))
            {
                StreamWriter outputFile = null;
                try
                {
                    using (outputFile = new StreamWriter(direccionArchivo + "." + extensionArchivo))
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

        private void TabInput_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
