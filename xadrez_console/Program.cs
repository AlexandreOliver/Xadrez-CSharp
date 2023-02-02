using tabuleiro;
using xadrez;
using xadrez_console;

try
{
    PartidaXadrez partida = new PartidaXadrez();

    while (!partida.terminada)
    {
        try
        {
            Console.Clear();
            Tela.ImprimirPartida(partida);

            Console.Write("\nOrigem: ");
            Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
            partida.validarPosicaoDeOrigem(origem);

            bool[,] posicaoPossiveis = partida.tab.peca(origem).movimentosPossiveis();

            Console.Clear();
            Tela.ImprimirPartida(partida, posicaoPossiveis);            

            Console.Write("\nDestino: ");
            Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
            partida.validarPosicaoDeDestino(origem, destino);

            partida.realizaJogada(origem, destino);
        }
        catch (TabuleiroException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }

    Console.Clear();
    Tela.ImprimirPartida(partida);
    
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}