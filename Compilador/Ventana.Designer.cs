using System.Drawing;
using System.Windows.Forms;

namespace Compilador
{
    partial class Ventana
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guardarComoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.compileButton = new System.Windows.Forms.ToolStripButton();
            this.barraHerramientas = new System.Windows.Forms.ToolStrip();
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
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_copiar = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_cortar = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_pegar = new System.Windows.Forms.ToolStripMenuItem();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.miSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.barraMenu = new System.Windows.Forms.MenuStrip();
            this.barraStatus = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitCode = new System.Windows.Forms.SplitContainer();
            this.nLineaBox = new System.Windows.Forms.RichTextBox();
            this.codeBox = new System.Windows.Forms.RichTextBox();
            this.tabsDerecha = new System.Windows.Forms.TabControl();
            this.tabLexico = new System.Windows.Forms.TabPage();
            this.lexicoBox = new System.Windows.Forms.RichTextBox();
            this.tabSemantico = new System.Windows.Forms.TabPage();
            this.semBox = new Compilador.BoxArbol();
            this.tabSintatico = new System.Windows.Forms.TabPage();
            this.arbolBox = new Compilador.BoxArbol();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabCodInt = new System.Windows.Forms.TabPage();
            this.codIntBox = new System.Windows.Forms.RichTextBox();
            this.tabsAbajo = new System.Windows.Forms.TabControl();
            this.tabErrores = new System.Windows.Forms.TabPage();
            this.erroresBox = new System.Windows.Forms.RichTextBox();
            this.tabResultados = new System.Windows.Forms.TabPage();
            this.resultadosBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.barraHerramientas.SuspendLayout();
            this.barraMenu.SuspendLayout();
            this.barraStatus.SuspendLayout();
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
            this.tabsDerecha.SuspendLayout();
            this.tabLexico.SuspendLayout();
            this.tabSemantico.SuspendLayout();
            this.tabSintatico.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabCodInt.SuspendLayout();
            this.tabsAbajo.SuspendLayout();
            this.tabErrores.SuspendLayout();
            this.tabResultados.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // barraHerramientas
            // 
            this.barraHerramientas.BackColor = System.Drawing.Color.Transparent;
            this.barraHerramientas.Dock = System.Windows.Forms.DockStyle.None;
            this.barraHerramientas.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.barraHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.barraHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.barraHerramientas.Location = new System.Drawing.Point(424, 4);
            this.barraHerramientas.Name = "barraHerramientas";
            this.barraHerramientas.Size = new System.Drawing.Size(226, 23);
            this.barraHerramientas.TabIndex = 4;
            this.barraHerramientas.Text = "toolStrip1";
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
            this.acercaToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.acercaToolStripMenuItem.Text = "Acerca";
            this.acercaToolStripMenuItem.Click += new System.EventHandler(this.acerca_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaToolStripMenuItem});
            this.ayudaToolStripMenuItem.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(67, 23);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // empezarToolStripMenuItem
            // 
            this.empezarToolStripMenuItem.Name = "empezarToolStripMenuItem";
            this.empezarToolStripMenuItem.Size = new System.Drawing.Size(144, 24);
            this.empezarToolStripMenuItem.Text = "Empezar";
            this.empezarToolStripMenuItem.Click += new System.EventHandler(this.compilar);
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.compilarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empezarToolStripMenuItem});
            this.compilarToolStripMenuItem.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compilarToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(87, 23);
            this.compilarToolStripMenuItem.Text = "Compilar";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_copiar,
            this.mi_cortar,
            this.mi_pegar});
            this.editarToolStripMenuItem.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editarToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // mi_copiar
            // 
            this.mi_copiar.Name = "mi_copiar";
            this.mi_copiar.Size = new System.Drawing.Size(126, 24);
            this.mi_copiar.Text = "Copiar";
            this.mi_copiar.Click += new System.EventHandler(this.copiar_Click);
            // 
            // mi_cortar
            // 
            this.mi_cortar.Name = "mi_cortar";
            this.mi_cortar.Size = new System.Drawing.Size(126, 24);
            this.mi_cortar.Text = "Cortar";
            this.mi_cortar.Click += new System.EventHandler(this.cortar_Click);
            // 
            // mi_pegar
            // 
            this.mi_pegar.Name = "mi_pegar";
            this.mi_pegar.Size = new System.Drawing.Size(126, 24);
            this.mi_pegar.Text = "Pegar";
            this.mi_pegar.Click += new System.EventHandler(this.pegar_Click);
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbrir,
            this.cerrarToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.miGuardarComo,
            this.miSalir});
            this.archivoToolStripMenuItem.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.archivoToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(76, 23);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // miAbrir
            // 
            this.miAbrir.Name = "miAbrir";
            this.miAbrir.Size = new System.Drawing.Size(201, 24);
            this.miAbrir.Text = "Abrir";
            this.miAbrir.Click += new System.EventHandler(this.miAbrir_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardar);
            // 
            // miGuardarComo
            // 
            this.miGuardarComo.Name = "miGuardarComo";
            this.miGuardarComo.Size = new System.Drawing.Size(201, 24);
            this.miGuardarComo.Text = "Guardar como...";
            this.miGuardarComo.Click += new System.EventHandler(this.guardarComo);
            // 
            // miSalir
            // 
            this.miSalir.Name = "miSalir";
            this.miSalir.Size = new System.Drawing.Size(201, 24);
            this.miSalir.Text = "Salir";
            this.miSalir.Click += new System.EventHandler(this.miSalir_Click);
            // 
            // barraMenu
            // 
            this.barraMenu.BackColor = System.Drawing.Color.Transparent;
            this.barraMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.barraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.compilarToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.barraMenu.Location = new System.Drawing.Point(0, 0);
            this.barraMenu.Name = "barraMenu";
            this.barraMenu.Size = new System.Drawing.Size(973, 27);
            this.barraMenu.TabIndex = 3;
            this.barraMenu.Text = "menuStrip1";
            // 
            // barraStatus
            // 
            this.barraStatus.BackColor = System.Drawing.Color.Transparent;
            this.barraStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.barraStatus.Location = new System.Drawing.Point(0, 125);
            this.barraStatus.Name = "barraStatus";
            this.barraStatus.Size = new System.Drawing.Size(973, 22);
            this.barraStatus.SizingGrip = false;
            this.barraStatus.TabIndex = 6;
            this.barraStatus.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.statusLabel.Size = new System.Drawing.Size(958, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Ren: 0, Col: 0";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statusLabel.Click += new System.EventHandler(this.code_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.tabsAbajo);
            this.splitContainer1.Panel2.Controls.Add(this.barraStatus);
            this.splitContainer1.Size = new System.Drawing.Size(973, 532);
            this.splitContainer1.SplitterDistance = 381;
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
            this.splitContainer2.Panel2.Controls.Add(this.tabsDerecha);
            this.splitContainer2.Size = new System.Drawing.Size(973, 381);
            this.splitContainer2.SplitterDistance = 650;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitCode
            // 
            this.splitCode.CausesValidation = false;
            this.splitCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCode.Location = new System.Drawing.Point(0, 0);
            this.splitCode.Name = "splitCode";
            // 
            // splitCode.Panel1
            // 
            this.splitCode.Panel1.Controls.Add(this.nLineaBox);
            this.splitCode.Panel1MinSize = 50;
            // 
            // splitCode.Panel2
            // 
            this.splitCode.Panel2.Controls.Add(this.codeBox);
            this.splitCode.Panel2MinSize = 590;
            this.splitCode.Size = new System.Drawing.Size(650, 381);
            this.splitCode.SplitterDistance = 51;
            this.splitCode.SplitterWidth = 1;
            this.splitCode.TabIndex = 1;
            // 
            // nLineaBox
            // 
            this.nLineaBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.nLineaBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nLineaBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nLineaBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nLineaBox.ForeColor = System.Drawing.Color.Orange;
            this.nLineaBox.Location = new System.Drawing.Point(0, 0);
            this.nLineaBox.Name = "nLineaBox";
            this.nLineaBox.ReadOnly = true;
            this.nLineaBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nLineaBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.nLineaBox.Size = new System.Drawing.Size(51, 381);
            this.nLineaBox.TabIndex = 3;
            this.nLineaBox.Text = "";
            this.nLineaBox.WordWrap = false;
            // 
            // codeBox
            // 
            this.codeBox.AcceptsTab = true;
            this.codeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.codeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeBox.ForeColor = System.Drawing.Color.White;
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(598, 381);
            this.codeBox.TabIndex = 1;
            this.codeBox.Text = "";
            this.codeBox.WordWrap = false;
            this.codeBox.VScroll += new System.EventHandler(this.codeBox_VScroll);
            this.codeBox.Click += new System.EventHandler(this.code_Click);
            this.codeBox.TextChanged += new System.EventHandler(this.codeBox_TextChanged);
            this.codeBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.code_KeyUp);
            // 
            // tabsDerecha
            // 
            this.tabsDerecha.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabsDerecha.CausesValidation = false;
            this.tabsDerecha.Controls.Add(this.tabLexico);
            this.tabsDerecha.Controls.Add(this.tabSemantico);
            this.tabsDerecha.Controls.Add(this.tabSintatico);
            this.tabsDerecha.Controls.Add(this.tabPage1);
            this.tabsDerecha.Controls.Add(this.tabCodInt);
            this.tabsDerecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsDerecha.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabsDerecha.HotTrack = true;
            this.tabsDerecha.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabsDerecha.Location = new System.Drawing.Point(0, 0);
            this.tabsDerecha.Margin = new System.Windows.Forms.Padding(0);
            this.tabsDerecha.Multiline = true;
            this.tabsDerecha.Name = "tabsDerecha";
            this.tabsDerecha.Padding = new System.Drawing.Point(0, 0);
            this.tabsDerecha.SelectedIndex = 0;
            this.tabsDerecha.Size = new System.Drawing.Size(319, 381);
            this.tabsDerecha.TabIndex = 4;
            // 
            // tabLexico
            // 
            this.tabLexico.BackColor = System.Drawing.Color.Transparent;
            this.tabLexico.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabLexico.Controls.Add(this.lexicoBox);
            this.tabLexico.Location = new System.Drawing.Point(4, 4);
            this.tabLexico.Name = "tabLexico";
            this.tabLexico.Padding = new System.Windows.Forms.Padding(3);
            this.tabLexico.Size = new System.Drawing.Size(267, 373);
            this.tabLexico.TabIndex = 0;
            this.tabLexico.Text = "Lexico";
            // 
            // lexicoBox
            // 
            this.lexicoBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lexicoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lexicoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lexicoBox.ForeColor = System.Drawing.Color.White;
            this.lexicoBox.Location = new System.Drawing.Point(3, 3);
            this.lexicoBox.Name = "lexicoBox";
            this.lexicoBox.ReadOnly = true;
            this.lexicoBox.Size = new System.Drawing.Size(261, 367);
            this.lexicoBox.TabIndex = 1;
            this.lexicoBox.Text = "";
            // 
            // tabSemantico
            // 
            this.tabSemantico.Controls.Add(this.semBox);
            this.tabSemantico.Location = new System.Drawing.Point(4, 4);
            this.tabSemantico.Name = "tabSemantico";
            this.tabSemantico.Padding = new System.Windows.Forms.Padding(3);
            this.tabSemantico.Size = new System.Drawing.Size(267, 373);
            this.tabSemantico.TabIndex = 2;
            this.tabSemantico.Text = "Sintactico";
            this.tabSemantico.UseVisualStyleBackColor = true;
            // 
            // semBox
            // 
            this.semBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.semBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.semBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.semBox.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.semBox.ForeColor = System.Drawing.Color.White;
            this.semBox.Indent = 20;
            this.semBox.ItemHeight = 25;
            this.semBox.Location = new System.Drawing.Point(3, 3);
            this.semBox.Name = "semBox";
            this.semBox.Size = new System.Drawing.Size(261, 367);
            this.semBox.TabIndex = 0;
            // 
            // tabSintatico
            // 
            this.tabSintatico.Controls.Add(this.arbolBox);
            this.tabSintatico.Location = new System.Drawing.Point(4, 4);
            this.tabSintatico.Name = "tabSintatico";
            this.tabSintatico.Padding = new System.Windows.Forms.Padding(3);
            this.tabSintatico.Size = new System.Drawing.Size(267, 373);
            this.tabSintatico.TabIndex = 1;
            this.tabSintatico.Text = "Semantico";
            this.tabSintatico.UseVisualStyleBackColor = true;
            // 
            // arbolBox
            // 
            this.arbolBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.arbolBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arbolBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arbolBox.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arbolBox.ForeColor = System.Drawing.Color.White;
            this.arbolBox.Indent = 20;
            this.arbolBox.ItemHeight = 25;
            this.arbolBox.Location = new System.Drawing.Point(3, 3);
            this.arbolBox.Name = "arbolBox";
            this.arbolBox.Size = new System.Drawing.Size(261, 367);
            this.arbolBox.TabIndex = 3;
            this.arbolBox.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.arbolBox_AfterSelect);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(267, 373);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Tabla Hash";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(267, 373);
            this.dataGridView1.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Lexema";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "#";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Lineas";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // tabCodInt
            // 
            this.tabCodInt.Controls.Add(this.codIntBox);
            this.tabCodInt.Location = new System.Drawing.Point(4, 4);
            this.tabCodInt.Name = "tabCodInt";
            this.tabCodInt.Padding = new System.Windows.Forms.Padding(3);
            this.tabCodInt.Size = new System.Drawing.Size(289, 373);
            this.tabCodInt.TabIndex = 3;
            this.tabCodInt.Text = "Codigo";
            this.tabCodInt.UseVisualStyleBackColor = true;
            // 
            // codIntBox
            // 
            this.codIntBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.codIntBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codIntBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codIntBox.ForeColor = System.Drawing.Color.White;
            this.codIntBox.Location = new System.Drawing.Point(3, 3);
            this.codIntBox.Name = "codIntBox";
            this.codIntBox.ReadOnly = true;
            this.codIntBox.Size = new System.Drawing.Size(283, 367);
            this.codIntBox.TabIndex = 0;
            this.codIntBox.Text = "";
            // 
            // tabsAbajo
            // 
            this.tabsAbajo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsAbajo.CausesValidation = false;
            this.tabsAbajo.Controls.Add(this.tabErrores);
            this.tabsAbajo.Controls.Add(this.tabResultados);
            this.tabsAbajo.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabsAbajo.Location = new System.Drawing.Point(0, -2);
            this.tabsAbajo.Name = "tabsAbajo";
            this.tabsAbajo.SelectedIndex = 0;
            this.tabsAbajo.Size = new System.Drawing.Size(973, 124);
            this.tabsAbajo.TabIndex = 4;
            // 
            // tabErrores
            // 
            this.tabErrores.BackColor = System.Drawing.Color.Transparent;
            this.tabErrores.Controls.Add(this.erroresBox);
            this.tabErrores.Location = new System.Drawing.Point(4, 29);
            this.tabErrores.Name = "tabErrores";
            this.tabErrores.Padding = new System.Windows.Forms.Padding(3);
            this.tabErrores.Size = new System.Drawing.Size(965, 91);
            this.tabErrores.TabIndex = 0;
            this.tabErrores.Text = "Errores";
            // 
            // erroresBox
            // 
            this.erroresBox.BackColor = System.Drawing.Color.Black;
            this.erroresBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.erroresBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.erroresBox.ForeColor = System.Drawing.Color.White;
            this.erroresBox.Location = new System.Drawing.Point(3, 3);
            this.erroresBox.Name = "erroresBox";
            this.erroresBox.ReadOnly = true;
            this.erroresBox.Size = new System.Drawing.Size(959, 85);
            this.erroresBox.TabIndex = 0;
            this.erroresBox.Text = "";
            // 
            // tabResultados
            // 
            this.tabResultados.Controls.Add(this.resultadosBox);
            this.tabResultados.Location = new System.Drawing.Point(4, 29);
            this.tabResultados.Name = "tabResultados";
            this.tabResultados.Padding = new System.Windows.Forms.Padding(3);
            this.tabResultados.Size = new System.Drawing.Size(965, 91);
            this.tabResultados.TabIndex = 1;
            this.tabResultados.Text = "Resultados";
            this.tabResultados.UseVisualStyleBackColor = true;
            // 
            // resultadosBox
            // 
            this.resultadosBox.BackColor = System.Drawing.Color.Black;
            this.resultadosBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultadosBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultadosBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultadosBox.ForeColor = System.Drawing.Color.White;
            this.resultadosBox.Location = new System.Drawing.Point(3, 3);
            this.resultadosBox.Name = "resultadosBox";
            this.resultadosBox.ReadOnly = true;
            this.resultadosBox.Size = new System.Drawing.Size(959, 85);
            this.resultadosBox.TabIndex = 0;
            this.resultadosBox.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 532);
            this.panel1.TabIndex = 5;
            // 
            // Ventana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(973, 559);
            this.Controls.Add(this.barraHerramientas);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barraMenu);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 10);
            this.MainMenuStrip = this.barraMenu;
            this.Name = "Ventana";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Compilador en C#";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.barraHerramientas.ResumeLayout(false);
            this.barraHerramientas.PerformLayout();
            this.barraMenu.ResumeLayout(false);
            this.barraMenu.PerformLayout();
            this.barraStatus.ResumeLayout(false);
            this.barraStatus.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
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
            this.tabsDerecha.ResumeLayout(false);
            this.tabLexico.ResumeLayout(false);
            this.tabSemantico.ResumeLayout(false);
            this.tabSintatico.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabCodInt.ResumeLayout(false);
            this.tabsAbajo.ResumeLayout(false);
            this.tabErrores.ResumeLayout(false);
            this.tabResultados.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolStripButton guardarComoButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton compileButton;
        private ToolStrip barraHerramientas;
        private ToolStripMenuItem acercaToolStripMenuItem;
        private ToolStripMenuItem ayudaToolStripMenuItem;
        private ToolStripMenuItem empezarToolStripMenuItem;
        private ToolStripMenuItem compilarToolStripMenuItem;
        private ToolStripMenuItem editarToolStripMenuItem;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private MenuStrip barraMenu;
        private ToolStripMenuItem miSalir;
        private ToolStripMenuItem miAbrir;
        private ToolStripMenuItem miGuardarComo;
        private StatusStrip barraStatus;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private ToolStripButton nuevoArchivo;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton cortar;
        private ToolStripButton copiar;
        private ToolStripButton pegar;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton acerca;
        private ToolStripMenuItem mi_copiar;
        private ToolStripMenuItem mi_cortar;
        private ToolStripMenuItem mi_pegar;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitCode;
        private RichTextBox codeBox;
        private TabControl tabsDerecha;
        private TabPage tabLexico;
        private TabPage tabSintatico;
        private TabPage tabSemantico;
        private TabPage tabCodInt;
        private RichTextBox codIntBox;
        private TabControl tabsAbajo;
        private TabPage tabErrores;
        private RichTextBox erroresBox;
        private TabPage tabResultados;
        private RichTextBox resultadosBox;
        private Panel panel1;
        private RichTextBox nLineaBox;
        private ToolStripStatusLabel statusLabel;
        private ToolStripMenuItem cerrarToolStripMenuItem;
        private RichTextBox lexicoBox;
        private BoxArbol arbolBox;
        private BoxArbol semBox;
        private TabPage tabPage1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}