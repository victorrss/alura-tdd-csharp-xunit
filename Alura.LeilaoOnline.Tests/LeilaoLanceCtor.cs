using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoLanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            // Arranje - Given
            var valorNegativo = -100;

            // Assert - Then
            var exceptionObtida = Assert.Throws<ArgumentException>(
                // Act - When
                () => new Lance(null, valorNegativo));

            // Assert - Then
            string msgEsperada = "Valor do lance é negativo";
            Assert.Equal(msgEsperada, exceptionObtida.Message);
        }
    }
}
