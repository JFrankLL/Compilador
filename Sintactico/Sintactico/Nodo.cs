using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sintactico {
    // Clase para generar arbol
    public class Nodo : TreeNode{
        //  [Atributos]
        private Token token { set; get; }
        public List<Nodo> hijos = new List<Nodo>();//nodos hijos
        public List<int> lineas = new List<int>();//lineas donde aparece
        public string comentario;
        public nd_t ntype { set; get; }
        public tk_t dtype { set; get; }
        public double val;

        //  [Constructor]
        public Nodo(string comentario="") {
            token = new Token("null","vacio");
            this.comentario = comentario;
            Text = Name = "vacio";
        }
        public Nodo(Token token) {
            this.token = token;
            comentario = token.tipo.ToString();
            Text = Name = token.ToString();//Lo que se muestra y como
            ForeColor = this.token.getTipoColor();//color
            dtype = token.tipo;
            val = 0;
            if (token.tipo == tk_t.N_INT)
                ntype = nd_t.INT;
            else if(token.tipo == tk_t.N_FLOAT)
                ntype = nd_t.FLOAT;
        }
        public Nodo(Nodo n) {//Copia
            token = n.token;
            hijos = n.hijos;
            comentario = n.comentario;
            ntype = n.ntype;
            dtype = n.dtype;
            val = 0;
            Text = Name = n.Text;
            armar(this);
            ForeColor = this.token.getTipoColor();//color
        }

        public void armar(Nodo t) {
            for(int i=0; i<t.hijos.Count; i++)
                t.Nodes.Add(new Nodo(t.hijos[i]));
        }

        //  [Metodos]
        public void setHijo(Nodo h) {
            this.Nodes.Add(h);
            hijos.Add(h);
            h.ForeColor = h.token.getTipoColor();//color
        }
        public void setToken(Token t) {
            this.token = t;
            this.Text = t.ToString();
            ForeColor = t.getTipoColor();//color
        }
        public Token getToken() { return this.token; }
        public tk_t getTipo() { return token.tipo; }
        public void setTipo(tk_t tipo) { token.tipo=tipo; }
        public string getValor() {
            if (ntype == nd_t.FLOAT)
                return val.ToString("0.0#########");
            return val.ToString();
        }
    }
}

