using JogoXadrez.Entidade.CamadaTabuleiro;

namespace JogoXadrez.Entidade.CamadaXadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        // Metodo que retorna a posição original da matriz posicao do tabuleiro

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a'); // Coluna - 'a', se coluna B, B - A = 1 (1 - 0 = 1)
        }

        public override string ToString()
        {
            return "" + Coluna + Linha; // "" transforma o texto em string, pois a linha é um inteiro e a coluna um char
        }
    }
}
