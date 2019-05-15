namespace PROYECTO
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ItemGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemErrores = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemAST = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ItemCompilar = new System.Windows.Forms.ToolStripMenuItem();
            this.TabOutput = new System.Windows.Forms.TabControl();
            this.TabConsola = new System.Windows.Forms.TabPage();
            this.txtoutput = new System.Windows.Forms.TextBox();
            this.TabVariable = new System.Windows.Forms.TabPage();
            this.gridVar = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TabInput = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.TabOutput.SuspendLayout();
            this.TabConsola.SuspendLayout();
            this.TabVariable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.herramientasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(978, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemAbrir,
            this.ItemNuevo,
            this.toolStripSeparator1,
            this.ItemGuardar,
            this.ItemGuardarComo});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // ItemAbrir
            // 
            this.ItemAbrir.Name = "ItemAbrir";
            this.ItemAbrir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ItemAbrir.Size = new System.Drawing.Size(293, 26);
            this.ItemAbrir.Text = "Abrir...";
            this.ItemAbrir.Click += new System.EventHandler(this.ItemAbrir_Click);
            // 
            // ItemNuevo
            // 
            this.ItemNuevo.Name = "ItemNuevo";
            this.ItemNuevo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.ItemNuevo.Size = new System.Drawing.Size(293, 26);
            this.ItemNuevo.Text = "Nuevo";
            this.ItemNuevo.Click += new System.EventHandler(this.ItemNuevo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(290, 6);
            // 
            // ItemGuardar
            // 
            this.ItemGuardar.Name = "ItemGuardar";
            this.ItemGuardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ItemGuardar.Size = new System.Drawing.Size(293, 26);
            this.ItemGuardar.Text = "Guardar";
            this.ItemGuardar.Click += new System.EventHandler(this.ItemGuardar_Click);
            // 
            // ItemGuardarComo
            // 
            this.ItemGuardarComo.Name = "ItemGuardarComo";
            this.ItemGuardarComo.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.ItemGuardarComo.Size = new System.Drawing.Size(293, 26);
            this.ItemGuardarComo.Text = "Guardar como...";
            this.ItemGuardarComo.Click += new System.EventHandler(this.ItemGuardarComo_Click);
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemErrores,
            this.ItemAST,
            this.toolStripSeparator3,
            this.ItemCompilar});
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            // 
            // ItemErrores
            // 
            this.ItemErrores.Name = "ItemErrores";
            this.ItemErrores.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.ItemErrores.Size = new System.Drawing.Size(257, 26);
            this.ItemErrores.Text = "Reporte de errores";
            this.ItemErrores.Click += new System.EventHandler(this.ItemErrores_Click);
            // 
            // ItemAST
            // 
            this.ItemAST.Name = "ItemAST";
            this.ItemAST.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.ItemAST.Size = new System.Drawing.Size(257, 26);
            this.ItemAST.Text = "Mostrar AST";
            this.ItemAST.Click += new System.EventHandler(this.ItemAST_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(254, 6);
            // 
            // ItemCompilar
            // 
            this.ItemCompilar.Name = "ItemCompilar";
            this.ItemCompilar.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.ItemCompilar.Size = new System.Drawing.Size(257, 26);
            this.ItemCompilar.Text = "Compilar";
            this.ItemCompilar.Click += new System.EventHandler(this.ItemCompilar_Click);
            // 
            // TabOutput
            // 
            this.TabOutput.Controls.Add(this.TabConsola);
            this.TabOutput.Controls.Add(this.TabVariable);
            this.TabOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TabOutput.Location = new System.Drawing.Point(0, 463);
            this.TabOutput.MinimumSize = new System.Drawing.Size(847, 216);
            this.TabOutput.Name = "TabOutput";
            this.TabOutput.Padding = new System.Drawing.Point(5, 5);
            this.TabOutput.SelectedIndex = 0;
            this.TabOutput.Size = new System.Drawing.Size(978, 216);
            this.TabOutput.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabOutput.TabIndex = 0;
            // 
            // TabConsola
            // 
            this.TabConsola.Controls.Add(this.txtoutput);
            this.TabConsola.Location = new System.Drawing.Point(4, 29);
            this.TabConsola.Name = "TabConsola";
            this.TabConsola.Padding = new System.Windows.Forms.Padding(3);
            this.TabConsola.Size = new System.Drawing.Size(970, 183);
            this.TabConsola.TabIndex = 0;
            this.TabConsola.Text = "Consola";
            this.TabConsola.UseVisualStyleBackColor = true;
            // 
            // txtoutput
            // 
            this.txtoutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtoutput.Font = new System.Drawing.Font("Courier New", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtoutput.Location = new System.Drawing.Point(3, 3);
            this.txtoutput.Multiline = true;
            this.txtoutput.Name = "txtoutput";
            this.txtoutput.Size = new System.Drawing.Size(964, 177);
            this.txtoutput.TabIndex = 0;
            // 
            // TabVariable
            // 
            this.TabVariable.Controls.Add(this.gridVar);
            this.TabVariable.Location = new System.Drawing.Point(4, 29);
            this.TabVariable.Name = "TabVariable";
            this.TabVariable.Padding = new System.Windows.Forms.Padding(3);
            this.TabVariable.Size = new System.Drawing.Size(970, 183);
            this.TabVariable.TabIndex = 1;
            this.TabVariable.Text = "Variables";
            this.TabVariable.UseVisualStyleBackColor = true;
            // 
            // gridVar
            // 
            this.gridVar.AllowUserToAddRows = false;
            this.gridVar.AllowUserToDeleteRows = false;
            this.gridVar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.gridVar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVar.Location = new System.Drawing.Point(3, 3);
            this.gridVar.Name = "gridVar";
            this.gridVar.ReadOnly = true;
            this.gridVar.RowTemplate.Height = 24;
            this.gridVar.Size = new System.Drawing.Size(964, 177);
            this.gridVar.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nombre";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Dato";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Tipo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Linea";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Columna";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Entorno";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // TabInput
            // 
            this.TabInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabInput.Location = new System.Drawing.Point(0, 28);
            this.TabInput.MaximumSize = new System.Drawing.Size(99999, 99999);
            this.TabInput.MinimumSize = new System.Drawing.Size(840, 400);
            this.TabInput.Name = "TabInput";
            this.TabInput.SelectedIndex = 0;
            this.TabInput.Size = new System.Drawing.Size(978, 435);
            this.TabInput.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabInput.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 679);
            this.Controls.Add(this.TabInput);
            this.Controls.Add(this.TabOutput);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(865, 700);
            this.Name = "Main";
            this.Text = "Interpreter";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TabOutput.ResumeLayout(false);
            this.TabConsola.ResumeLayout(false);
            this.TabConsola.PerformLayout();
            this.TabVariable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItemAbrir;
        private System.Windows.Forms.ToolStripMenuItem ItemNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ItemGuardar;
        private System.Windows.Forms.ToolStripMenuItem ItemGuardarComo;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItemErrores;
        private System.Windows.Forms.ToolStripMenuItem ItemAST;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ItemCompilar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl TabOutput;
        private System.Windows.Forms.TabPage TabConsola;
        private System.Windows.Forms.TabPage TabVariable;
        private System.Windows.Forms.TabControl TabInput;
        private System.Windows.Forms.TextBox txtoutput;
        private System.Windows.Forms.DataGridView gridVar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}

