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
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            ConjuntoPecasNova = new HashSet<Peca>();
            ConjuntoPecasCapturada = new HashSet<Peca>();
            ColocarPecas();
        }

        // Metodo para executar um movimento

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);               // Retirar a peça da origem, estou mexendo essa peça
            peca.IncrementarMovimentos();                            // Incrementar a qtd de movimentos dessa peça
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);     // Capturar peça que está no destino ou não, pode estar vazio
            Tabuleiro.ColocarPeca(peca, destino);                    // Colocar a peça no destino
            if (pecaCapturada != null)                               // Se existir uma peca no local de destino ela será capturada
            {
                ConjuntoPecasCapturada.Add(pecaCapturada);           // Adciona ao conjunto de pecas capturadas
            }
            // return pecaCapturada;                                   // Retorna a peca capturada

            // Implantar o Roque 

            if (peca is Rei ) // Testa se a peça é um rei , se é o primeiro movimento ja foi testado na classe
            {
                // Roque pequeno
                if (destino.Coluna == origem.Coluna + 2) // Se o rei andou para a direita duas casas, quer dizer que é o roque pequeno
                {
                    Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3); // Irá pegar a posição que está a 3 casas a direita do rei (posição de origem, Torre)
                    Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1); // Irá pegar a posição que está a 1 casas a direita do rei (posição de destino, Torre)

                    Peca torre = Tabuleiro.RetirarPeca(origemTorre); // Pega a peça que está na posição de origem 
                    torre.IncrementarMovimentos();                   // Incrementa o movimento
                    Tabuleiro.ColocarPeca(torre, destinoTorre);      // E coloca a peca na posição de destina instaciada acima
                }

                // Roque grande
                if (destino.Coluna == origem.Coluna - 2)
                {
                    Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                    Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                    Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                    torre.IncrementarMovimentos();
                    Tabuleiro.ColocarPeca(torre, destinoTorre);
                }
            }

            // Implantar o En Passant

            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null) // Testa se houve um movimento na diagonal e se não capturou peça
                {
                    Posicao posicaoPeaoCapturar;
                    if (peca.Cor == Cor.Branca)
                    {
                        posicaoPeaoCapturar = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posicaoPeaoCapturar = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tabuleiro.RetirarPeca(posicaoPeaoCapturar);
                    ConjuntoPecasCapturada.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        // Metodo que desfaz a jogada

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);   // Retira a peca que foi para o destino
            peca.DecrementarMovimentos(); // Tira a quantidade feita no movimento
            if (pecaCapturada != null)    // Se existir uma peça capturada, preciso devolver ela no lugar do destino
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);  // Coloca peca de volta, ela estava no destino
                ConjuntoPecasCapturada.Remove(pecaCapturada);   // Remove a peca capturada do conjunto de pecas capturadas
            }
            Tabuleiro.ColocarPeca(peca, origem); // Colocar peca na posição de origem

            if (peca is Rei )
            {
                // Roque pequeno
                if (destino.Coluna == origem.Coluna + 2)
                {
                    Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                    Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                    Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                    torre.DecrementarMovimentos();
                    Tabuleiro.ColocarPeca(torre, origemTorre);
                }

                // Roque grande
                if (destino.Coluna == origem.Coluna - 2)
                {
                    Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                    Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                    Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                    torre.DecrementarMovimentos();
                    Tabuleiro.ColocarPeca(torre, origemTorre);
                }
            }

            // En Passant

            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant) // Testa se teve um movimento na diagonal e se a peca capturada foi uma peça vulneravel
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posicaoAntigaPeao;
                    if (peca.Cor == Cor.Branca)
                    {
                        posicaoAntigaPeao = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posicaoAntigaPeao = new Posicao(4, destino.Coluna);
                    }
                    Tabuleiro.ColocarPeca(peao, posicaoAntigaPeao);
                }
            }
        }

        // Metodo que realiza a jogada, verificando se o rei não ficará em xeque, se ficar, tem que desfazer a jogada

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino); // Guardo a peca capturada (se tiver alguma) para depois desfazer o movimento, caso o rei fique em xeque

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExcecao("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))  // Se a cor adversaria entre em xeque
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TestaXequeMate(Adversaria(JogadorAtual))) // Se o jogador atual (cor) está em xeque mate
            {
                Terminada = true;
            }
            else
            {
                Turno++; // Passar o turno
                MudaJogador();
            }

            Peca peca = Tabuleiro.Peca(destino); // Qual peça foi movida? 

            // EnPassant 

            if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)) //Verifica se é um peao e se andou 2 casas, se andou é porque é o primeiro movimento
            {
                VulneravelEnPassant = peca;
            }
            else
            {
                VulneravelEnPassant = null;
            }
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
            if (!Tabuleiro.Peca(origem).MovimentoPossivelPara(destino))
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
        
        public HashSet<Peca> PecasCapturadas(Cor cor)
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

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();              // Cria um conjunto de pecas auxiliar
            foreach (Peca peca in ConjuntoPecasNova)              // Percorre peca no Conjunto de pecas em jogo (novas)
            {
                if (peca.Cor == cor)                              // Se a peca for da cor informada no paramentro adiciona ela no conjunto aux
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));            // irá Pega todas as peças, exceto as que foram capturadas dessa cor
            return aux;                                         // Retorna conjunto auxiliar 
        }

        // Metodo para retornar a cor adversaria

        private Cor Adversaria (Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        // Metodo para retornar uma rei de uma dada cor

        private Peca Rei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))  // Percorrer todas as pecas do jogo até achar uma rei
            {
                if (peca is Rei)            // Se a peca pertence a subClasse Rei, retorna a peca, no caso o Rei
                {
                    return peca;
                }
            }
            return null;   // Se não achar o rei é porque tem algo errado, mas tenho que colocar o retur null para não deixar com erro.
        }

        // Metodo que retorna se o rei está em xeque

        public bool EstaEmXeque(Cor cor)
        {
            Peca r = Rei(cor);   // Pega o rei da cor adversaria
            if (r == null)      // Se não tiver rei, lança uma excecao
            {
                throw new TabuleiroExcecao("Não tem Rei da cor " + cor + "no tabuleiro!");
            }

            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))   //Para cada peca que está nas pecas adversarias que ainda esta em jogo
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[r.Posicao.Linha,r.Posicao.Coluna])  // Se na matriz de movimentos possiveis da peca adversaria (peca), se na posicao onde estiver o rei estiver verdadeiro é porque ele está em xeque 
                {
                    return true;
                }
            }
            return false;
        }

        // Metodo que verifica se está em xeque mate

        public bool TestaXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor))  // Percorre todas as pecas que estão em jogo da cor passada no parametro, preciso achar uma peca que movendo tira do xeque
            {
                bool[,] matriz = peca.MovimentosPossiveis(); //Pega todos os movimentos possiveis de cada peca

                for (int i = 0; i < Tabuleiro.Linhas; i++) // vou percorrer o tabuleiro inteiro e para cada movimento possivel, tenho que ver se tira do xeque , percorrendo linha
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++) // Percorrendo colunas
                    {
                        if (matriz[i,j]) // Se a matriz na possição i,j estiver marcada como verdadeira, ou seja, é um movimento possivel daquela peca
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j); // guardo o destino , para facilitar o codigo abaixo
                            Peca pecaCapturada = ExecutaMovimento(origem, destino); // Movimento a peca para esse lugar que ela poderá ir
                            bool estaEmXeque = EstaEmXeque(cor);                           // Ai depois que movimentei vou ver se ainda o rei está em xeque
                            DesfazMovimento(origem, destino, pecaCapturada);        // Desfaço o movimento que fiz anteriormente
                            if (!estaEmXeque)  // Se o rei não estiver em xeque, retorno false
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true; // Se depois de testar todas as pecas, todos os movimentos possiveis e nada tira o rei do xeque, então é true, ou seja, xeque mate
        }

        // Metodo que coloca nova peca no Tabuleiro em uma nova posicao

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna,linha).ToPosicao());
            ConjuntoPecasNova.Add(peca);
        }

        private void ColocarPecas()
        {
            // Testar En passant

            //ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            //ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));

            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this)); // Passando a partida para o rei
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this)); // Passando a partida para o rei
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        }
    }
}
