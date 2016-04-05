using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Compilador{
    public partial class Form1 : Form{
        public Form1(){
            InitializeComponent();
            this.statusLabel.Text = string.Format("Ren: {0}, Col: {1}", 0, 0);
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
            this.nLineaBox.Font = this.codeBox.Font;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e){

            Console.WriteLine("Codigo cambiado");
        }

        private void Form1_Load(object sender, EventArgs e) {
            actualizaLineas();
            Console.WriteLine("Cargado");
        }

        private void codeBox_TextChanged(object sender, EventArgs e){
            actualizaLineas();
        }

        private void miAbrir_Click(object sender, EventArgs e){
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "SISH|*.sish;*txt";
            if (openFile1.ShowDialog() == DialogResult.OK)
                codeBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
        }
        private void miGuardar_Click(object sender, EventArgs e){
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "SISH|*.sish|TXT|*txt";
            dialog.Title = "Save a File";
            if (dialog.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = File.CreateText(dialog.FileName);
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);
                sw.Flush();
                sw.Close();
            }
        }
        private void miSalir_Click(object sender, EventArgs e){
            Application.Exit();
        }

        private void code_Click(object sender, EventArgs e) {
            int indiceChar = Int32.Parse(codeBox.SelectionStart.ToString()),
                ren = codeBox.GetLineFromCharIndex(indiceChar),
                col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);
            this.statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren+1, col);
        }

        private void actualizaLineas() {
            int i = 0;
            this.nLineaBox.Clear();
            foreach (string s in this.codeBox.Lines)
                this.nLineaBox.AppendText(++i + "\n");
            this.nLineaBox.ScrollToCaret();
        }
    }
}
