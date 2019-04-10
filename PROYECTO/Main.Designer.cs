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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.itemNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemErrores = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAST = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.itemCompilar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAbrir,
            this.itemNuevo,
            this.toolStripSeparator1,
            this.itemGuardar,
            this.itemGuardarComo,
            this.toolStripSeparator2,
            this.itemSalir});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // itemAbrir
            // 
            this.itemAbrir.Name = "itemAbrir";
            this.itemAbrir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itemAbrir.Size = new System.Drawing.Size(293, 26);
            this.itemAbrir.Text = "Abrir...";
            // 
            // itemNuevo
            // 
            this.itemNuevo.Name = "itemNuevo";
            this.itemNuevo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.itemNuevo.Size = new System.Drawing.Size(293, 26);
            this.itemNuevo.Text = "Nuevo";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(290, 6);
            // 
            // itemGuardar
            // 
            this.itemGuardar.Name = "itemGuardar";
            this.itemGuardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itemGuardar.Size = new System.Drawing.Size(293, 26);
            this.itemGuardar.Text = "Guardar";
            // 
            // itemGuardarComo
            // 
            this.itemGuardarComo.Name = "itemGuardarComo";
            this.itemGuardarComo.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.itemGuardarComo.Size = new System.Drawing.Size(293, 26);
            this.itemGuardarComo.Text = "Guardar como...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(290, 6);
            // 
            // itemSalir
            // 
            this.itemSalir.Name = "itemSalir";
            this.itemSalir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.itemSalir.Size = new System.Drawing.Size(293, 26);
            this.itemSalir.Text = "Salir";
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemErrores,
            this.itemAST,
            this.toolStripSeparator3,
            this.itemCompilar});
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            // 
            // itemErrores
            // 
            this.itemErrores.Name = "itemErrores";
            this.itemErrores.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.itemErrores.Size = new System.Drawing.Size(257, 26);
            this.itemErrores.Text = "Reporte de errores";
            // 
            // itemAST
            // 
            this.itemAST.Name = "itemAST";
            this.itemAST.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.itemAST.Size = new System.Drawing.Size(257, 26);
            this.itemAST.Text = "Generar AST";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(254, 6);
            // 
            // itemCompilar
            // 
            this.itemCompilar.Name = "itemCompilar";
            this.itemCompilar.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.itemCompilar.Size = new System.Drawing.Size(257, 26);
            this.itemCompilar.Text = "Compilar";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "[OLC1] - PROYECTO 2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemAbrir;
        private System.Windows.Forms.ToolStripMenuItem itemNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem itemGuardar;
        private System.Windows.Forms.ToolStripMenuItem itemGuardarComo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem itemSalir;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemErrores;
        private System.Windows.Forms.ToolStripMenuItem itemAST;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem itemCompilar;
    }
}

