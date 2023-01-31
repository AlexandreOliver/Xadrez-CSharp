namespace tabuleiro;

class Tabuleiro
{
    public int Linhas { get; set; }
    public int Colunas { get; set; }
    private Peca[,] pecas;

    public Tabuleiro(int linhas, int colunas)
    {
        Linhas = linhas;
        Colunas = colunas;
        pecas = new Peca[linhas, colunas];
    }

    public Peca peca(int linha, int coluna)
    {
        return pecas[linha, coluna];
    }

    public Peca peca(Posicao pos)
    {
        return pecas[pos.Linha, pos.Coluna];
    }

    public bool existePeca(Posicao pos)
    {
        validarPosicao(pos);
        return peca(pos) != null;
    }

    public void colocarPeca(Peca peca, Posicao posicao)
    {
        if (existePeca(posicao))
        {
            throw new TabuleiroException("Já existe uma peça nessa posição");
        }
        pecas[posicao.Linha, posicao.Coluna] = peca;

        peca.Posicao = posicao;
    }

    public bool posicaoValida(Posicao pos)
    {
        if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
        {
            return false;
        }
        return true;
    }

    public void validarPosicao(Posicao pos)
    {
        if (!posicaoValida(pos))
        {
            throw new TabuleiroException("Posição Invalida!");
        }
    }
}