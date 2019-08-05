using System.Collections.Generic;
using JogoXadrez.Entidade.CamadaTabuleiro;
using JogoXadrez.Entidade.CamadaTabuleiro.Enum;
using JogoXadrez.Entidade.CamadaTabuleiro.Excecao;

namespace JogoXadrez.Entidade.CamadaXadrez
{      
    // Terá a mecanica do jogo

    class PartidaDeXadrez 
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> ConjuntoPecasNova;
        private HashSet<Peca> ConjuntoPecasCapturada;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ConjuntoPecasNova = new HashSet<Peca>();
            ConjuntoPecasCapturada = new HashSet<Peca>();
            ColocarPecas();
        }

        // Metodo para executar um movimento

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);               // Retirar a peça da origem, estou mexendo essa peça
            peca.IncrementarMovimentos();                            // Incrementar a qtd de movimentos dessa peça
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);     // Capturar peça que está no destino ou não, pode estar vazio
            Tabuleiro.ColocarPeca(peca, destino);                    // Colocar a peça no destino
            if (pecaCapturada != null)                               // Se existir uma peca no local de destino ela será capturada
            {
                ConjuntoPecasCapturada.Add(pecaCapturada);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++; // Passar o turno
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao posicao)
        {
            if (!Tabuleiro.ExistePeca(posicao)) // Testa se a existe peça na posição e se a posição é valida
            {
            }
            if (Tabuleiro.Peca(posicao) == null) // Testa se existe peca na posiçao escolhida
            {
                throw new TabuleiroExcecao("Não existe peça na posição de origem escolhida");
            }
            if (JogadorAtual != Tabuleiro.Peca(posicao).Cor) // Teste se o jogador atual (cor) é diferente da peca que ele irá mover (cor)
            {
                throw new TabuleiroExcecao("A peça de origm escolhida não é sua!");
            }
            if (!Tabuleiro.Peca(posicao).ExisteMovimentosPossiveis()) //Testa se NÂO exite movimentos possiveis
            {
                throw new TabuleiroExcecao("Não há movimentos possíveis para a peça de origem escolhida");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroExcecao("Posição de destino inválida!"); //Se a peça Não poder mover para o destino lança a exceção
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        // Metodo para retornar um conjunto de peca capturadas dada uma cor 
        
        public HashSet<Peca> CorPecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();              // Cria um conjunto de pecas auxiliar
            foreach (Peca peca in ConjuntoPecasCapturada)         // Percorre peca no Conjunto de pecas capturadas
            {
                if (peca.Cor == cor)                              // Se a peca for da cor informada no paramentro adiciona ela no conjunto aux
                {
                    aux.Add(peca);
                }
            }
            return aux;     // Retorna conjunto auxiliar 
        }

        // Metodo para retornar um conjunto de peca do jogo dada uma cor 

        public HashSet<Peca> CorPecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();              // Cria um conjunto de pecas auxiliar
            foreach (Peca peca in ConjuntoPecasNova)              // Percorre peca no Conjunto de pecas em jogo (novas)
            {
                if (peca.Cor == cor)                              // Se a peca for da cor informada no paramentro adiciona ela no conjunto aux
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(CorPecasCapturadas(cor));            // irá Pega todas as peças, exceto as que foram capturadas dessa cor
            return aux;                                         // Retorna conjunto auxiliar 
        }

        // Metodo que coloca nova peca no Tabuleiro em uma nova posicao

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna,linha).ToPosicao());
            ConjuntoPecasNova.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tabuleiro, Cor.Preta));
        }
    }
}
