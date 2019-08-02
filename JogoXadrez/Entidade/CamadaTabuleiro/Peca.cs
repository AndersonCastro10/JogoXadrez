using JogoXadrez.Entidade.CamadaTabuleiro.Enum;

namespace JogoXadrez.Entidade.CamadaTabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            QtdMovimentos = 0;
            Tabuleiro = tabuleiro;
        }

        // Incrementar a quantidade de movimentos da peça

        public void IncrementarMovimentos()
        {
            QtdMovimentos++;
        }

        // Metodo que verifica as possiveis movimentações de uma peca, nessa clase o metodo será abstrato
        // Pois assim será obrigatorio cada peca ter uma sobregarga desse metodo

        public abstract bool[,] MovimentosPossiveis();

        // Metodo que verfica se existe os movimentos possiveis para uma determinada peca

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();

            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (matriz[i,j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}