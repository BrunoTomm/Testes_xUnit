using Alura.LeilaoOnline.Core;

class program
{
    private static void Verifica(double esperado, double obtido)
    {
        var cor = Console.ForegroundColor;

        if (esperado == obtido)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TESTE OK");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"TESTE FALHOU! Esperado: {esperado}, obtido: {obtido}.");
        }

        Console.ForegroundColor = cor;
    }
    private static void LeilaoComVariosLances()
    {
        // Arranje 
        // Todas as pré-definições de entrada ou cenário:
        var leilao = new Leilao("Van Gogh");
        var fulano = new Interessada("Fulano", leilao);
        var maria = new Interessada("Maria", leilao);

        leilao.RecebeLance(fulano, 800);
        leilao.RecebeLance(maria, 800);
        leilao.RecebeLance(fulano, 1000);

        // Act
        // Qual o método a ser testado:
        leilao.TerminaPregao();


        // Acert
        // Verificamos o resultado esperado:
        var valorEsperado = 1000;
        var valorObtido = leilao.Ganhador.Valor;

        Verifica(valorEsperado, valorObtido);

    }
    private static void LeilaoComApenasUmLance()
    {
        // Arranje 
        // Todas as pré-definições de entrada ou cenário:
        var leilao = new Leilao("Van Gogh");
        var fulano = new Interessada("Fulano", leilao);

        leilao.RecebeLance(fulano, 600);

        // Act
        // Qual o método a ser testado:
        leilao.TerminaPregao();


        // Acert
        // Verificamos o resultado esperado:
        var valorEsperado = 800;
        var valorObtido = leilao.Ganhador.Valor;

        Verifica(valorEsperado, valorObtido);
    }

    static void Main()
    {
        LeilaoComVariosLances();
        LeilaoComApenasUmLance();
    }

}
