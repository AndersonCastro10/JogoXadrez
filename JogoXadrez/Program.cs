using System;
using JogoXadrez.Entidade.Tabuleiro;

namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao posicao = new Posicao(1, 2);
            Console.WriteLine(posicao);
        }
    }
}
