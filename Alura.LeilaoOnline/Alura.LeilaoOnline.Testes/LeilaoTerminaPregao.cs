using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTerminaPregao
    {
        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {

            // Arranje 
            // Todas as pré-definições de entrada ou cenário:
            var leilao = new Leilao("Van Gogh");

            //Assert
            var excessaoObtida = Assert.Throws<InvalidOperationException>(
                // Act - Qual o método a ser testado:
                () => leilao.TerminaPregao()
            );
            var msgEsperada = "Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método IniciaPregao()";

            Assert.Equal(msgEsperada, excessaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            /*
            CENARIO
                Dado leilao sem qualquer lance
                quando o pregao/ leilao termina
                entao o valor do lance ganhador é zero
             */

            // Arranje 
            // Todas as pré-definições de entrada ou cenário:
            var leilao = new Leilao("Van Gogh");

            leilao.IniciaPregao();

            // Act
            // Qual o método a ser testado:
            leilao.TerminaPregao();

            // Acert
            // Verificamos o resultado esperado:
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            /* 
             CENARIO
                Dado leilao pelo menos um lance
                quando o pregao/leilao termina
                entao o valor esperado é o maior valor dado
                e o cliente ganhador é o que deu o maior lance
             */

            // Arranje 
            // Todas as pré-definições de entrada ou cenário:
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }

            }

            // Act
            // Qual o método a ser testado:
            leilao.TerminaPregao();


            // Acert
            // Verificamos o resultado esperado:
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }


    }
}
