using tabuleiro;
using xadrez;
using xadrez_console;

try
{
PartidaXadrez partida = new PartidaXadrez();

while (!partida.terminada)
{
    Console.Clear();
    Tela.ImprimirTabuleiro(partida.tab);

    Console.Write("\nOrigem: ");
    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

    bool[,] posicaoPossiveis = partida.tab.peca(origem).movimentosPossiveis();

    Console.Clear();
    Tela.ImprimirTabuleiro(partida.tab, posicaoPossiveis);


    Console.Write("\nDestino: ");
    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

    partida.executarMovimento(origem, destino);
}



}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}