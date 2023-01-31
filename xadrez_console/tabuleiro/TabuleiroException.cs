using System;


namespace xadrez_console.tabuleiro;

class TabuleiroException : Exception
{
    public TabuleiroException(string message)
        : base(message)
    {

    }
}