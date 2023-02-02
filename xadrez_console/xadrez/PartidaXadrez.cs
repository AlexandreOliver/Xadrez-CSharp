using tabuleiro;


namespace xadrez;

class PartidaXadrez
{
    public Tabuleiro tab { get; private set; }
    public int turno { get; private set; }
    public Cor jogadorAtual { get; private set; }
    public bool terminada { get; private set; }

    public PartidaXadrez()
    {
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branco;
        terminada = false;
        
        colocarPecas();
    }

    public void executarMovimento(Posicao origem, Posicao destino)
    {
        
        Peca? p = tab.retirarPeca(origem);
        p.addMovimento();
        Peca? PecaCaptu = tab.retirarPeca(destino);
        tab.colocarPeca(p, destino);
    }

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        executarMovimento(origem, destino);
        turno++;
        mudaJogador();
    }

    public void validarPosicaoDeOrigem(Posicao pos)
    {
        if (tab.peca(pos) == null)
        {
            throw new TabuleiroException("Nao existe peca nessa posicao!");
        }
        if (jogadorAtual != tab.peca(pos).Cor)
        {
            throw new TabuleiroException("A peca escolhida nao e sua!");
        }
        if (!tab.peca(pos).existeMovimentoPossiveis())
        {
            throw new TabuleiroException("Nao ha movimentos possiveis para essa peca!");
        }
    }

    public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!tab.peca(origem).podeMoverPara(destino))
        {
            throw new TabuleiroException("Voce nao pode mover para ai!");
        }
    }

    private void mudaJogador()
    {
        jogadorAtual = (jogadorAtual == Cor.Branco)? Cor.Preto : Cor.Branco;
    }

    public void colocarPecas()
    {
        tab.colocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('c', 1).toPosicao());
        tab.colocarPeca(new Rei(tab, Cor.Branco), new PosicaoXadrez('c', 2).toPosicao());

        tab.colocarPeca(new Rei(tab, Cor.Branco), new PosicaoXadrez('d', 1).toPosicao());
        tab.colocarPeca(new Rei(tab, Cor.Preto), new PosicaoXadrez('f', 6).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('b', 1).toPosicao());
    }
}