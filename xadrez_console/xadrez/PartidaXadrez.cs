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
    public Peca? vulneravelEnPassant { get; private set; }


    public PartidaXadrez()
    {
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branco;
        terminada = false;
        vulneravelEnPassant = null;
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
        
        // #JogadaEspecial roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = tab.retirarPeca(origemTorre);
            T.addMovimento();
            tab.colocarPeca(T, destinoTorre);

        }

        // #JogadaEspecial roque grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = tab.retirarPeca(origemTorre);
            T.addMovimento();
            tab.colocarPeca(T, destinoTorre);

        }

        // #JogadaEspecial EnPassant
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna && PecaCapturada == null)
            {
                Posicao posicaoPeao;
                if (p.Cor == Cor.Branco)
                {
                    posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);

                }
                else
                {
                    posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                }

                PecaCapturada = tab.retirarPeca(posicaoPeao);
                capturadas.Add(PecaCapturada);
            }
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

        // #JogadaEspecial roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = tab.retirarPeca(destinoTorre);
            T.decrementarMovimento();
            tab.colocarPeca(T, origemTorre);
        }

        // #JogadaEspecial roque grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = tab.retirarPeca(destinoTorre);
            T.decrementarMovimento();
            tab.colocarPeca(T, origemTorre);
        }


        //#JogadaEspecial EnPassant
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
            {
                Peca peao = tab.retirarPeca(destino);
                Posicao posicaoPeao;
                if (peao.Cor == Cor.Branco)
                {
                    posicaoPeao = new Posicao(3, destino.Coluna);

                }
                else
                {
                    posicaoPeao = new Posicao(4, destino.Coluna);
                }

                tab.colocarPeca(peao, posicaoPeao);
            }
        }
    }

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        
        Peca pecaCapturada = executarMovimento(origem, destino);

        if (estaEmXeque(jogadorAtual))
        {
            desfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Movimento Invalido: Seu rei esta em xeque!");
        }

        Peca p = tab.peca(destino);

        // JogadaEspecial Promocao
        if (p is Peao)
        {
            if (p.Cor == Cor.Branco && destino.Linha == 0 || p.Cor == Cor.Preto && destino.Linha == tab.Linhas - 1)
            {
                p = tab.retirarPeca(destino);
                pecas.Remove(p);
                Peca dama = new Dama(tab, p.Cor);
                tab.colocarPeca(dama, destino);
                pecas.Add(dama);
            }
        }
        
        xeque = (estaEmXeque(adversario(jogadorAtual)))? true : false;

        if (testeXequeMate(adversario(jogadorAtual)))
        {
            terminada = true;
        }
        else
        {
            turno++;
            mudaJogador();
        } 
        

        //#JogadaEspecial EnPassant
        if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
        {
            vulneravelEnPassant = p;

        }
        else
        {
            vulneravelEnPassant = null;
        }
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
        if (!tab.peca(origem).movimentoPossivel(destino))
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

    public bool testeXequeMate(Cor cor)
    {
        if (!estaEmXeque(cor))
        {
            return false;
        }
        
        foreach (Peca x in pecasEmJogo(cor))
        {
            bool[,] mat = x.movimentosPossiveis();
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        Posicao origem = x.Posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = executarMovimento(origem, destino);
                        bool testeXeque = estaEmXeque(cor);
                        desfazMovimento(origem, destino, pecaCapturada);
                        if (!testeXeque)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    public void colocarNovaPeca(char coluna, int linha, Peca peca)
    {
        tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        pecas.Add(peca);
    }

    public void colocarPecas()
    {
        colocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
        colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branco));
        colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branco));
        colocarNovaPeca('d', 1, new Dama(tab, Cor.Branco));
        colocarNovaPeca('e', 1, new Rei(tab, Cor.Branco, this));
        colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branco));
        colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branco));
        colocarNovaPeca('h', 1, new Torre(tab, Cor.Branco));
        colocarNovaPeca('a', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('b', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('c', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('d', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('e', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('f', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('g', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('h', 2, new Peao(tab, Cor.Branco, this));


        colocarNovaPeca('a', 8, new Torre(tab, Cor.Preto));
        colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preto));
        colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preto));
        colocarNovaPeca('d', 8, new Dama(tab, Cor.Preto));
        colocarNovaPeca('e', 8, new Rei(tab, Cor.Preto, this));
        colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preto));
        colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preto));
        colocarNovaPeca('h', 8, new Torre(tab, Cor.Preto));
        colocarNovaPeca('a', 7, new Peao(tab, Cor.Preto, this));
        colocarNovaPeca('b', 7, new Peao(tab, Cor.Preto, this));
        colocarNovaPeca('c', 7, new Peao(tab, Cor.Preto, this));
        colocarNovaPeca('d', 7, new Peao(tab, Cor.Preto, this));
        colocarNovaPeca('e', 7, new Peao(tab, Cor.Preto, this));
        colocarNovaPeca('f', 7, new Peao(tab, Cor.Preto, this));
        colocarNovaPeca('g', 7, new Peao(tab, Cor.Preto, this));
        colocarNovaPeca('h', 7, new Peao(tab, Cor.Preto, this));
    }
}