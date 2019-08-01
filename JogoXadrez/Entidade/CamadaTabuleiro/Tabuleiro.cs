using JogoXadrez.Entidade.CamadaTabuleiro.Excecao;

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

        public Peca Peca(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }

        // Colocar uma peça no tabuleiro, e depois atribuir uma posição a minha peça
        // Só posso colocar uma peça onde não tenha nenhuma peça
        
        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao)) // Existe peça na posição, true exceção, false continua
            {
                throw new TabuleiroExcecao("Já existi uma peça nessa posição!");
            }
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        // Retirar peca do tabuleiro

        public Peca RetirarPeca(Posicao posicao)
        {
            if (ExistePeca(posicao) == false)   //Existe peca na posição? se falso retorna null
            {
                return null;
            }
            Peca aux = Peca(posicao);    // Recebe a peca que está na posição e guarda na variavel auxiliar
            aux.Posicao = null;          // Agora a posicao da peça ficara null
            Pecas[posicao.Linha, posicao.Coluna] = null;         // Posição no tabuleiro fica null (vazio)
            return aux;                  // Retorno somente a peça, sem posição
        }

        // Testar se a posição é valida 
        
        public bool TestarPosicao(Posicao posicao)
        {
            if(posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        // Lançar uma exceção caso a posição for invalida

        public void ValidarPosicao(Posicao posicao)
        {
            if (!TestarPosicao(posicao))
            {
                throw new TabuleiroExcecao("Posição inválida!");
            }
        }

        // Verificar se existi uma peça em uma posição, retorna uma peça na posição que esteja diferente de null
        // Ou seja a posição tem que estar ocupada por uma peça

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return Peca(posicao) != null; // mesma coisa do IF, peca na posição x é diferente de null, true, false
        }
    }
}
