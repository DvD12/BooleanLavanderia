using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia
{
    public enum StatoSportello
    {
        Aperto,
        Chiuso
    }
    public enum StatoFunzione
    {
        InFunzione,
        Ferma
    }
    public abstract class Macchina
    {
        public abstract List<TipoProgramma> ListaProgrammi { get; protected set; }
        public int DurataRimanenteMinutiProgramma { get; set; }
        public StatoSportello StatoSportello { get; set; }
        public StatoFunzione StatoFunzione { get; set; }

        public TipoProgramma? ProgrammaSelezionato { get; set; }
        public int NumeroGettoni { get; set; }

        public Programma DefinizioneProgrammaSelezionato => Programma.Programmi[this.ProgrammaSelezionato.Value];

        public Macchina()
        {
            var rand = new System.Random();
            StatoSportello = Extensions.GetRandomElement<StatoSportello>();
            NumeroGettoni = rand.Next(0, 16);
            StatoFunzione = StatoFunzione.Ferma;
        }

        public void Simula(int minuti)
        {
            if (StatoFunzione == StatoFunzione.InFunzione)
            {
                DurataRimanenteMinutiProgramma -= minuti;
                DurataRimanenteMinutiProgramma = Math.Max(0, DurataRimanenteMinutiProgramma);

                if (DurataRimanenteMinutiProgramma == 0)
                    FermaProgramma();
            }
        }

        public void ApriSportello()
        {
            if (StatoFunzione == StatoFunzione.InFunzione)
            {
                Console.WriteLine("Non posso aprire mentre la macchina è in funzione!");
                return;
            }
            StatoSportello = StatoSportello.Aperto;
            Console.WriteLine("Sportello aperto");
        }
        public void ChiudiSportello()
        {
            StatoSportello = StatoSportello.Chiuso;
            Console.WriteLine("Sportello chiuso");
        }
        public void InserisciGettoni(int gettoni)
        {
            if (gettoni <= 0)
            {
                Console.WriteLine("Il numero di gettoni dev'essere positivo");
                return;
            }

            NumeroGettoni += gettoni;
            Console.WriteLine($"La macchina ha ora {NumeroGettoni} gettoni");
        }


        public void SelezionaProgramma(int numeroProgramma)
        {
            numeroProgramma -= 1;
            if (this.ListaProgrammi.Count <= numeroProgramma)
            {
                Console.WriteLine("Il programma selezionato è inesistente!");
                return;
            }
            SelezionaProgramma(this.ListaProgrammi[numeroProgramma]);

            /*
            if (!Enum.IsDefined(typeof(TipoProgramma), numeroProgramma))
            {
                Console.WriteLine("Il programma selezionato è inesistente!");
                return;
            }

            var p = (TipoProgramma)numeroProgramma;
            SelezionaProgramma(p);*/
        }
        private void SelezionaProgramma(TipoProgramma programma)
        {
            if (StatoFunzione == StatoFunzione.InFunzione)
            {
                Console.WriteLine("Programma già in esecuzione!");
                return;
            }
            if (!ListaProgrammi.Contains(programma))
            {
                Console.WriteLine($"La macchina non prevede il programma {programma}");
            }    
            ProgrammaSelezionato = programma;
            Console.WriteLine($"Programma selezionato: {ProgrammaSelezionato}");
        }

        public List<TipoProgramma> GetListaProgrammi() => ListaProgrammi;

        public virtual bool AvviaProgramma()
        {
            var oldStatoFunzione = StatoFunzione;
            StatoFunzione = StatoFunzione.InFunzione;
            Console.WriteLine("Programma messo in moto");
            if (oldStatoFunzione != StatoFunzione)
            {
                this.NumeroGettoni -= DefinizioneProgrammaSelezionato.NumeroGettoni;
                this.DurataRimanenteMinutiProgramma = DefinizioneProgrammaSelezionato.DurataMinuti;
            }
            return oldStatoFunzione != StatoFunzione; // true -> c'è stato un cambiamento di stato
        }

        protected virtual bool CheckAvvioProgramma()
        {
            if (ProgrammaSelezionato == null)
            {
                Console.WriteLine("Nessun programma selezionato da eseguire!");
                return false;
            }
            if (StatoFunzione == StatoFunzione.InFunzione)
            {
                Console.WriteLine("C'è già un programma in esecuzione!");
                return false;
            }
            if (StatoSportello == StatoSportello.Aperto)
            {
                Console.WriteLine("Sportello aperto -- impossibile eseguire il programma!");
                return false;
            }
            if (DefinizioneProgrammaSelezionato.NumeroGettoni > this.NumeroGettoni)
            {
                Console.WriteLine($"Non hai abbastanza gettoni! Richiesti: {DefinizioneProgrammaSelezionato.NumeroGettoni}; disponibili: {this.NumeroGettoni}");
                return false;
            }
            return true;
        }

        public void FermaProgramma()
        {
            if (ProgrammaSelezionato == null)
            {
                Console.WriteLine("Nessun programma selezionato da fermare!");
                return;
            }
            if (StatoFunzione == StatoFunzione.InFunzione)
                Console.WriteLine("Programma fermato");
            StatoFunzione = StatoFunzione.Ferma;
        }
    }
}
