using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Compilador{
    public partial class Form1 : Form{

        int indiceChar = 0, ren = 0, col = 0, edoActual=0, charAct=0;
        private string ruta = "", strTkn = "";

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

        private void richTextBox1_TextChanged(object sender, EventArgs e){}

        private void Form1_Load(object sender, EventArgs e) {
            actualizaLineas();
        }

        private void codeBox_TextChanged(object sender, EventArgs e){
            actualizaLineas();
            this.nLineaBox.ScrollToCaret();
        }
        ///
        /// Botones Archivo menu 
        ///
        private void miAbrir_Click(object sender, EventArgs e){
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "SISH|*.sish;*txt";
            if (openFile1.ShowDialog() == DialogResult.OK)
                codeBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            ruta = openFile1.FileName;
        }
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e) {
            if (ruta != "") {
                StreamWriter sw = File.CreateText(ruta);
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);
                sw.Flush();
                sw.Close();
                FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite);
                fs.SetLength(fs.Length - 2);
                fs.Close();
            } else
                miGuardarComo_Click(sender, e);//Talvez guarde, o no.
        }
        private  void miGuardarComo_Click(object sender, EventArgs e){
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "SISH|*.sish|TXT|*txt";
            dialog.Title = "Save a File";
            if (dialog.ShowDialog() == DialogResult.OK) {//Si guardo
                StreamWriter sw = File.CreateText(ruta);
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);
                sw.Flush();
                sw.Close();
                FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite);
                fs.SetLength(fs.Length - 2);
                fs.Close();
                ruta = dialog.FileName;
            }
            ruta = "";
        }
        private void miSalir_Click(object sender, EventArgs e){
            Application.Exit();
        }
        /// Fin botones archivo
        
        private void code_Click(object sender, EventArgs e) {
            indiceChar = Int32.Parse(codeBox.SelectionStart.ToString());
            ren = codeBox.GetLineFromCharIndex(indiceChar);
            col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren+1, col);
            
        }

        private void actualizaLineas() {
            int i = 0;
            nLineaBox.Clear();
            foreach (string s in codeBox.Lines)
                nLineaBox.AppendText(++i + "\n");
            nLineaBox.ScrollToCaret();
        }
        /// 
        /// Compilacion
        ///

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
            Console.WriteLine("Automata, edo: {0}, in: {1}", this.edoActual, entrada);
            switch (this.edoActual) {
                case 0://Estado inicial
                    if (entrada >= '0' && entrada <= '9') {
                        this.edoActual = 1;
                        strTkn += entrada;
                        return true;
                    } else if (entrada == '_' || (entrada >= 'a' && entrada <= 'z') || (entrada >= 'A' && entrada <= 'Z')) {
                        this.edoActual = 2;
                        strTkn += entrada;
                        return true;
                    } else
                        return true;
                case 1://Puro numer
                    if (entrada >= '0' && entrada <= '9') {
                        this.edoActual = 1;
                        strTkn += entrada;
                        return true;
                    } else if (entrada == '_' || (entrada >= 'a' && entrada <= 'z') || (entrada >= 'A' && entrada <= 'Z')) {
                        this.edoActual = 2;
                        this.lexicoBox.AppendText(strTkn+"\n");
                        strTkn = "";
                        strTkn += entrada;
                        return true;
                    } else {
                        this.lexicoBox.AppendText(strTkn + "\n");
                        strTkn = "";
                        this.edoActual = 20;
                        return true;
                    }
                case 2://Identificadores
                    if (entrada == '_' || (entrada >= 'a' && entrada <= 'z') || (entrada >= 'A' && entrada <= 'Z')) {
                        this.edoActual = 2;
                        strTkn += entrada;
                        return true;
                    }else if (entrada >= '0' && entrada <= '9') {
                        this.edoActual = 1;
                        lexicoBox.AppendText(strTkn + "\n");
                        strTkn = "";
                        strTkn += entrada;
                        return true;
                    } else {
                        this.edoActual = 0;
                        lexicoBox.AppendText(strTkn + "\n");
                        strTkn = "";
                        return true;
                    }
                default:
                    return false;
            }
        }
        //Empezar accion
        private void empezarToolStripMenuItem_Click(object sender, EventArgs e) {
            guardarToolStripMenuItem_Click(sender, e);
            if(ruta != "") {
               /* lexicoBox.Text = "";
                this.edoActual = 0;
                charAct = 0;
                while (true) if (!autmataLexico(leerChar(++charAct))) break;
                Console.WriteLine("Chars: " + charAct);*/
                Console.WriteLine(leerChar(7));
            }
        }
        /// FIN Compilacion
    }
}