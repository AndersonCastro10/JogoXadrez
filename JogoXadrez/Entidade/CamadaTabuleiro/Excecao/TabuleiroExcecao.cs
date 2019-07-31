using System;

namespace JogoXadrez.Entidade.CamadaTabuleiro.Excecao
{
    class TabuleiroExcecao : Exception
    {
        public TabuleiroExcecao(string message) : base(message)
        {
        }
    }
}
