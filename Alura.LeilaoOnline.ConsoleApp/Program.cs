using Alura.LeilaoOnline.Core;
using System;
using System.Reflection;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void LeilaoComVariosLances()
        {
            var leilao = new Leilao("Van Gogh");

            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);

            leilao.TerminaPregao();

            double valorEsperado = 1000;
            double valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido, MethodBase.GetCurrentMethod().Name);
        }

        private static void Verifica(double valorEsperado, double valorObtido, string metodoInvoke)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(metodoInvoke + ": ");

            if (valorEsperado == valorObtido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("TESTE OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"TESTE FALHOU! Esperado: {valorEsperado}, obtido: {valorObtido}");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\r\n");
        }

        private static void LeilaoComApenasUmLance()
        {
            var leilao = new Leilao("Van Gogh");

            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            leilao.TerminaPregao();

            double valorEsperado = 800;
            double valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido, MethodBase.GetCurrentMethod().Name);
        }

        static void Main(string[] args)
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLance();
        }
    }
}