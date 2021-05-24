using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino,
            double valorEsperado,
            double[] ofertas)
        {
            // Arranje - Given
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, ofertas[i]);
                else
                    leilao.RecebeLance(maria, ofertas[i]);

            }

            // Act - When
            leilao.TerminaPregao();

            // Assert - Then
            double valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            // Arranje - Given
            IModalidadeAvaliacao modalidade = new OfertaMaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            leilao.IniciaPregao();

            foreach (var valor in ofertas)
                leilao.RecebeLance(new Interessada("Fulano " + valor, leilao), valor);

            // Act - When
            leilao.TerminaPregao();

            // Assert - Then
            double valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        private static void LancaInvalidOperationExceptionDadoLeilaoAguardandoInicio()
        {
            // Arranje - Given
            var leilao = new Leilao("Van Gogh");
            // Assert - Then
            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                // Act - When
                () => leilao.TerminaPregao());

            string msgEsperada = "Não é possível terminar um leilao que está aguardando o seu ínicio.";
            Assert.Equal(msgEsperada, excecaoObtida.Message);
        }

        [Fact]
        private static void RetornaZeroDadoLeilaoSemLances()
        {
            // Arranje - Given
            var leilao = new Leilao("Van Gogh");

            leilao.IniciaPregao();

            // Act - When
            leilao.TerminaPregao();

            // Assert - Then
            double valorEsperado = 0;
            double valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
