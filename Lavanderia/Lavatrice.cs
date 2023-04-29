using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia
{
    public class Lavatrice : Macchina
    {
        public Lavatrice() : base()
        {
            var rand = new System.Random();
            DetersivoMillilitri = rand.Next(0, Lavatrice.MAX_DETERSIVO_MILLILITRI);
            AmmorbidenteMillilitri = rand.Next(0, Lavatrice.MAX_AMMORBIDENTE_MILLILITRI);
        }

        public override List<TipoProgramma> ListaProgrammi { get; protected set; } = new List<TipoProgramma>()
        {
            TipoProgramma.Rinfrescante,
            TipoProgramma.Rinnovante,
            TipoProgramma.Sgrassante
        };

        public const int MAX_DETERSIVO_MILLILITRI = 1000;
        public const int MAX_AMMORBIDENTE_MILLILITRI = 500;

        private int _DetersivoMillilitri;
        public int DetersivoMillilitri
        {
            get
            {
                return _DetersivoMillilitri;
            }
            set
            {
                value = Math.Min(MAX_DETERSIVO_MILLILITRI, value);
                value = Math.Max(0, value);

                _DetersivoMillilitri = value;
            }
        }
        public int _AmmorbidenteMillilitri;
        public int AmmorbidenteMillilitri
        {
            get
            {
                return _AmmorbidenteMillilitri;
            }
            set
            {
                value = Math.Min(MAX_AMMORBIDENTE_MILLILITRI, value);
                value = Math.Max(0, value);

                _AmmorbidenteMillilitri = value;
            }
        }

        protected override bool CheckAvvioProgramma()
        {
            if (base.CheckAvvioProgramma() == false)
                return false;
            if (DefinizioneProgrammaSelezionato.ConsumoAmmorbidenteMillilitri > AmmorbidenteMillilitri)
            {
                Console.WriteLine($"Non c'è abbastanza ammorbidente! Richiesto: {DefinizioneProgrammaSelezionato.ConsumoAmmorbidenteMillilitri}, disponibile: {AmmorbidenteMillilitri}");
                return false;
            }
            if (DefinizioneProgrammaSelezionato.ConsumoDetersivoMillilitri > DetersivoMillilitri)
            {
                Console.WriteLine($"Non c'è abbastanza detersivo! Richiesto: {DefinizioneProgrammaSelezionato.ConsumoDetersivoMillilitri}, disponibile: {DetersivoMillilitri}");
                return false;
            }

            return true;
        }

        public override bool AvviaProgramma()
        {
            if (CheckAvvioProgramma() == true)
            {
                bool avviato = base.AvviaProgramma();
                if (avviato)
                {
                    this.AmmorbidenteMillilitri -= DefinizioneProgrammaSelezionato.ConsumoAmmorbidenteMillilitri;
                    this.DetersivoMillilitri -= DefinizioneProgrammaSelezionato.ConsumoDetersivoMillilitri;
                }
                return avviato;
            }
            return false;
        }
    }
}
