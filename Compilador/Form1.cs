using System;
using System.IO;
using System.Windows.Forms;

namespace Compilador{
    public partial class Form1 : Form{
        //Atributos
        int indiceChar = 0, ren = 0, col = 0, edoActual=0, charAct=0;
        private string ruta = "", strTkn = "";
        //Constructor
        public Form1(){
            InitializeComponent();
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", 0, 0);
            /*foreach (string line in richTextBox1.Lines) {
                string[] strings = { "if", "else", "for", "do", "while", "int", "float", "double", "string", "short", ";" };
                int n_linea;
                foreach (string s in strings) {
                    if ((n_linea = richTextBox1.Find(s)) > 0) {
                        richTextBox1.Select(n_linea, s.Length);
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    }
                }
            }
            this.richTextBox1.DeselectAll();*/
            nLineaBox.Font = codeBox.Font;
        }
        //////////////////////////////////////////////////////////////////////////////////////
        /// Basicos
        private void Form1_Load(object sender, EventArgs e) {//Evento: ventana cargada
            actualizaLineas();
        }
        private void codeBox_TextChanged(object sender, EventArgs e){//Area de codigo cambia
            actualizaLineas();
        }
        private void actualizaLineas() {//cuenta lineas
            int i = 0;
            nLineaBox.Clear();//limpia area de numeros
            switch (codeBox.Lines.Length) {
                case 0: nLineaBox.AppendText(++i + "\n"); break;//1
                default: foreach (string s in codeBox.Lines) nLineaBox.AppendText(++i + "\n"); break;// n
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        /// Menu 
        private void miAbrir_Click(object sender, EventArgs e){//Abrir archivo
            OpenFileDialog openFile1 = new OpenFileDialog();//ventana grafica de ruta
            openFile1.Filter = "SISH|*.sish;*txt";//extensiones disponibles
            if (openFile1.ShowDialog() == DialogResult.OK)//aceptar//cargar en el area de codigo como string
                codeBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            ruta = openFile1.FileName;//resguardar ruta de archivo
        }
        private void guardarComo(object sender, EventArgs e) {//GuardarComo..
            ruta = "";
            guardar(sender, e);
        }
        private void guardar(object sender, EventArgs e) {//Guardar
            SaveFileDialog dialog = new SaveFileDialog();//ventana
            dialog.Filter = "SISH|*.sish|TXT|*txt";
            dialog.Title = "Save a File";
            if (ruta == "" && dialog.ShowDialog() == DialogResult.OK)// primera vez (guardar como)
                ruta = dialog.FileName;
            if (ruta != "") {
                StreamWriter sw = File.CreateText(ruta);//guardador crea archivo
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);//escribe linea
                sw.Flush();
                sw.Close();//Quitar ultimas lineas (extras)
                FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite);
                fs.SetLength(fs.Length - 2);
                fs.Close();
            }
        }
        private void miSalir_Click(object sender, EventArgs e) { Application.Exit(); }
        ///////////////////////////////////////////////////////////////////////////////////
        ///  Textos
        private void code_Click(object sender, EventArgs e) {
            indiceChar = Int32.Parse(codeBox.SelectionStart.ToString());
            ren = codeBox.GetLineFromCharIndex(indiceChar);
            col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren + 1, col);
            //actualizaLineas();
        }
        private void codeBox_scrollV(Message msg) {
            msg.HWnd = nLineaBox.Handle;
            nLineaBox.PubWndProc(ref msg);
        }
        /*****************************************************************************************
                                               Compilar (Inicio)                                */
        private void compilar(object sender, EventArgs e) {//compilar
            guardar(sender, e);//respalda trabajo
            if (ruta != "") {//Si guardado
                lexicoBox.Text = "";//limpia lista de tokens
                edoActual = 0;//inicio
                charAct = 0;//cuenta char
                while (true) if (!autmataLexico(leerChar(++charAct))) break;//leer todos los char
            }
        }
        /*                                    Compilar (FIN)
        ******************************************************************************************/
        
        /// Compilacion  //// Va pa' juarez (ignorar en caso de ver esto)
        private char leerChar(int indice) {
            char c = '`';
            if (ruta != "") {
                StreamReader sr = new StreamReader(ruta);
                /*string linea;
                int i = 0;
                while (!sr.EndOfStream) {
                    linea = sr.ReadLine();
                    foreach (char c in linea.ToCharArray())
                        if (++i == indice) {
                            sr.Close();
                            return c;
                        }
                }*/
                for(int i=0; i<indice; i++)
                    c = (char)sr.Read();
                sr.Close();
            }
            return c;
        }
        private bool autmataLexico(char entrada) {//Falta hacer el retorno del estado final = TRUE
            Console.WriteLine("Automata, edo: {0}, in: {1}", edoActual, entrada);
            switch (edoActual) {
                case 0://Estado inicial
                    if (entrada >= '0' && entrada <= '9') {
                        edoActual = 1;
                        strTkn += entrada;
                        return true;
                    } else if (entrada == '_' || (entrada >= 'a' && entrada <= 'z') || (entrada >= 'A' && entrada <= 'Z')) {
                        edoActual = 2;
                        strTkn += entrada;
                        return true;
                    } else
                        return true;
                case 1://Puro numer
                    if (entrada >= '0' && entrada <= '9') {
                        edoActual = 1;
                        strTkn += entrada;
                        return true;
                    } else if (entrada == '_' || (entrada >= 'a' && entrada <= 'z') || (entrada >= 'A' && entrada <= 'Z')) {
                        edoActual = 2;
                        lexicoBox.AppendText(strTkn+"\n");
                        strTkn = "";
                        strTkn += entrada;
                        return true;
                    } else {
                        lexicoBox.AppendText(strTkn + "\n");
                        strTkn = "";
                        edoActual = 20;
                        return true;
                    }
                case 2://Identificadores
                    if (entrada == '_' || (entrada >= 'a' && entrada <= 'z') || (entrada >= 'A' && entrada <= 'Z')) {
                        edoActual = 2;
                        strTkn += entrada;
                        return true;
                    }else if (entrada >= '0' && entrada <= '9') {
                        edoActual = 1;
                        lexicoBox.AppendText(strTkn + "\n");
                        strTkn = "";
                        strTkn += entrada;
                        return true;
                    } else {
                        edoActual = 0;
                        lexicoBox.AppendText(strTkn + "\n");
                        strTkn = "";
                        return true;
                    }
                default:
                    return false;
            }
        }
        /// FIN Compilacion
    }
    ////////////////////////////////////////////////////////////////////
    ///                       Clases Graficas                        ///
    ////////////////////////////////////////////////////////////////////
    public class SynchronizedScrollRichTextBox : RichTextBox {
        public event vScrollEventHandler vScroll;
        public delegate void vScrollEventHandler(Message message);

        public const int WM_VSCROLL = 0x115;

        protected override void WndProc(ref Message msg) {
            if (msg.Msg == WM_VSCROLL)
                if (vScroll != null)
                    vScroll(msg);
            base.WndProc(ref msg);
        }

        public void PubWndProc(ref Message msg) {
            base.WndProc(ref msg);
        }
    }
}