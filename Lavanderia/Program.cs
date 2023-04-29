using ConsoleTables;

namespace Lavanderia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string rawInput = "";
            while (rawInput != "esci")
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("=========================================================");
                var tabellaMacchine = new ConsoleTable("Numero", "Tipo", "Gettoni", "Aperta", "In funzione", "Programma", "Tempo rimanente", "Detersivo", "Ammorbidente");
                foreach (var m in Lavanderia.Macchine)
                {
                    int detersivo = m.Value is Lavatrice l ? l.DetersivoMillilitri : 0;
                    int ammorbidente = m.Value is Lavatrice l2 ? l2.AmmorbidenteMillilitri : 0;
                    tabellaMacchine.AddRow(m.Key, m.Value.GetType().Name, m.Value.NumeroGettoni, m.Value.StatoSportello, m.Value.StatoFunzione,
                        m.Value.ProgrammaSelezionato, m.Value.DurataRimanenteMinutiProgramma, detersivo, ammorbidente);
                }
                tabellaMacchine.Write();

                Console.WriteLine("=========================================================");
                Console.WriteLine("Inserisci un comando con la seguente sintassi:");
                Console.WriteLine("<tipo comando> <numero macchina> <parametro aggiuntivo>");
                Console.WriteLine("Lista comandi:");
                Console.WriteLine("=========================================================");
                Console.WriteLine("apri");
                Console.WriteLine("chiudi");
                Console.WriteLine("gettoni <numero gettoni > 0>");
                Console.WriteLine("lista");
                Console.WriteLine("programma <numero programma>");
                Console.WriteLine("avvia");
                Console.WriteLine("ferma");
                Console.WriteLine("detersivo <quantità detersivo in millilitri>");
                Console.WriteLine("ammorbidente <quantità ammorbidente in millilitri>");
                Console.WriteLine("esci");

                rawInput = Console.ReadLine();
                var input = ParseCommand(rawInput);
                if (Lavanderia.Macchine.TryGetValue(input.macchina, out var macchina) == false)
                {
                    Console.WriteLine("Specifica un numero valido di una macchina!");
                    continue;
                }

                switch (input.Comando.ToLower())
                {
                    case "apri":
                        macchina.ApriSportello();
                        break;
                    case "chiudi":
                        macchina.ChiudiSportello();
                        break;
                    case "gettoni":
                        macchina.InserisciGettoni(input.parametroAggiuntivo);
                        break;
                    case "lista":
                        var programmi = macchina.GetListaProgrammi();
                        var table = new ConsoleTable("Numero", "Nome", "Durata", "Gettoni", "Consumo ammorbidente", "Consumo detersivo");
                        foreach (var p in programmi)
                        {
                            var definizione = Programma.Programmi[p];
                            table.AddRow(definizione.Numero, definizione.Nome, definizione.DurataMinuti, definizione.NumeroGettoni, definizione.ConsumoAmmorbidenteMillilitri, definizione.ConsumoDetersivoMillilitri);
                        }
                        table.Write();
                        break;
                    case "programma":
                        macchina.SelezionaProgramma(input.parametroAggiuntivo);
                        break;
                    case "avvia":
                        macchina.AvviaProgramma();
                        break;
                    case "ferma":
                        macchina.FermaProgramma();
                        break;
                    case "detersivo":
                        {
                            if (input.parametroAggiuntivo < 0)
                                Console.WriteLine("Non puoi sottrarre detersivo!");
                            else if (macchina is Lavatrice l)
                                l.DetersivoMillilitri += input.parametroAggiuntivo;
                            else
                                Console.WriteLine("La macchina selezionata non supporta il comando dato!");
                            break;
                        }
                    case "ammorbidente":
                        {
                            if (input.parametroAggiuntivo < 0)
                                Console.WriteLine("Non puoi sottrarre ammorbidente!");
                            else if (macchina is Lavatrice l)
                                l.AmmorbidenteMillilitri += input.parametroAggiuntivo;
                            else
                                Console.WriteLine("La macchina selezionata non supporta il comando dato!");
                            break;
                        }
                }
                Console.WriteLine();
                Console.WriteLine();

                Thread.Sleep(1000);
                Lavanderia.Simula(1);
            }
        }

        private static (string Comando, int macchina, int parametroAggiuntivo) ParseCommand(string input)
        {
            var split = input.Split(' ');

            var comando = "";
            int macchina = -1;
            int parametroAggiuntivo = -1;
            try
            {
                comando = split[0];
            }
            catch
            {
                Console.WriteLine("Il comando selezionato è vuoto!");
            }
            try
            {
                macchina = int.Parse(split[1]);
            }
            catch
            {
                Console.WriteLine("Parametro macchina non valido!");
            }
            try
            {
                parametroAggiuntivo = int.Parse(split[2]);
            }
            catch
            {

            }

            return (comando, macchina, parametroAggiuntivo);
        }
    }
}