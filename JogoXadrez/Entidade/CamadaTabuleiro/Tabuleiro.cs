namespace JogoXadrez.Entidade.CamadaTabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public Peca[,] Pecas { get; private set; }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        // dar acesso a uma peça individual, retornando uma peça em 
        // um posição especifica

        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        // Colocar uma peça no tabuleiro, e depois atribuir uma posição a minha peça
        
        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }
    }
}
