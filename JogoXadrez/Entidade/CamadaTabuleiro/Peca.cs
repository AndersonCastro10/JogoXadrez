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
    }
}