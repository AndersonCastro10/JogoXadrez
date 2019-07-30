using System;
using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaXadrez;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;

namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao posicao = new Posicao(1, 2);
            Console.WriteLine(posicao);

            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(2, 4));

            Tela.ImprimirTabuleiro(tabuleiro);

            Console.ReadLine();

        }
    }
}
