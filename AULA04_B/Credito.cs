using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AULA04_B
{
    class Credito
    {
        public string Nome { get; }
        public string Email { get; }
        public string RA { get; }
        public Texture2D Foto { get; }

        public Credito (string nome, string email,string RA, Texture2D img)
        {
            this.Nome = nome;
            this.Email = email;
            this.RA = RA;
            this.Foto = img;
        }
    }
}
