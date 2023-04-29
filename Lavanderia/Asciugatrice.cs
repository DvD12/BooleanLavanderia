using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia
{
    public class Asciugatrice : Macchina
    {
        public override List<TipoProgramma> ListaProgrammi { get; protected set; } = new()
        {
            TipoProgramma.Rapido,
            TipoProgramma.Intenso
        };
    }
}
