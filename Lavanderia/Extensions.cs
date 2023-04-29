using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia
{
    public static class Extensions
    {
        public static T GetRandomElement<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            var rand = new System.Random();
            var i = rand.Next(0, values.Count);
            return values[i];
        }
    }
}
