using tabuleiro;
using xadrez;
using xadrez_console;

try
{
Tabuleiro tab = new Tabuleiro(8, 8);

tab.colocarPeca(new Torre(tab, Cor.Branco), new Posicao(0, 0));
tab.colocarPeca(new Rei(tab, Cor.Branco), new Posicao(1, 3));
tab.colocarPeca(new Torre(tab, Cor.Preto), new Posicao(3, 3));
tab.colocarPeca(new Rei(tab, Cor.Preto), new Posicao(4, 0));



Tela.ImprimirTabuleiro(tab);
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}