using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;

namespace JogoXadrez.Entidade.CamadaXadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "D";
        }

        // Metodo que verificar se o Toore pode mover para uma determinada posicao

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);    // Recebe uma peca de uma posicao
            return peca == null || peca.Cor != Cor; // Verifica se a peca existe ou se é diferente da cor da peca que está se movendo

        }

        // Metodo para verificar os possiveis movimentos do Dama
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas]; // Criar uma matriz de linhas e colunas igual ao do tabuleiro especificado

            Posicao posicao = new Posicao(0, 0);

            // Verificar acima

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Linha = posicao.Linha - 1; // Para verificar a proxima posição acima
            }

            // Verificar abaixo

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Linha = posicao.Linha + 1; // Para verificar a proxima posição abaixo
            }

            // Verificar direita

            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Coluna = posicao.Coluna + 1; // Para verificar a proxima posição direita
            }

            // Verificar esquerda

            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Coluna = posicao.Coluna - 1; // Para verificar a proxima posição esquerda
            }

            // Verificar Nordeste

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Linha = posicao.Linha - 1; // Para verificar a proxima posição nordeste
                posicao.Coluna = posicao.Coluna - 1; // Para verificar a proxima posição nordeste
            }

            // Verificar Sudoeste

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Linha = posicao.Linha + 1; // Para verificar a proxima posição sudoeste
                posicao.Coluna = posicao.Coluna - 1; // Para verificar a proxima posição sudoeste
            }

            // Verificar Nordeste

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Coluna = posicao.Coluna + 1; // Para verificar a proxima posição nordeste
                posicao.Linha = posicao.Linha - 1; // Para verificar a proxima posição nordeste
            }

            // Verificar Sudeste

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tabuleiro.TestarPosicao(posicao) && PodeMover(posicao)) // Enquanto a posiçao for valida e posso mover para la
            {
                matriz[posicao.Linha, posicao.Coluna] = true;       // A matriz irá receber essa posição
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor) // Se tiver peça lá e for diferente de cor para o While
                {
                    break;
                }
                posicao.Coluna = posicao.Coluna + 1; // Para verificar a proxima posição sudeste
                posicao.Linha = posicao.Linha + 1; // Para verificar a proxima posição sudeste
            }

            return matriz;
        }
    }
}
