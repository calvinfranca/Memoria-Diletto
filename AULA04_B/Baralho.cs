using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AULA04_B
{
    class Baralho
    {
        public List<Carta> cartas;
        int Colunas;
        int Linhas;
        public Baralho(int _lin,int _col)
        {
            this.cartas = new List<Carta>();
            this.Colunas = _col;
            this.Linhas = _lin;
            int nPares = (_col * _lin) / 2;
            int nPar =0;
            for(int lin = 0; lin < Linhas; lin++)
            {
                for(int col = 0; col < Colunas; col++)
                {
                    if ((Colunas * lin + col) % 2 == 0) { nPar = nPar + 1; }

                    Carta carta1 =
                  new Carta(col,lin, 164, 150, nPar, Colunas * lin + col);


                    cartas.Add(carta1);

                }
            }
            this.Embaralhar();
        }

        public void Embaralhar()
        {
            Random rnd = new Random();
            List<Carta> aux = new List<Carta>();
            int lin = 0;
            int col = 0;
            while (cartas.Count > 0)
            {
                int i = rnd.Next(0, cartas.Count);
                Carta c = cartas[i];
                c.X = col;
                c.Y = lin;
                aux.Add(c);
                cartas.RemoveAt(i);
                col++;
                if (col >= Colunas)
                {
                    col = 0;
                    lin++;
                }
            }
            cartas = aux;
        }

    }
}
