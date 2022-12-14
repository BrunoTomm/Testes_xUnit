using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(2, new double[] {800, 900})]
        [InlineData(4, new double[] {1000, 1200, 1400, 1600})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int qtdEsperada, double[] ofertas)
        {
            /*
              CENARIO
                  Dado leilao finalizado com X lances
                  quando o leilao recebe nova oferta de lance
                  entao a qtde de lances continua sendo X
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
                if ((i%2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }

            }

            leilao.TerminaPregao();

            // Act
            // Qual o método a ser testado:
            leilao.RecebeLance(fulano, 1000);

            // Acert
            // Verificamos o resultado esperado:
            var qtdObtida = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtida);
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            /*
              CENARIO
                  Dado leilao iniciado E interessado X realizou o ultimo lance
                  Quando mesmo interessado X realiza o próximo lance
                  Entao leilao nao aceita o segundo lance
            */

            // Arranje 
            // Todas as pré-definições de entrada ou cenário:
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);


            // Act
            // Qual o método a ser testado:
            leilao.RecebeLance(fulano, 1000);

            // Acert
            // Verificamos o resultado esperado:
            var qtdEsperada = 1;
            var qtdObtida = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtida);
        }
    }
}
