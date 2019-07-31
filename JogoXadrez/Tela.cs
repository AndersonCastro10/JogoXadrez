using System;
using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;

namespace JogoXadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro (Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (tabuleiro.Peca(i,j) == null)
                    {
                        Console.Write("- ");

                    }
                    else
                    {
                        ImprimirPeca(tabuleiro.Peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;      // Guarda cor atual dos carecteres (meio branco)
                Console.ForegroundColor = ConsoleColor.DarkYellow;   // Coloca a cor como amarelo escuro, para destacar melhor
                Console.Write(peca);                             // Imprime a peca, que ira ficar amarela
                Console.ForegroundColor = aux;                   // Depois volta a cor antes (meio branco)           
            }
        }
    }
}
