namespace JogoXadrez.Entidade.Tabuleiro
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

    }
}
