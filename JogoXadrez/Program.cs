using System;
using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaXadrez;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;
using JogoXadrez.Entidade.CamadaTabuleiro.Excecao;

namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Posicao posicao = new Posicao(1, 2);
                Console.WriteLine(posicao);

                Tabuleiro tabuleiro = new Tabuleiro(8, 8);

                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 1));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(2, 7));

                Tela.ImprimirTabuleiro(tabuleiro);

                PosicaoXadrez posicaoXadrez = new PosicaoXadrez('b', 7);
                Console.WriteLine(posicaoXadrez);
                Console.WriteLine(posicaoXadrez.ToPosicao());

            }
            catch (TabuleiroExcecao e)
            {

                Console.WriteLine(e.Message); ;
            }
           

            Console.ReadLine();

        }
    }
}
