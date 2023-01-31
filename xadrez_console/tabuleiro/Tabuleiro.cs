namespace xadrez_console.tabuleiro;

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

    public void colocarPeca(Peca peca, Posicao posicao)
    {
        pecas[posicao.Linha, posicao.Coluna] = peca;

        peca.Posicao = posicao;
    }
}