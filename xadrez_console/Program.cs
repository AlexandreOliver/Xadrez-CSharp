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

    Console.Write("Origem: ");
    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
    Console.Write("Destino: ");
    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

    partida.executarMovimento(origem, destino);
}



}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}