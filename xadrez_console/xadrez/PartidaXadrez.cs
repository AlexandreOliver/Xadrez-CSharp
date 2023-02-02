using tabuleiro;


namespace xadrez;

class PartidaXadrez
{
    public Tabuleiro tab { get; private set; }
    public int turno { get; private set; }
    public Cor jogadorAtual { get; private set; }
    public bool terminada { get; private set; }
    private HashSet<Peca> pecas;
    private HashSet<Peca> capturadas;
    public bool xeque { get; private set;}


    public PartidaXadrez()
    {
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branco;
        terminada = false;
        xeque = false;
        pecas = new HashSet<Peca> ();
        capturadas = new HashSet<Peca> ();
        colocarPecas();
    }

    public Peca executarMovimento(Posicao origem, Posicao destino)
    {
        
        Peca? p = tab.retirarPeca(origem);
        p.addMovimento();
        Peca? PecaCapturada = tab.retirarPeca(destino);
        tab.colocarPeca(p, destino);

        if (PecaCapturada != null)
        {
            capturadas.Add(PecaCapturada);
        }
        
        return PecaCapturada;
    } 

    public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = tab.retirarPeca(destino);
        p.decrementarMovimento();
        if (pecaCapturada != null)
        {
            tab.colocarPeca(pecaCapturada, destino);
            capturadas.Remove(pecaCapturada);
        }

        tab.colocarPeca(p, origem);
    }

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        
        Peca pecaCapturada = executarMovimento(origem, destino);

        if (estaEmXeque(jogadorAtual))
        {
            desfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Movimento Invalido: Seu rei esta em xeque!");
        }

        xeque = (estaEmXeque(adversario(jogadorAtual)))? true : false;

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

    public HashSet<Peca> pecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca> ();
        foreach (Peca x in capturadas)
        {
            if (x.Cor == cor)
            {
                aux.Add(x);
            }
        }

        return aux;
    }

    public HashSet<Peca> pecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca> ();
        foreach (Peca x in pecas)
        {
            if (x.Cor == cor)
            {
                aux.Add(x);
            }
        }

        aux.ExceptWith(pecasCapturadas(cor));
        return aux;
    }

    private Cor adversario(Cor cor)
    {
        return (cor == Cor.Branco)? Cor.Preto : Cor.Branco;
    }

    private Peca? rei(Cor cor)
    {
        foreach (Peca x in pecasEmJogo(cor))
        {
            if (x is Rei)
            {
                return x;
            }
        }
        return null;
    }

    public bool estaEmXeque(Cor cor)
    {
        Peca? R = rei(cor);
        if  (R == null) 
        { throw new TabuleiroException("Nao tem rei da cor" + cor); }

        foreach (Peca x in pecasEmJogo(adversario(cor)))
        {
            bool[,] mat = x.movimentosPossiveis();
            if (mat[R.Posicao.Linha, R.Posicao.Coluna])
            {
                return true;
            }
        }

        return false;
    }

    public void colocarNovaPeca(char coluna, int linha, Peca peca)
    {
        tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        pecas.Add(peca);
    }

    public void colocarPecas()
    {
        colocarNovaPeca('a', 8, new Torre(tab, Cor.Branco));
        colocarNovaPeca('a', 1, new Torre(tab, Cor.Preto));
        colocarNovaPeca('e', 8, new Rei(tab, Cor.Branco));
        colocarNovaPeca('e', 1, new Rei(tab, Cor.Preto));
        colocarNovaPeca('h', 8, new Torre(tab, Cor.Branco));
        colocarNovaPeca('h', 1, new Torre(tab, Cor.Preto));
    }
}