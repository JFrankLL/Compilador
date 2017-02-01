using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Compilador {
    delegate void CambiarTextoCallback(RichTextBox richBox, string text);
    public enum ScrollBarType : uint { SbHorz = 0, SbVert = 1, SbCtl = 2, SbBoth = 3 }
    public enum Message : uint { WM_VSCROLL = 0x0115 }
    public enum ScrollBarCommands : uint { SB_THUMBPOSITION = 4 }
    //Class [Ventana]//////////////////////////////////////////////////////////////////////////////////////
    public partial class Ventana : Form{
        [DllImport("User32.dll")]public extern static int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("User32.dll")]public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]public static extern bool LockWindowUpdate(IntPtr hWndLock);//no flash
        //Atributos
        private int indiceChar = 0, ren = 0, col = 0;
        private string ruta = "";
        private bool igual = false;
        private string[] palabras = { "cin", "cout", "if", "then", "else", "repeat", "until", "while", "end", "main", "float", "int", "boolean", "true", "false" };
        //var pal = @"""((\\[^\n]|[^""\n])*)""";
        //var vPal = @"@(""[^""]*"")+";
        private Regex rPal = new Regex("\\b(cin|cout|if|then|else|repeat|until|while|end|main|float|int|boolean|true|false)\\b");
        private Regex rNum = new Regex("\\b[0-9]+(\\.[0-9]+)?");
        private Regex rBlq = new Regex("(\\(|\\)|{|}|;|,)");
        private Regex rOpe = new Regex("<=?|>=?|==|!=|:=|\\+\\+?|\\-\\-?|\\/|\\*");
        private Regex regX = new Regex("\\(|\\) |{|}|;|"+"(cin|cout|if|then|else|repeat|until|while|end|main|float|int|boolean|true|false)");
        private Regex rCmt = new Regex(@"//.*" + @"|/\*(.|[\r\n])*?\*/");
        //private Regex rNum = new Regex("\\b[0-9]+(\\.[0-9]+)?\\b");
        //Constructor
        public Ventana(){
            InitializeComponent();
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", 0, 0);
            nLineaBox.Font = codeBox.Font;
        }
        private void CheckKeyword(string word, Color color, int startIndex) {
            if (codeBox.Text.Contains(word)) {
                int index = -1;
                int selectStart = codeBox.SelectionStart;
                while ((index = codeBox.Text.IndexOf(word, (index + 1))) != -1) {
                    codeBox.Select((index + startIndex), word.Length);
                    codeBox.SelectionColor = color;
                    codeBox.Select(selectStart, 0);
                    codeBox.SelectionColor = Color.White;
                }
            }
        }
        // [Basicos]- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private void Form1_Load(object sender, EventArgs e) {
            string[] palR = { "if", "then", "else", "repeat", "until", "while", "end", "main", "float", "int", "boolean" };
            string[] digs = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "8", "9", "." };
            string[] oper = { ":=", "==", ">", "<", ">=", "<=", "!=", "+", "-", "*", "%", "++", "--", "/" };
            string[] cmts = { "//", "/*", "*/" };
            string[] blks = { "(", ")" };
            string[] blcs = { "{", "}" };
            codeBox_TextChanged(sender, e);
            //nLineaBox.Text = " 1 ";
            /*Thread myThread = new System.Threading.Thread(delegate () {cuentaLineas();});
            myThread.Start();*/
            //for (int i = 1; i <= 1000; i++) nLineaBox.AppendText("  " + i + Environment.NewLine);
            codeBox.Focus();
        }
        private void syntaxRichTextBox1_TextChanged(object sender, EventArgs e) {
            codeBox_TextChanged(sender, e);
        }
        private void codeBox_TextChanged(object sender, EventArgs e){//Area de codigo cambia
            foreach(string s in palabras) CheckKeyword(s, Color.YellowGreen, 0);
            cuentaLineas();
            res(rCmt, Color.Gray);//cmts, ?
            codeBox_VScroll(sender, e);
            /* Pintado - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            //res(regX, Color.DarkOrange, FontStyle.Bold);//palabras reservadas nomas
            res(rPal, Color.YellowGreen, FontStyle.Bold);//palabras reservadas
            
            if (igual) {
                res(rBlq, Color.DodgerBlue);//bloque
                res(rOpe, Color.Violet);//operadores
                res(rNum, Color.Tomato);//numeros
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/
        }
        public void res(Regex regex, Color color, FontStyle s=FontStyle.Regular) {
            try {
                LockWindowUpdate(codeBox.Handle);
                int selPos = codeBox.SelectionStart;
                foreach (Match m in regex.Matches(codeBox.Text)) {
                    codeBox.Select(m.Index, m.Length);
                    //resaltado
                    codeBox.SelectionFont = new Font(codeBox.SelectionFont.FontFamily, codeBox.SelectionFont.Size, s);
                    codeBox.SelectionColor = color;
                    codeBox.SelectionStart = selPos;
                }
            } finally {
                codeBox.DeselectAll();
                LockWindowUpdate(IntPtr.Zero);
                //reset
                codeBox.SelectionFont = new Font(codeBox.SelectionFont.FontFamily, codeBox.SelectionFont.Size, FontStyle.Regular);
                codeBox.SelectionColor = Color.White;
            }
        }
        public void resaltaPal(RichTextBox rchTxtBox, string palabra) {//***X
            int s_start = rchTxtBox.SelectionStart, startIndex = 0, index;
            while ((index = rchTxtBox.Text.IndexOf(palabra, startIndex)) != -1) {
                rchTxtBox.Select(index, palabra.Length);
                //rchTxtBox.SelectionBackColor = Color.Yellow;
                rchTxtBox.SelectionColor = Color.YellowGreen;
                rchTxtBox.SelectionFont = new Font(
                    rchTxtBox.SelectionFont.FontFamily,
                    rchTxtBox.SelectionFont.Size,
                    FontStyle.Bold
                );
                startIndex = index + palabra.Length;
            }
            rchTxtBox.SelectionStart = s_start;
            rchTxtBox.SelectionLength = 0;
            rchTxtBox.SelectionColor = Color.White; rchTxtBox.SelectionFont = new Font(
                     rchTxtBox.SelectionFont.FontFamily,
                     rchTxtBox.SelectionFont.Size,
                     FontStyle.Regular);
            rchTxtBox.ScrollToCaret();
        }
        private void cuentaLineas(){
            nLineaBox.Clear();//Limpia numeros
            if (codeBox.Lines.Length > 1){//n lineas
                for (int i = 1; i <= codeBox.Lines.Length; i++)//pon numeros
                    nLineaBox.AppendText("  " + i + Environment.NewLine);
            }
            else//1 linea
                nLineaBox.AppendText("  " + 1);
        }
        // [Menu] - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void nuevoArch    (object sender, EventArgs e){//[Nuevo archivo]
            ruta = "";//sin guardar
            codeBox.Clear();//limpia codigo
            Text = "Compilador en C# [" + ruta + "]";
        }
        private void miAbrir_Click(object sender, EventArgs e){//[Abrir archivo]
            OpenFileDialog openFile1 = new OpenFileDialog();//ventana grafica de ruta
            openFile1.Filter = "SISH|*.sish;*txt";//extensiones disponibles
            if (openFile1.ShowDialog() == DialogResult.OK)//aceptar//cargar en el area de codigo como string
                codeBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            ruta = openFile1.FileName;//resguardar ruta de archivo
            Text = "Compilador en C# [" + ruta + "]";
        }
        private void guardar      (object sender, EventArgs e){//[Guardar]
            SaveFileDialog dialog = new SaveFileDialog();//ventana
            dialog.Filter = "SISH|*.sish|TXT|*txt";
            dialog.Title = "Save a File";
            if (ruta == "" && dialog.ShowDialog() == DialogResult.OK)// primera vez (guardar como)
                ruta = dialog.FileName;
            if (ruta != ""){
                StreamWriter sw = File.CreateText(ruta);//guardador crea archivo
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);//escribe linea
                sw.Flush();
                sw.Close();//Quitar ultimas lineas (extras)
                if (codeBox.Lines.Length != 0){//Archivo vacio (enti error)
                    FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite);
                    fs.SetLength(fs.Length - 2);
                    fs.Close();
                }
            }
            Text = "Compilador en C# [" + ruta + "]";
        }
        private void guardarComo  (object sender, EventArgs e){//[GuardarComo..]
            ruta = "";
            guardar(sender, e);
        }
        private void cortar_Click (object sender, EventArgs e){//ctrl + x
            codeBox.Copy(); codeBox.SelectedText = "";
        }
        private void copiar_Click (object sender, EventArgs e){//ctrl + c
            codeBox.Copy();
        }
        private void pegar_Click  (object sender, EventArgs e){//ctrl + v
            codeBox.Paste();
        }
        private void miSalir_Click(object sender, EventArgs e){
            Application.Exit();
        }
        private void acerca_Click (object sender, EventArgs e){
            MessageBox.Show("Compilador desarrollado en C#\nParticipantes:\nJose Francisco Lopez Lopez\nLuis Gerardo Gutierrez Castañeda",
                "Acerca de..", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // [Textos] - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void code_Click(object sender, EventArgs e){//[codigo click]
            indiceChar = Int32.Parse(codeBox.SelectionStart.ToString());//carrete
            ren = codeBox.GetLineFromCharIndex(indiceChar);//linea
            col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);//columna
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren + 1, col);//actualiza label
            codeBox_VScroll(sender, e);//sincroniza numeros
        }
        private void code_KeyUp(object sender, KeyEventArgs e){//[codigo keyUp]
            indiceChar = Int32.Parse(codeBox.SelectionStart.ToString());//carrete
            ren = codeBox.GetLineFromCharIndex(indiceChar);//linea
            col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);//columna
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren + 1, col);//actualiza label
            codeBox_VScroll(sender, e);//sincroniza numeros
        }
        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e){
            ruta = "";
            if (MessageBox.Show("Desea Guardar?", "Desea guardar?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                guardar(sender, e);
            codeBox.Clear();
            nLineaBox.Clear();
            cuentaLineas();
            Text = ruta;
        }
        private void codeBox_VScroll(object sender, EventArgs e){//[scroll V]
            int nPos = GetScrollPos(codeBox.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(nLineaBox.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }
        /*****************************************Compilar (Inicio)***************************************/
        private void compilar(object sender, EventArgs e){//compilar
            guardar(sender, e);//respalda trabajo
            if (ruta == "") return;//guardar cancelado
            // RunLexico -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  - 
            AL.Lex_auto lexico = new AL.Lex_auto(ruta);//nuevo lexico
            lexico.run();//analizar
            Lexico_Exited(sender, e);
            // Sintactico   -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  - 
            Sintactico.Sintactico sintaxis = new Sintactico.Sintactico();//nuevo sintax
            sintaxis.run();//analizar
            // Arbol Sintactico   -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
            arbolBox.Nodes.Clear();//limpia arbol
            arbolBox.Nodes.Add(sintaxis.raiz);//agregar al ide, el nuevo arbol
            using (StreamReader sr = new StreamReader("errores_sin.txt")) {
                string s = sr.ReadToEnd();
                erroresBox.Text += (s!="")? "\n -  -  -  -  -  -  -  -  -  -  -  -  - [ERRORES SINTACTICOS] -  -  -  -  -  -  -  -  -  -  -  -  - \n" + s:"";
            }
            // Semantico -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
            Semantico semantico = new Semantico(sintaxis.raiz);//
            semantico.data = dataGridView1;
            //limpia tabla hash
            dataGridView1.Rows.Clear();
            semantico.analizar();//program (codigo)
            semBox.Nodes.Clear();//limpiar
            semBox.Nodes.Add(semantico.raiz);
            semantico.raiz.Expand();
            sintaxis.raiz.Expand();
            erroresBox.Text += (semantico.erroresS!="")? "\n -  -  -  -  -  -  -  -  -  -  -  -  - [ERRORES SEMANTICA] -  -  -  -  -  -  -  -  -  -  -  -  - " + semantico.erroresS:"";
            semantico.data.AutoResizeColumns();
            semantico.data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //color tablahash
            foreach (DataGridViewRow d in dataGridView1.Rows) {
                d.DefaultCellStyle.BackColor = Color.FromArgb(42, 42, 42);
                d.DefaultCellStyle.ForeColor = Color.White;
            }
            erroresBox.ForeColor = Color.Red;
            //generacion de codigo P
            codIntBox.Text = "";//limpia
            if (erroresBox.Text.Length < 1) {//Sin errores de algun tipo
                CodigoP codigo_p = new CodigoP(semantico);
                codigo_p.analizar();
                codIntBox.Text += codigo_p.getInstrucciones();//resultados
                Ejecutadora ejecutadora = new Ejecutadora(codigo_p.instrucciones, semantico.hashTable);
                ejecutadora.setConsola(resultadosBox);
                ejecutadora.ejecutar();
            }else
                codIntBox.Text = "Debido a que existen errores\neste modulo no ha concretado\nsu objetivo.";
        }
        /******************************************Compilar (FIN)*****************************************/
        private void Lexico_Exited(object sender, EventArgs e) {
            using (StreamReader sr = new StreamReader("errores.txt"))
                CambiarTexto(erroresBox, sr.ReadToEnd());
            using (StreamReader sr = new StreamReader("tokens.txt"))
                CambiarTexto(lexicoBox, sr.ReadToEnd());
        }
        private void sintactico_Exited(object sender, EventArgs e) {}
        private void CambiarTexto(RichTextBox rB, string s) {
            if (rB.InvokeRequired)
                Invoke(new CambiarTextoCallback(CambiarTexto), new object[] { rB, s });
            else
                rB.Text = s;
        }
        private void arbolBox_AfterSelect(object sender, TreeViewEventArgs e) {}
        private void semanticoBox_TextChanged(object sender, EventArgs e) {}
        private void semBox_AfterSelect(object sender, TreeViewEventArgs e) {}
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {}
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {}
        private Process nuevoProc(string exe_nom, EventHandler evento) {
            Process sin_Exe = new Process();
            sin_Exe.StartInfo.FileName = exe_nom;
            sin_Exe.StartInfo.Arguments = "\"" + ruta + "\"";
            sin_Exe.EnableRaisingEvents = true;
            sin_Exe.Exited += evento;
            return sin_Exe;
        }
        public void recorrer(Sintactico.Nodo raiz, string identado = "") {//recorre arbol y escribe en archivo txt
            if (raiz == null) return;//no hay arbol
            Console.WriteLine("{0}{1}", identado, raiz.Name);
            foreach (Sintactico.Nodo n in raiz.hijos)
                recorrer(n, identado + "\t");//hijos
        }
    }
}