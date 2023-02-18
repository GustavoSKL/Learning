using System;
using System.Data.SqlTypes;
using System.Net.WebSockets;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
{
    internal class Program : GameWindow
    {
        int yJogador1 = 0;
        int yJogador2 = 0;
        int xBola = 0;
        int yBola = 0;
        int velocidadeDaBolaX = 3;
        int velocidadeDaBolaY = 3;
        int TamanhoDaBola = 20;
        int v1y = 0;
        int v2y = 0;

        int xDoJogador1()
        {
            return -ClientSize.Width / 2 + larguraDosJogadores() / 2;
        }
        int xDoJogador2()
        {
            return ClientSize.Width / 2 - larguraDosJogadores() / 2;
        }

        int larguraDosJogadores()
        {
            return TamanhoDaBola;
        }
        int alturaDosJogadores()
        {
            return 3 * TamanhoDaBola;
        }
        static void Main()
        {
            new Program().Run(60);  
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            xBola = xBola + velocidadeDaBolaX;
            yBola = yBola + velocidadeDaBolaY;



            if (xBola + TamanhoDaBola / 2 > xDoJogador2() - larguraDosJogadores() / 2 
               && yBola - TamanhoDaBola / 2 < yJogador2 + alturaDosJogadores() / 2
               && yBola + TamanhoDaBola / 2 > yJogador2 - alturaDosJogadores() / 2)
            {
                velocidadeDaBolaX = velocidadeDaBolaX + 1;
                v2y = v2y + 1;
                velocidadeDaBolaX = -velocidadeDaBolaX;
            }

            if (xBola - TamanhoDaBola / 2 < xDoJogador1() + larguraDosJogadores() / 2
               && yBola - TamanhoDaBola / 2 < yJogador1 + alturaDosJogadores() / 2
               && yBola + TamanhoDaBola / 2 > yJogador1 - alturaDosJogadores() / 2)
            {
                velocidadeDaBolaX = velocidadeDaBolaX - 1;
                v1y = v1y + 1;

                velocidadeDaBolaX = -velocidadeDaBolaX;
            }
            if (yBola + TamanhoDaBola > ClientSize.Height / 2)
            {
                velocidadeDaBolaX = velocidadeDaBolaX + 1;

                velocidadeDaBolaY = -velocidadeDaBolaY;
            }
            if (yBola - TamanhoDaBola < -ClientSize.Height / 2)
            {
                velocidadeDaBolaX = velocidadeDaBolaX - 1;

                velocidadeDaBolaY = -velocidadeDaBolaY;
            }

            if (xBola < - ClientSize.Width / 2 || xBola > ClientSize.Width / 2)
            {
                xBola = 0;
                yBola = 0;
                velocidadeDaBolaX = 3;
                velocidadeDaBolaY = 3;
                yJogador1 = 0;
                yJogador2 = 0;
                v1y = 0;
                v2y = 0;

            }

            if (Keyboard.GetState().IsKeyDown(Key.W))
            {

                    yJogador1 = yJogador1 + 5 + v1y;
            }
            if (Keyboard.GetState().IsKeyDown(Key.S))
            {

                yJogador1 = yJogador1 - 5 - v1y;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {

                yJogador2 = yJogador2 + 5 + v2y;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {

                yJogador2 = yJogador2 - 5 - v2y;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Console.WriteLine(ClientSize.Width + " " + ClientSize.Height);

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            Matrix4 projectoin = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectoin);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            Quadrado(xDoJogador1(), yJogador1, larguraDosJogadores(), alturaDosJogadores());
            Quadrado(xDoJogador2(), yJogador2, larguraDosJogadores(), alturaDosJogadores());
            Quadrado(xBola, yBola, TamanhoDaBola, TamanhoDaBola);

            SwapBuffers();
        }

        void Quadrado(int x, int y, int width, int height)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * width + x, -0.5f * height + y);
            GL.Vertex2(0.5f * width + x, -0.5f * height + y);
            GL.Vertex2(0.5f * width + x, 0.5f * height + y);
            GL.Vertex2(-0.5f * width + x, 0.5f * height + y);
            GL.End();
        }
    }
}
