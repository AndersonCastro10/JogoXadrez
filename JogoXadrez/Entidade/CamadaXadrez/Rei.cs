using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;

namespace JogoXadrez.Entidade.CamadaXadrez
{
    class Rei : Peca
    {
        public PartidaDeXadrez Partida {get;private set; }

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        // Metodo que verificar se o Rei pode mover para uma determinada posicao

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);    // Recebe uma peca de uma posicao
            return peca == null || peca.Cor != Cor; // Verifica se a peca existe ou se é diferente da cor da peca que está se movendo
        }

        // Testar se a torre está possivel para roque

        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);  // Pega a peca daquela possição, ainda não sei se é uma torre
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QtdMovimentos == 0; // Se não está vazio, se é uma torre, se é da mesma cor e se ainda não movimentou
        }

        // Metodo para verificar os possiveis movimentos do REI
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas]; // Criar uma matriz de linhas e colunas igual ao do tabuleiro especificado

            Posicao posicao = new Posicao(0, 0);

            // Verificar acima

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar Nordeste

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar Direita

            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar Sudeste

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar Abaixo

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar Sudoeste

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar Esquerda

            posicao.DefinirValores(Posicao.Linha , Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Verificar Noroeste

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Roque

            if (QtdMovimentos == 0 && !Partida.Xeque)  // Verifica se o rei ainda não moveu e se ele não está em xeque
            {
                // Roque pequeno

                Posicao posicaoTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3); // Pega a peça que está 3 cassas ao lado direito do rei
                if (TesteTorreParaRoque(posicaoTorre1)) // Se for uma torre e estiver valida para o roque
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1); // Pega a posição que está ao lado direito do rei (+ 1)
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2); // Pega a posição que está ao lado direito do rei (+ 2)

                    if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null) // Testa se as duas posições estão vazias
                    {
                        matriz[Posicao.Linha, Posicao.Coluna + 2] = true; // Então essa posição é valida
                    }
                }

                // Roque Grande

                Posicao posicaoTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4); // Pega a peça que está 3 cassas ao lado direito do rei
                if (TesteTorreParaRoque(posicaoTorre1)) // Se for uma torre e estiver valida para o roque
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1); // Pega a posição que está ao lado direito do rei (+ 1)
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2); // Pega a posição que está ao lado direito do rei (+ 2)
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3); // Pega a posição que está ao lado direito do rei (+ 3)

                    if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null && Tabuleiro.Peca(p3) == null) // Testa se as tres posições estão vazias
                    {
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true; // Então essa posição é valida
                    }
                }
            }

            return matriz;
        }
    }
}
