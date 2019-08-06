using System;
using System.Collections.Generic;
using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;
using JogoXadrez.Entidade.CamadaXadrez;

namespace JogoXadrez
{
    class Tela
    {

        // Metodo para imprimir a partida na tela

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno : " + partida.Turno);
            Console.WriteLine("Aguardando jogado : " + partida.JogadorAtual);
            if (partida.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Pecas capturadas");
            Console.Write(" Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write(" Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[ ");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + ", ");
            }
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        // Metodos com o tabuleiro e posiçoes possiveis

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor; // Guardando o fundo original (no caso preto)
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray; // Guardando o fundo alterado (no caso cinza escuro)

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i,j]) // se posição possivel for verdadeiro, altera o fundo para cinza, se não fica preto normal
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal; // Depois que imprimir a peca tenho que colocar o fundo original!
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal; // Para garantir que o fundo volte a ser original!
        }

        // Metodo para ler do teclado do usuario a posição do Xadrez

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s.Substring(1).ToString());   // Preciso transformar em string porque o 1 caracter é char, coloquei substring porque pode ser digitado um valor fora do tabuleiro, ex c22
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
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
                Console.Write(" ");
            }
        }
    }
}
