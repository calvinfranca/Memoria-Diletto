using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AULA04_B
{

    class DrawObjetos
    {
        
        public static void DrawCarta(Carta crt, SpriteBatch sp,
                                    Texture2D imgCosta,
                                    Texture2D imgFrente)
        {
            Rectangle rect = new Rectangle(crt.X*crt.Largura+ 70, crt.Y*crt.Altura+ 200,
                                          crt.Largura, crt.Altura);
            //Vector2 posTexto = new Vector2(crt.X + 20, crt.Y + 15);
            Texture2D img = imgCosta;
            if (crt.Clicada == true) { img = imgFrente; }

            sp.Draw(img, rect, Color.White);
           
        }

        public static void DrawBotao(Botao btn, SpriteBatch sp,
                                    Texture2D textura,
                                    SpriteFont fonte)
        {
            Rectangle rect = new Rectangle(btn.X, btn.Y,
                                          btn.Largura, btn.Altura);
            Vector2 posTexto = new Vector2(btn.X + 40, btn.Y + 15);
            Color cor = btn.Cor;
            if (btn.Clicado == true) { cor = Color.Gray; }
            sp.Draw(textura, rect, cor);
            sp.DrawString(fonte, btn.Texto, posTexto, Color.White);
        }

        public static void Credito(Credito cred, SpriteBatch sp,SpriteFont fonte)
        {
            Vector2 posNome = new Vector2(200, 20);
            Vector2 posEmail = new Vector2(200, 50);
            Vector2 posRA = new Vector2(200, 80);
            Rectangle rectFoto = new Rectangle(250, 110, 100, 200);

            sp.DrawString(fonte, "ALUNO: " + cred.Nome, posNome, Color.DarkBlue);
            sp.DrawString(fonte, "EMAIL: " + cred.Email, posEmail, Color.DarkGreen);
            sp.DrawString(fonte, "RA   : " + cred.RA, posRA, Color.Black);
            sp.Draw(cred.Foto, rectFoto, Color.White);

        }
    }
}
