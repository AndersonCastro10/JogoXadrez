using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;

namespace JogoXadrez.Entidade.CamadaXadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez Partida;

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);      // Pego a peça que está na posição
            return peca != null && peca.Cor != Cor;   // Verifico se tem peça e se ela é da outra cor
        }

        private bool Livre(Posicao posicao)
        {
            return Tabuleiro.Peca(posicao) == null;   // Tem peça naquela possição?, se null true, não tem peça, se false false, tem peça
        }

        // Metodo para verificar os possiveis movimentos do Peão Branco e Preto
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas]; // Criar uma matriz de linhas e colunas igual ao do tabuleiro especificado

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna); // Peça branca só anda para cima
                if (Tabuleiro.TestarPosicao(posicao) && Livre(posicao))    // Se a posição está ok e está livre
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;

                    if (QtdMovimentos == 0)
                    {
                        posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                        if (Tabuleiro.TestarPosicao(posicao) && Livre(posicao))    // Se a posição está ok e está livre e se for o primeiro movimento do peão 
                        {
                            matriz[posicao.Linha, posicao.Coluna] = true;
                        }
                    }
                    
                }

                // Verificar o lado superior esquerdo se existe inimigo

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.TestarPosicao(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Verificar o lado superior direito se existe inimigo

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.TestarPosicao(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // En Passant

                if (posicao.Linha == 3 ) // o en passant da branca só acontece na linha 3, contando de cima para baixo
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (Tabuleiro.TestarPosicao(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == Partida.VulneravelEnPassant) // Testa se a posição é valida, se tem um inimigo la e se a peca é a peca que está vulneravel, no caso somente o peao pode ficar vulneravel
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (Tabuleiro.TestarPosicao(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == Partida.VulneravelEnPassant) // Testa se a posição é valida, se tem um inimigo la e se a peca é a peca que está vulneravel, no caso somente o peao pode ficar vulneravel
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }

            else
            {
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna); // Peça branca só anda para baixo
                if (Tabuleiro.TestarPosicao(posicao) && Livre(posicao))    // Se a posição está ok e está livre
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;

                    if (QtdMovimentos == 0)
                    {
                        posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                        if (Tabuleiro.TestarPosicao(posicao) && Livre(posicao))    // Se a posição está ok e está livre e se for o primeiro movimento do peão 
                        {
                            matriz[posicao.Linha, posicao.Coluna] = true;
                        }
                    }
                    
                }

                // Verificar o lado superior esquerdo se existe inimigo

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.TestarPosicao(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Verificar o lado superior direito se existe inimigo

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.TestarPosicao(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // En Passant

                if (posicao.Linha == 4) // o en passant da preta só acontece na linha 4, contando de cima para baixo
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (Tabuleiro.TestarPosicao(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == Partida.VulneravelEnPassant) // Testa se a posição é valida, se tem um inimigo la e se a peca é a peca que está vulneravel, no caso somente o peao pode ficar vulneravel
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (Tabuleiro.TestarPosicao(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == Partida.VulneravelEnPassant) // Testa se a posição é valida, se tem um inimigo la e se a peca é a peca que está vulneravel, no caso somente o peao pode ficar vulneravel
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }
    }
}