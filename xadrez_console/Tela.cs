using tabuleiro;
using xadrez;

namespace xadrez_console;

class Tela
{
    public static void ImprimirPartida(PartidaXadrez partida)
    {
        Tela.ImprimirTabuleiro(partida.tab);
        Console.WriteLine();
        ImprimirPecasCapturadas(partida);
        Console.WriteLine();
        Console.WriteLine("\nTurno: " + partida.turno);
        if (!partida.terminada)
        {
            Console.WriteLine("Aguardando Jogada do " + partida.jogadorAtual);
            if (partida.xeque)
            { Console.WriteLine("Xeque!"); }
        }
        else
        {
            Console.WriteLine("\nXeque-Mate!");
            Console.WriteLine("Vencedor: " + partida.jogadorAtual);
        }
    }

    
    public static void ImprimirPartida(PartidaXadrez partida, bool[,] posicoesPossiveis)
    {
        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);
        Console.WriteLine();
        ImprimirPecasCapturadas(partida);
        Console.WriteLine();
        Console.WriteLine("\nTurno: " + partida.turno);
        Console.WriteLine("Aguardando Jogada do " + partida.jogadorAtual);
    }

    public static void ImprimirPecasCapturadas(PartidaXadrez partida)
    {
        //Console.WriteLine("Pecas Capturadas: ");
        Console.Write("Brancas: ");
        ImprimirConjunto(partida.pecasCapturadas(Cor.Branco));
        Console.WriteLine();
        Console.Write("Pretas: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        ImprimirConjunto(partida.pecasCapturadas(Cor.Preto));
        Console.ForegroundColor = aux;
    }

    public static void ImprimirConjunto(HashSet<Peca> pecas)
    {
        Console.Write("[ ");
        foreach (Peca x in pecas)
        {
            Console.Write(x + " ");
        }
        Console.Write("]");
    }
    
    public static void ImprimirTabuleiro(Tabuleiro tab)
    {
        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write(8 - i + " |");
            for (int j = 0; j < tab.Colunas; j++)
            {
                imprimirPeca(tab.peca(i, j));
            }
        
            Console.WriteLine();
        }

        Console.WriteLine("   ------------------------");
        Console.WriteLine("    a  b  c  d  e  f  g  h");
    }

    public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
    {
        ConsoleColor fundoOriginal = Console.BackgroundColor;
        ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write(8 - i + " |");
            for (int j = 0; j < tab.Colunas; j++)
            {
                if (posicoesPossiveis[i, j])
                {
                    Console.BackgroundColor = fundoAlterado;
                }
                else
                {
                    Console.BackgroundColor = fundoOriginal;
                }
                
                imprimirPeca(tab.peca(i, j));
                Console.BackgroundColor = fundoOriginal;

            }
        
            Console.WriteLine();
        }

        Console.BackgroundColor = fundoOriginal;
        Console.WriteLine("   ------------------------");
        Console.WriteLine("    a  b  c  d  e  f  g  h");
    }
    public static void imprimirPeca(Peca peca)
    {
        if (peca == null)
        {
            Console.Write(" - ");
        }
        else
        {
            if (peca.Cor == Cor.Branco)
                Console.Write(" " + peca);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" " + peca);
                Console.ForegroundColor = aux;
            }

            Console.Write(" ");
        }
    }

    public static PosicaoXadrez lerPosicaoXadrez()
    {
        string? s = Console.ReadLine();
        char coluna = s[0];
        int linha = int.Parse(s[1] + "");
        return new PosicaoXadrez(coluna, linha);
    }
}