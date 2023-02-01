namespace tabuleiro;

class Peca
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
}