using System;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoEnum
    {
        AguardandoInicio,
        EmAndamento,
        Finalizado
    }

    public enum ModalidadeOfertaEnum
    {
        SuperiorMaisProxima,
        MaiorValor
    }
    public class Leilao
    {
        private IList<Lance> _lances;
        private IModalidadeAvaliacao _avaliador;
        private Interessada _ultimoCliente;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; set; }
        public EstadoEnum Estado { get; private set; }

        public Leilao(string peca)
        {
            Estado = EstadoEnum.AguardandoInicio;
            Peca = peca;
            _lances = new List<Lance>();
            _avaliador = new OfertaMaiorValor();
        }

        public Leilao(string peca, IModalidadeAvaliacao avaliador) : this(peca)
        {
            _avaliador = avaliador;
        }

        private bool ValidarLance(Interessada cliente, double valor)
        {
            return Estado == EstadoEnum.EmAndamento
                && cliente != _ultimoCliente;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (ValidarLance(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoEnum.EmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado == EstadoEnum.AguardandoInicio)
                throw new InvalidOperationException("Não é possível terminar um leilao que está aguardando o seu ínicio.");

            Ganhador = _avaliador.Avalia(this);
            Estado = EstadoEnum.Finalizado;
        }
    }
}
