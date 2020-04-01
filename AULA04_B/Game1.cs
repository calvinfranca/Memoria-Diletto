using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AULA04_B
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        enum EstadoJogo {Jogar, NovoJogo, Ajuda, Menu, GameOver, Credito, Sair };

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont fonte;
        Texture2D imgCosta;
        Texture2D [] imgFrente;
        Texture2D imgBotao;
        Baralho baralho;
        Credito about;
        //Carta[] baralho;
        int numCartas = 2;
        //Texture2D imgFrente2;
        Carta carta1;
        Carta carta2;
        Botao botaoVoltar;
        Botao botaoAjuda;
        Botao botaoJogar;
        Botao botaoNovoJogo;
        Botao botaoCredito;
        Botao botaoSair;
        EstadoJogo estado;
        bool usado;
        int vidas;
        MouseState meuMouse;
        Vector2 posMouse;
        bool leftButtonPressed;
        bool clickedMouse;
        int tempoPausaInteracao;
        Vector2 posMsg;
        Vector2 posVidas;
        Vector2 posMortes;
        Texture2D tCalvin;
        Texture2D tPuc;
        Texture2D tFundo;
        SpriteFont fonte18;
        SpriteFont fonteGrande;
        SpriteFont fonte14;
        SpriteFont fonte24;
        Vector2 posCalvin;
        Vector2 posTexto;
        Vector2 posRa;
        Vector2 posEmail;
        Rectangle rectCalvin;
        Rectangle rectPuc;
        Rectangle rectLogo;
        Texture2D tLogo;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            baralho = new Baralho(2, 5);
            usado = false;
            vidas = 3;
            posVidas = new Vector2(450, 700);
            posMortes = new Vector2(60, 50);
            numCartas = 2 * 5;//Linha*Coluna
            imgFrente = new Texture2D[numCartas];
            carta1 = null;
            carta2 = null;
            botaoVoltar = new Botao(400, 750, "VOLTAR");
            botaoAjuda = new Botao(400, 680, "AJUDA");
            botaoJogar = new Botao(400, 560, "CONTINUAR");
            botaoNovoJogo = new Botao(400, 500, "NOVO JOGO");
            botaoCredito = new Botao(400, 620, "SOBRE");
            botaoSair = new Botao(400, 740, "SAIR");
            estado = EstadoJogo.Menu;
            posCalvin = new Vector2(380, 230);
            posRa = new Vector2(430, 270);
            posEmail = new Vector2(390, 310);
            posTexto = new Vector2(0, 0);
            rectCalvin = new Rectangle(200, -100, 600, 450);
            rectPuc = new Rectangle(0, 350, 70, 125);
            rectLogo = new Rectangle(0, 0, 1000, 800);
            /*baralho = new Carta[numCartas];
            for (int i = 0; i < numCartas; i++)
            {
                baralho[i] = new Carta(i * 101, 0, 100,200, 1, i);
            }*/
            //carta = new Carta(0, 0, 100, 200, 1, 1);
            //carta2 = new Carta(101, 0, 100, 200, 1, 2);
            leftButtonPressed =false;
            clickedMouse=false;
            tempoPausaInteracao = 0;
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth =1000;
            graphics.PreferredBackBufferHeight =800 ;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tCalvin = Content.Load<Texture2D>("Calvin");
            tPuc = Content.Load<Texture2D>("Puc");
            tFundo = Content.Load<Texture2D>("fundo");
            tLogo = Content.Load<Texture2D>("fundologo");
            fonte18 = Content.Load<SpriteFont>("Arial18");
            fonte14 = Content.Load<SpriteFont>("Arial14");
            fonte24 = Content.Load<SpriteFont>("Arial24");
            fonteGrande = Content.Load<SpriteFont>("ArialGrande");
            imgCosta = Content.Load<Texture2D>("CartaCosta");
            for (int i=0;i<numCartas; i++){
                imgFrente[i] = Content.Load<Texture2D>("Carta"+(i+1));
            }
            fonte = Content.Load<SpriteFont>("Arial");
            imgBotao = Content.Load<Texture2D>("botao");
            about = new Credito("EDUARDO SAVINO GOMES",
                              "egomes@pucsp.br",
                              "005614",
                              imgFrente[0]);

            //imgFrente[0] = Content.Load<Texture2D>("Carta1"); 
            //imgFrente[1] = Content.Load<Texture2D>("Carta2"); 
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (estado == EstadoJogo.Jogar)
            {
                #region JOGAR
                if (tempoPausaInteracao == 0)
                {
                    verificarCliqueMouse();

                    if (clickedMouse == true)
                    {

                        for (int i = 0; i < numCartas; i++)
                        {
                            Carta crt = baralho.cartas[i];
                            crt.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                            if (!crt.Descoberta)
                            {
                                //Se ele clico na Carta1 Novamente Então Devole Carta
                                if (crt == carta1 && !crt.Clicada) { carta1 = null; }
                                #region Pegar As Cartas
                                if (crt.Clicada && crt != carta1)
                                {
                                    if (carta1 == null)
                                    {
                                        carta1 = crt;
                                        break;
                                    }
                                    else
                                    {
                                        carta2 = crt;
                                        tempoPausaInteracao = 80;
                                        posMsg = new Vector2(20, 50);
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }

                        //carta.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        //carta2.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        if (botaoVoltar.Clicado)
                        {
                            estado = EstadoJogo.Menu;
                            botaoVoltar.Clicado = false;
                        }
                        clickedMouse = false;
                    }

                    if (tempoPausaInteracao == 0)
                    {
                        #region Comparar as Cartas
                        //Se já pegou duas cartas então compara
                        if (carta1 != null && carta2 != null)
                        {
                            if (carta1.Igual(carta2))
                            {
                                carta1.Descoberta = true;
                                carta2.Descoberta = true;
                            }
                            else
                            {
                                vidas -= 1;
                                carta1.Clicada = false;
                                carta2.Clicada = false;
                            }
                            carta1 = null;
                            carta2 = null;
                        }
                        #endregion
                    }

                }

                if (tempoPausaInteracao > 0)
                {
                    tempoPausaInteracao = tempoPausaInteracao - 1;
                }
                #endregion
            }
            else if (estado == EstadoJogo.NovoJogo)
            {
                if (usado == true)
                {
                    //Cria novo baralho e dá atributos iniciais
                    baralho = new Baralho(2, 5);
                    baralho.Embaralhar();
                    vidas = 3;
                    usado = false;
                }
                #region JOGAR
                //No jogar o programa varre todas as cartas do baralho e checa se alguma foi clicada.
                //Se houve clique ele armazena a carta
                //Se houve outro clique ele armazena a segunda carta e compara elas

                    if (tempoPausaInteracao == 0)
                    {
                        verificarCliqueMouse();

                        if (clickedMouse == true)
                        {

                            for (int i = 0; i < numCartas; i++)
                            {
                                Carta crt = baralho.cartas[i];
                                crt.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                                if (!crt.Descoberta)
                                {
                                    //Se ele clico na Carta1 Novamente Então Devole Carta
                                    if (crt == carta1 && !crt.Clicada) { carta1 = null; }
                                    #region Pegar As Cartas
                                    if (crt.Clicada && crt != carta1)
                                    {
                                        if (carta1 == null)
                                        {
                                            carta1 = crt;
                                            break;
                                        }
                                        else
                                        {
                                            carta2 = crt;
                                            tempoPausaInteracao = 80;
                                            posMsg = new Vector2(20, 50);
                                            break;
                                        }
                                    }
                                    #endregion
                                }
                            }

                           
                            botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                            if (botaoVoltar.Clicado)
                            {
                                estado = EstadoJogo.Menu;
                                botaoVoltar.Clicado = false;
                            }
                            clickedMouse = false;
                        }

                        if (tempoPausaInteracao == 0)
                        {
                            #region Comparar as Cartas
                            //Se já pegou duas cartas então compara
                            if (carta1 != null && carta2 != null)
                            {
                                if (carta1.Igual(carta2))
                                {
                                    carta1.Descoberta = true;
                                    carta2.Descoberta = true;
                                }
                                else
                                {
                                    vidas -= 1;
                                    carta1.Clicada = false;
                                    carta2.Clicada = false;
                                }
                                carta1 = null;
                                carta2 = null;
                            }
                            #endregion
                        }

                    }

                    //Controle de tempo da iteração do jogo
                    if (tempoPausaInteracao > 0)
                    {
                        tempoPausaInteracao = tempoPausaInteracao - 1;
                    }
                    #endregion
                              
            }
            else if (estado == EstadoJogo.Credito)
            {
                rectPuc.X += 2;


                if (rectPuc.X > GraphicsDevice.Viewport.Width)
                {
                    rectPuc.X = -200;
                    rectPuc.Y = 350;

                }
                posTexto.X = rectPuc.X - 50;
                posTexto.Y = rectPuc.Y - 10;
                verificarCliqueMouse();

                if (clickedMouse == true)
                {
                    botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    if (botaoVoltar.Clicado)
                    {
                        estado = EstadoJogo.Menu;
                        botaoVoltar.Clicado = false;
                    }
                    clickedMouse = false;
                }
            }
            else if (estado == EstadoJogo.GameOver)
            {
                verificarCliqueMouse();
                if (clickedMouse == true)
                {
                    botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    if (botaoVoltar.Clicado)
                    {
                        estado = EstadoJogo.Menu;
                        botaoVoltar.Clicado = false;
                    }
                    clickedMouse = false;
                }
            }
            else if (estado == EstadoJogo.Ajuda)
            {
                verificarCliqueMouse();
                if (clickedMouse == true)
                {
                    botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    if (botaoVoltar.Clicado)
                    {
                        estado = EstadoJogo.Menu;
                        botaoVoltar.Clicado = false;
                    }
                    clickedMouse = false;
                }
            }
            else if (estado == EstadoJogo.Menu)
            {
                #region MENU
                verificarCliqueMouse();
                if (clickedMouse)
                {
                    botaoJogar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    botaoAjuda.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    botaoNovoJogo.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    botaoCredito.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    botaoSair.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    if (botaoJogar.Clicado)
                    {
                        estado = EstadoJogo.Jogar;
                        
                        botaoJogar.Clicado = false;
                    }
                    else if (botaoNovoJogo.Clicado)
                    {
                        estado = EstadoJogo.NovoJogo;
                        usado = true;
                        botaoNovoJogo.Clicado = false;
                    }
                    else if (botaoAjuda.Clicado)
                    {
                        estado = EstadoJogo.Ajuda;
                        usado = true;
                        botaoAjuda.Clicado = false;
                    }
                    else if (botaoCredito.Clicado)
                    {
                        estado = EstadoJogo.Credito;
                        botaoCredito.Clicado = false;
                    }
                    else if (botaoSair.Clicado)
                    {
                        estado = EstadoJogo.Sair;
                        botaoSair.Clicado = false;
                    }
                    clickedMouse = false;
                }
                #endregion
            }else if (estado == EstadoJogo.Sair)
            {
                Exit();
            }
            base.Update(gameTime);

        }

        private void verificarCliqueMouse()
        {
            meuMouse = Mouse.GetState();
            posMouse.X = meuMouse.X;
            posMouse.Y = meuMouse.Y;
            if (meuMouse.LeftButton == ButtonState.Pressed)
            {
                leftButtonPressed = true;
            }

            if (meuMouse.LeftButton == ButtonState.Released &&
                leftButtonPressed == true)
            {
                clickedMouse = true;
                leftButtonPressed = false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (estado == EstadoJogo.Jogar)
            {
                spriteBatch.Draw(tLogo, rectLogo, Color.White);
                spriteBatch.DrawString(fonte, "VIDAS:" + vidas, posVidas, Color.Black);
                #region DRAW JOGAR
                for (int i = 0; i < numCartas; i++)
                {
                    DrawObjetos.DrawCarta(baralho.cartas[i], spriteBatch, imgCosta,
                                imgFrente[baralho.cartas[i].Image]);
                }
                if (tempoPausaInteracao > 0)
                {
                    posMsg.X += 2;
                    
                    if (carta1.Igual(carta2))
                    {
                        spriteBatch.DrawString(fonteGrande, "ACERTOU!!!", posMsg, Color.Green);
                        
                        carta1.Largura -= 3;
                        carta1.Altura -= 5;
                        carta2.Largura -= 3;
                        carta2.Altura -= 5;
                    }
                    else
                    {
                        
                        spriteBatch.DrawString(fonteGrande, "ERROU.....", posMsg, Color.Red);
                    }
                }
                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch,
                                      imgBotao, fonte);
                #endregion
                if (vidas == -1)
                {
                    estado = EstadoJogo.GameOver;
                }
            }
            else if (estado == EstadoJogo.NovoJogo)
            {
                spriteBatch.Draw(tLogo, rectLogo, Color.White);
                spriteBatch.DrawString(fonte, "VIDAS:"+vidas, posVidas, Color.Black);

                #region DRAW JOGAR
                for (int i = 0; i < numCartas; i++)
                {
                    DrawObjetos.DrawCarta(baralho.cartas[i], spriteBatch, imgCosta,
                                imgFrente[baralho.cartas[i].Image]);
                }
                if (tempoPausaInteracao > 0)
                {
                    posMsg.X += 2;

                    if (carta1.Igual(carta2))
                    {
                        spriteBatch.DrawString(fonteGrande, "ACERTOU!!!", posMsg, Color.Green);
                        carta1.Largura -= 3;
                        carta1.Altura -= 5;
                        carta2.Largura -= 3;
                        carta2.Altura -= 5;
                    }
                    else
                    {
                        
                        spriteBatch.DrawString(fonteGrande, "ERROU...", posMsg, Color.Red);
                    }
                }
                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch,
                                      imgBotao, fonte);
                #endregion
                if(vidas == -1)
                {
                    estado = EstadoJogo.GameOver;
                    vidas = 3;
                }
            }
            else if (estado == EstadoJogo.Menu)
            {
                spriteBatch.Draw(tLogo, rectLogo, Color.White);
                #region DRAW MENU
                DrawObjetos.DrawBotao(botaoJogar, spriteBatch, imgBotao, fonte);
                DrawObjetos.DrawBotao(botaoAjuda, spriteBatch, imgBotao, fonte);
                DrawObjetos.DrawBotao(botaoNovoJogo, spriteBatch, imgBotao, fonte);
                DrawObjetos.DrawBotao(botaoCredito, spriteBatch, imgBotao, fonte);
                DrawObjetos.DrawBotao(botaoSair, spriteBatch, imgBotao, fonte);
                #endregion

            }
            else if (estado == EstadoJogo.Credito)
            {
                //spriteBatch.DrawString(fonte, "TELA DE CREDIO", Vector2.Zero, Color.Red);
                //DrawObjetos.Credito(about, spriteBatch, fonte);
                
                Vector2 pos = new Vector2(0, 0);
                Rectangle rectFundo = new Rectangle(0, 0,
                                      GraphicsDevice.Viewport.Width,
                                      GraphicsDevice.Viewport.Height);
                // TODO: Add your drawing code here
                
                spriteBatch.Draw(tFundo, rectFundo, Color.White);
                spriteBatch.Draw(tCalvin, rectCalvin, Color.White);
                spriteBatch.Draw(tPuc, rectPuc, Color.White);


                spriteBatch.DrawString(fonte, "Calvin do Amaral Franca", posCalvin, Color.Black);
                spriteBatch.DrawString(fonte14, "RA00207814", posRa, Color.Black);
                spriteBatch.DrawString(fonte14, "calvin.franca@gmail.com", posEmail, Color.Black);
                spriteBatch.DrawString(fonte14, "LPG3 - Jogos Digitais", posTexto, Color.Red);
                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch, imgBotao, fonte);
            }
            else if (estado == EstadoJogo.GameOver)
            {
                spriteBatch.Draw(tLogo, rectLogo, Color.White);
                spriteBatch.DrawString(fonte24, "TENTE NOVAMENTE....TA FRACA SUA MEMORIA HEIN?", posMortes, Color.Black);

                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch, imgBotao, fonte);
            }
            else if (estado == EstadoJogo.Ajuda)
            {
                spriteBatch.Draw(tLogo, rectLogo, Color.White);
                spriteBatch.DrawString(fonte24, "CLIQUE EM DUAS CARTAS E TENTE FORMAR OS PARES", posMortes, Color.Black);
                

                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch, imgBotao, fonte);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
