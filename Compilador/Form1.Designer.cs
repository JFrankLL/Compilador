namespace Compilador
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing){
            if (disposing && (components != null)){
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.guardarComoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.compileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.nuevoArchivo = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cortar = new System.Windows.Forms.ToolStripButton();
            this.copiar = new System.Windows.Forms.ToolStripButton();
            this.pegar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.acerca = new System.Windows.Forms.ToolStripButton();
            this.acercaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empezarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.miSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitCode = new System.Windows.Forms.SplitContainer();
            this.nLineaBox = new System.Windows.Forms.RichTextBox();
            this.codeBox = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lexicoBox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mi_copiar = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_cortar = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_pegar = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_alinNum = new System.Windows.Forms.ToolStripMenuItem();
            this.sintaticoBox = new System.Windows.Forms.RichTextBox();
            this.semanticoBox = new System.Windows.Forms.RichTextBox();
            this.codIntBox = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.erroresBox = new System.Windows.Forms.RichTextBox();
            this.resultadosBox = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCode)).BeginInit();
            this.splitCode.Panel1.SuspendLayout();
            this.splitCode.Panel2.SuspendLayout();
            this.splitCode.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // guardarComoButton
            // 
            this.guardarComoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.guardarComoButton.Image = ((System.Drawing.Image)(resources.GetObject("guardarComoButton.Image")));
            this.guardarComoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.guardarComoButton.Name = "guardarComoButton";
            this.guardarComoButton.Size = new System.Drawing.Size(23, 20);
            this.guardarComoButton.Text = "Guradar como..";
            this.guardarComoButton.Click += new System.EventHandler(this.guardarComo);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // compileButton
            // 
            this.compileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.compileButton.Image = ((System.Drawing.Image)(resources.GetObject("compileButton.Image")));
            this.compileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(23, 20);
            this.compileButton.Text = "compileButton";
            this.compileButton.ToolTipText = "Compilar";
            this.compileButton.Click += new System.EventHandler(this.compilar);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoArchivo,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.guardarComoButton,
            this.toolStripSeparator,
            this.cortar,
            this.copiar,
            this.pegar,
            this.toolStripSeparator3,
            this.compileButton,
            this.toolStripSeparator1,
            this.acerca});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 29);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1006, 23);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // nuevoArchivo
            // 
            this.nuevoArchivo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nuevoArchivo.Image = ((System.Drawing.Image)(resources.GetObject("nuevoArchivo.Image")));
            this.nuevoArchivo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nuevoArchivo.Name = "nuevoArchivo";
            this.nuevoArchivo.Size = new System.Drawing.Size(23, 20);
            this.nuevoArchivo.Text = "&New";
            this.nuevoArchivo.ToolTipText = "Nuevo archivo vacio";
            this.nuevoArchivo.Click += new System.EventHandler(this.nuevoArch);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 20);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.miAbrir_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 20);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.guardar);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 23);
            // 
            // cortar
            // 
            this.cortar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cortar.Image = ((System.Drawing.Image)(resources.GetObject("cortar.Image")));
            this.cortar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cortar.Name = "cortar";
            this.cortar.Size = new System.Drawing.Size(23, 20);
            this.cortar.Text = "C&ut";
            this.cortar.ToolTipText = "Cortar";
            this.cortar.Click += new System.EventHandler(this.cortar_Click);
            // 
            // copiar
            // 
            this.copiar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copiar.Image = ((System.Drawing.Image)(resources.GetObject("copiar.Image")));
            this.copiar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copiar.Name = "copiar";
            this.copiar.Size = new System.Drawing.Size(23, 20);
            this.copiar.Text = "&Copy";
            this.copiar.ToolTipText = "Copiar";
            this.copiar.Click += new System.EventHandler(this.copiar_Click);
            // 
            // pegar
            // 
            this.pegar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pegar.Image = ((System.Drawing.Image)(resources.GetObject("pegar.Image")));
            this.pegar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pegar.Name = "pegar";
            this.pegar.Size = new System.Drawing.Size(23, 20);
            this.pegar.Text = "&Paste";
            this.pegar.ToolTipText = "Pegar";
            this.pegar.Click += new System.EventHandler(this.pegar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // acerca
            // 
            this.acerca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.acerca.Image = ((System.Drawing.Image)(resources.GetObject("acerca.Image")));
            this.acerca.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.acerca.Name = "acerca";
            this.acerca.Size = new System.Drawing.Size(23, 20);
            this.acerca.Text = "He&lp";
            this.acerca.ToolTipText = "Acerca";
            this.acerca.Click += new System.EventHandler(this.acerca_Click);
            // 
            // acercaToolStripMenuItem
            // 
            this.acercaToolStripMenuItem.Name = "acercaToolStripMenuItem";
            this.acercaToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.acercaToolStripMenuItem.Text = "Acerca";
            this.acercaToolStripMenuItem.Click += new System.EventHandler(this.acerca_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaToolStripMenuItem});
            this.ayudaToolStripMenuItem.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // empezarToolStripMenuItem
            // 
            this.empezarToolStripMenuItem.Name = "empezarToolStripMenuItem";
            this.empezarToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.empezarToolStripMenuItem.Text = "Empezar";
            this.empezarToolStripMenuItem.Click += new System.EventHandler(this.compilar);
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.compilarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empezarToolStripMenuItem});
            this.compilarToolStripMenuItem.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compilarToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(85, 25);
            this.compilarToolStripMenuItem.Text = "Compilar";
            // 
            // formatoToolStripMenuItem
            // 
            this.formatoToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.formatoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_alinNum});
            this.formatoToolStripMenuItem.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formatoToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.formatoToolStripMenuItem.Name = "formatoToolStripMenuItem";
            this.formatoToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.formatoToolStripMenuItem.Text = "Formato";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_copiar,
            this.mi_cortar,
            this.mi_pegar});
            this.editarToolStripMenuItem.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editarToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(63, 25);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbrir,
            this.guardarToolStripMenuItem,
            this.miGuardarComo,
            this.miSalir});
            this.archivoToolStripMenuItem.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.archivoToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(75, 25);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // miAbrir
            // 
            this.miAbrir.Name = "miAbrir";
            this.miAbrir.Size = new System.Drawing.Size(193, 26);
            this.miAbrir.Text = "Abrir";
            this.miAbrir.Click += new System.EventHandler(this.miAbrir_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardar);
            // 
            // miGuardarComo
            // 
            this.miGuardarComo.Name = "miGuardarComo";
            this.miGuardarComo.Size = new System.Drawing.Size(193, 26);
            this.miGuardarComo.Text = "Guardar como...";
            this.miGuardarComo.Click += new System.EventHandler(this.guardarComo);
            // 
            // miSalir
            // 
            this.miSalir.Name = "miSalir";
            this.miSalir.Size = new System.Drawing.Size(193, 26);
            this.miSalir.Text = "Salir";
            this.miSalir.Click += new System.EventHandler(this.miSalir_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.formatoToolStripMenuItem,
            this.compilarToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1006, 29);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 507);
            this.panel1.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(1006, 507);
            this.splitContainer1.SplitterDistance = 364;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitCode);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(1006, 364);
            this.splitContainer2.SplitterDistance = 650;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitCode
            // 
            this.splitCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCode.Location = new System.Drawing.Point(0, 0);
            this.splitCode.Name = "splitCode";
            // 
            // splitCode.Panel1
            // 
            this.splitCode.Panel1.Controls.Add(this.nLineaBox);
            // 
            // splitCode.Panel2
            // 
            this.splitCode.Panel2.Controls.Add(this.codeBox);
            this.splitCode.Size = new System.Drawing.Size(650, 364);
            this.splitCode.SplitterDistance = 44;
            this.splitCode.TabIndex = 0;
            // 
            // nLineaBox
            // 
            this.nLineaBox.BackColor = System.Drawing.Color.Gray;
            this.nLineaBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nLineaBox.Font = this.codeBox.Font;
            this.nLineaBox.ForeColor = System.Drawing.Color.White;
            this.nLineaBox.Location = new System.Drawing.Point(0, 0);
            this.nLineaBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.nLineaBox.Name = "nLineaBox";
            this.nLineaBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nLineaBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.nLineaBox.Size = new System.Drawing.Size(44, 364);
            this.nLineaBox.TabIndex = 0;
            this.nLineaBox.Text = "";
            this.nLineaBox.WordWrap = false;
            // 
            // codeBox
            // 
            this.codeBox.AcceptsTab = true;
            this.codeBox.BackColor = System.Drawing.Color.White;
            this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeBox.ForeColor = System.Drawing.Color.Black;
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(602, 364);
            this.codeBox.TabIndex = 4;
            this.codeBox.Text = "";
            this.codeBox.WordWrap = false;
            this.codeBox.VScroll += new System.EventHandler(this.codeBox_VScroll);
            this.codeBox.Click += new System.EventHandler(this.code_Click);
            this.codeBox.TextChanged += new System.EventHandler(this.codeBox_TextChanged);
            this.codeBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.code_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(352, 364);
            this.panel3.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(352, 364);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.lexicoBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(344, 335);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Lexico";
            // 
            // lexicoBox
            // 
            this.lexicoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lexicoBox.Location = new System.Drawing.Point(3, 3);
            this.lexicoBox.Name = "lexicoBox";
            this.lexicoBox.Size = new System.Drawing.Size(338, 329);
            this.lexicoBox.TabIndex = 0;
            this.lexicoBox.Text = "";
            this.lexicoBox.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.sintaticoBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(344, 335);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sintactico";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.semanticoBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(344, 335);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Semantico";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.codIntBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(344, 335);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Codigo Intermedio";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1006, 139);
            this.panel4.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(995, 111);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.resultadosBox);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(987, 78);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Resultados";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1006, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.statusLabel.Size = new System.Drawing.Size(991, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Ren: 0, Col: 0";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statusLabel.Click += new System.EventHandler(this.code_Click);
            // 
            // mi_copiar
            // 
            this.mi_copiar.Name = "mi_copiar";
            this.mi_copiar.Size = new System.Drawing.Size(152, 26);
            this.mi_copiar.Text = "Copiar";
            this.mi_copiar.Click += new System.EventHandler(this.copiar_Click);
            // 
            // mi_cortar
            // 
            this.mi_cortar.Name = "mi_cortar";
            this.mi_cortar.Size = new System.Drawing.Size(152, 26);
            this.mi_cortar.Text = "Cortar";
            this.mi_cortar.Click += new System.EventHandler(this.cortar_Click);
            // 
            // mi_pegar
            // 
            this.mi_pegar.Name = "mi_pegar";
            this.mi_pegar.Size = new System.Drawing.Size(152, 26);
            this.mi_pegar.Text = "Pegar";
            this.mi_pegar.Click += new System.EventHandler(this.pegar_Click);
            // 
            // mi_alinNum
            // 
            this.mi_alinNum.Name = "mi_alinNum";
            this.mi_alinNum.Size = new System.Drawing.Size(195, 26);
            this.mi_alinNum.Text = "Alinear numeros";
            this.mi_alinNum.Click += new System.EventHandler(this.codeBox_VScroll);
            // 
            // sintaticoBox
            // 
            this.sintaticoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sintaticoBox.Location = new System.Drawing.Point(3, 3);
            this.sintaticoBox.Name = "sintaticoBox";
            this.sintaticoBox.Size = new System.Drawing.Size(338, 329);
            this.sintaticoBox.TabIndex = 0;
            this.sintaticoBox.Text = "";
            // 
            // semanticoBox
            // 
            this.semanticoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.semanticoBox.Location = new System.Drawing.Point(3, 3);
            this.semanticoBox.Name = "semanticoBox";
            this.semanticoBox.Size = new System.Drawing.Size(338, 329);
            this.semanticoBox.TabIndex = 0;
            this.semanticoBox.Text = "";
            // 
            // codIntBox
            // 
            this.codIntBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codIntBox.Location = new System.Drawing.Point(3, 3);
            this.codIntBox.Name = "codIntBox";
            this.codIntBox.Size = new System.Drawing.Size(338, 329);
            this.codIntBox.TabIndex = 0;
            this.codIntBox.Text = "";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Transparent;
            this.tabPage5.Controls.Add(this.erroresBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(987, 78);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Errores";
            // 
            // erroresBox
            // 
            this.erroresBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.erroresBox.Location = new System.Drawing.Point(3, 3);
            this.erroresBox.Name = "erroresBox";
            this.erroresBox.Size = new System.Drawing.Size(981, 72);
            this.erroresBox.TabIndex = 0;
            this.erroresBox.Text = "";
            // 
            // resultadosBox
            // 
            this.resultadosBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultadosBox.Location = new System.Drawing.Point(3, 3);
            this.resultadosBox.Name = "resultadosBox";
            this.resultadosBox.Size = new System.Drawing.Size(981, 72);
            this.resultadosBox.TabIndex = 0;
            this.resultadosBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1006, 559);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Compilador en C#";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitCode.Panel1.ResumeLayout(false);
            this.splitCode.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCode)).EndInit();
            this.splitCode.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton guardarComoButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton compileButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem acercaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empezarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSalir;
        private System.Windows.Forms.ToolStripMenuItem miAbrir;
        private System.Windows.Forms.ToolStripMenuItem miGuardarComo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.SplitContainer splitCode;
        private System.Windows.Forms.RichTextBox codeBox;
        private System.Windows.Forms.RichTextBox nLineaBox;
        private System.Windows.Forms.RichTextBox lexicoBox;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton nuevoArchivo;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cortar;
        private System.Windows.Forms.ToolStripButton copiar;
        private System.Windows.Forms.ToolStripButton pegar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton acerca;
        private System.Windows.Forms.ToolStripMenuItem mi_alinNum;
        private System.Windows.Forms.ToolStripMenuItem mi_copiar;
        private System.Windows.Forms.ToolStripMenuItem mi_cortar;
        private System.Windows.Forms.ToolStripMenuItem mi_pegar;
        private System.Windows.Forms.RichTextBox sintaticoBox;
        private System.Windows.Forms.RichTextBox semanticoBox;
        private System.Windows.Forms.RichTextBox codIntBox;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RichTextBox erroresBox;
        private System.Windows.Forms.RichTextBox resultadosBox;
    }
}