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
}