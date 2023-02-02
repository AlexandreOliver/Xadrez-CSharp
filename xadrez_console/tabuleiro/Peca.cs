namespace tabuleiro;

abstract class Peca
{
    public Posicao? Posicao { get; set; }
    public Cor Cor { get; protected set; }
    public int QntMovimentos { get; protected set; }
    public Tabuleiro Tab { get; protected set; } 

    public Peca(Tabuleiro tab, Cor cor)
    {
        Posicao = null;
        Cor = cor;
        QntMovimentos = 0;
        Tab = tab;
    }

    public void addMovimento()
    {
        QntMovimentos++; 
    }

    public void decrementarMovimento()
    { QntMovimentos--;}

    public abstract bool[,] movimentosPossiveis();

    public bool existeMovimentoPossiveis()
    {
        bool[,] mat = movimentosPossiveis();
        for (int i = 0; i < Tab.Linhas; i++)
        {
            for (int j = 0; j < Tab.Colunas; j++)
            {
                if (mat[i,j])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool movimentoPossivel(Posicao pos)
    {
        return movimentosPossiveis()[pos.Linha, pos.Coluna];
    }
}