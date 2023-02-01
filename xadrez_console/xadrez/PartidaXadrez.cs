using tabuleiro;


namespace xadrez;

class PartidaXadrez
{
    public Tabuleiro tab { get; private set; }
    private int turno;
    private Cor jogadorAtual;
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

    public void colocarPecas()
    {
        tab.colocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('c', 1).toPosicao());
        tab.colocarPeca(new Rei(tab, Cor.Branco), new PosicaoXadrez('d', 4).toPosicao());
        tab.colocarPeca(new Rei(tab, Cor.Preto), new PosicaoXadrez('f', 6).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('b', 1).toPosicao());
    }
}