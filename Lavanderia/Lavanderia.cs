using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia
{
    public static class Lavanderia
    {
        public static Dictionary<int, Macchina> Macchine { get; set; } = new Dictionary<int, Macchina>()
        {
            { 1, new Lavatrice() },
            { 2, new Lavatrice() },
            { 3, new Lavatrice() },
            { 4, new Asciugatrice() },
            { 5, new Asciugatrice() },
        };
        public static HashSet<int> Numeri { get; set; } = new HashSet<int>() { 1, 2, 3, 1 };

        public static void Simula(int minuti)
        {
            foreach (var m in Macchine.Values)
                m.Simula(minuti);
        }
    }
}
