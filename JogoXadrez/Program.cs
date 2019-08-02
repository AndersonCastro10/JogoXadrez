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
               PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro);
                    Console.WriteLine();
                    Console.WriteLine("Turno : " + partida.Turno);
                    Console.WriteLine("Aguardando jogado : " + partida.JogadorAtual);

                    Console.WriteLine();
                    Console.Write("Digite a posição de origem : ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                    bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossiveis(); // Pegar os movimentos posiveis de uma peca

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Digite a posição de destino : ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.RealizaJogada(origem, destino);
                }

            }
            catch (TabuleiroExcecao e)
            {

                Console.WriteLine(e.Message); ;
            }
           

            Console.ReadLine();

        }
    }
}
