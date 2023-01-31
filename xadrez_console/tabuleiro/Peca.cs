namespace xadrez_console.tabuleiro;

class Peca
{


    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; }
    public int QntMovimentos { get; protected set; }
    public Tabuleiro Tab { get; protected set; } 

    public Peca(Posicao posicao, Tabuleiro tab, Cor cor)
    {
        Posicao = posicao;
        Cor = cor;
        QntMovimentos = 0;
        Tab = tab;
    }    
}