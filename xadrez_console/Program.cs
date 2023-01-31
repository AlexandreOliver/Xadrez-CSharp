using xadrez_console.tabuleiro;
using xadrez_console.pecasXadrez;
using xadrez_console;

try
{
Tabuleiro tab = new Tabuleiro(8, 8);

tab.colocarPeca(new Torre(tab, Cor.Branco), new Posicao(0, 0));
tab.colocarPeca(new Rei(tab, Cor.Branco), new Posicao(1, 3));
tab.colocarPeca(new Torre(tab, Cor.Branco), new Posicao(3, 3));
tab.colocarPeca(new Rei(tab, Cor.Branco), new Posicao(4, 0));



Tela.ImprimirTela(tab);
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}