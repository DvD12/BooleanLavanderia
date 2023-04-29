using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia
{
    public enum TipoProgramma
    {
        Rinfrescante,
        Rinnovante,
        Sgrassante,
        Rapido,
        Intenso
    }
    public class Programma
    {
        public int Numero { get; set; }
        public TipoProgramma Nome { get; set; }
        public int DurataMinuti { get; set; }
        public int NumeroGettoni { get; set; }
        public int ConsumoAmmorbidenteMillilitri { get; set; }
        public int ConsumoDetersivoMillilitri { get; set; }

        public int MinutiRimanenti { get; set; }

        public Programma() { }

        public Programma(int numero, TipoProgramma nome, int durataMinuti, int numeroGettoni, int consumoAmmorbidenteMillilitri = 0, int consumoDetersivoMillilitri = 0)
        {
            Numero = numero;
            Nome = nome;
            DurataMinuti = durataMinuti;
            NumeroGettoni = numeroGettoni;
            ConsumoAmmorbidenteMillilitri = consumoAmmorbidenteMillilitri;
            ConsumoDetersivoMillilitri = consumoDetersivoMillilitri;
        }

        public static List<Programma> asd = new List<Programma>()
        {
            new Programma(),
            new Programma(),
            new Programma(),
            new Programma()
            {
                 ConsumoAmmorbidenteMillilitri = 1,
                  Numero = 5
            }
        };
        public static Dictionary<TipoProgramma, Programma> Programmi { get; set; } = new Dictionary<TipoProgramma, Programma>()
        {
            { TipoProgramma.Rinfrescante, new Programma(1, TipoProgramma.Rinfrescante, 20, 5, 20, 25) },
            { TipoProgramma.Rinnovante, new Programma(2, TipoProgramma.Rinnovante, 40, 10, 40, 50) },
            { TipoProgramma.Sgrassante, new Programma(3, TipoProgramma.Sgrassante, 60, 15, 60, 100) },
            { TipoProgramma.Rapido, new Programma(1, TipoProgramma.Rapido, 20, 2) },
            { TipoProgramma.Intenso, new Programma(2, TipoProgramma.Intenso, 60, 4) },
        };
    }
}
