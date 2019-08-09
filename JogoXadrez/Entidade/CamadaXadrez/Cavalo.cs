using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;

namespace JogoXadrez.Entidade.CamadaXadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        // Metodo que verificar se o Cavalo pode mover para uma determinada posicao

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);    // Recebe uma peca de uma posicao
            return peca == null || peca.Cor != Cor; // Verifica se a peca existe ou se é diferente da cor da peca que está se movendo
        }

        // Metodo para verificar os possiveis movimentos do Cavalo

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas]; // Criar uma matriz de linhas e colunas igual ao do tabuleiro especificado

            Posicao posicao = new Posicao(0, 0);

            // Verificar acima noroeste

            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar esquerda noroeste

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna -2);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar esquerda sudoeste

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar abaixo sudoeste

            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar abaixo sudeste

            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar direita sudeste

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar direira nordeste

            posicao.DefinirValores(Posicao.Linha - 1 , Posicao.Coluna + 2);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar acima nordeste

            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            return matriz;
        }
    }
}
