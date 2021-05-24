using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            // Arranje - Given
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

        
            leilao.RecebeLance(fulano, 800);


            // Act - When
            leilao.RecebeLance(fulano, 900);
            

            // Assert - Then
            int qtdEsperada = 1;
            int qtdObtida = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdObtida);
        }

        [Theory]
        [InlineData(4, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] ofertas)
        {
            // Arranje - Given
            var leilao = new Leilao("Van Gogh");
            
            leilao.IniciaPregao();

            foreach (var valor in ofertas)
                leilao.RecebeLance(new Interessada("Fulano " + valor, leilao), valor);

            leilao.TerminaPregao();

            // Act - When
            leilao.RecebeLance(new Interessada("Fulano Leilao Finalizado", leilao), 1500);
            
            // Assert - Then
            int qtdObtida = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdObtida);
        }
    }
}
