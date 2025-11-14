using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace id_generator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var generator = new Generator();
            var resultFinal = generator.GerarChave();
        }
    }

    public class Generator
    {
        public long TickDateInicio { get; private set; }
        public long TickDataMeio { get; private set; }
        public long TickDataAtual { get; private set; }
        public string? TickFinalProcessado { get; private set; }
        public DateTime DataInicio { get; private set; }

        public long GerarChave()
        {
            var data3 = DateTime.Now;
            data3 = data3.Date;
            var anoDestino = data3.Year - 1000;

            DataInicio = ValidarDataDeAnoBissexto(data3, anoDestino);

            var timeResult = data3 - DataInicio;

            var apoiadorRandomico = new Random();

            var fator = apoiadorRandomico.NextDouble();
            var data2 = DataInicio.AddTicks((long)(timeResult.Ticks * fator));

            var potencia = apoiadorRandomico.Next(5, 10);
            var resultPotencia = (long)Math.Pow(data3.Day, potencia);

            var tickData2Final = data2.Ticks + resultPotencia;
            var data2Final = new DateTime(tickData2Final, DateTimeKind.Local);

            TickDateInicio = DataInicio.Ticks;
            TickDataMeio = data2Final.Ticks;
            TickDataAtual = data3.Ticks;

            if (TickDataMeio < TickDateInicio || TickDataMeio > TickDataAtual)
            {
                Console.WriteLine("Data do meio fora do intervalo, será reajustada por data pivo");
                TickDataMeio = AjusteTickPorDataPivo(data2Final);
            }

            return TickDataMeio;
        }

        public DateTime ValidarDataDeAnoBissexto(DateTime dataEmAvaliacao, int anoDestino)
        {
            var isBisexto = DateTime.IsLeapYear(anoDestino);

            if (dataEmAvaliacao.Month == 2 && dataEmAvaliacao.Day == 29)
            {
                // Se o ano destino for bissexto, mantém 29/02
                if (isBisexto)
                    DataInicio = new DateTime(anoDestino, 2, 29);
                else
                    DataInicio = new DateTime(anoDestino, 2, 28); // fallback seguro
            }
            else
                DataInicio = new DateTime(anoDestino, dataEmAvaliacao.Month, dataEmAvaliacao.Day);

            return DataInicio;
        }

        public long AjusteTickPorDataPivo(DateTime dataMeio)
        {
            var dataPivo = DateTime.Now;
            var anoDestino = dataPivo.Year - 500;

            dataPivo = ValidarDataDeAnoBissexto(dataMeio, anoDestino);

            var tickFinal = RefinarParteFinalChave2(dataPivo.Ticks); 

            return tickFinal;
        }

        public long RefinarParteFinalChave2(long tickFinal)
        {
            var randFator = new Random();

            var fator1 = 1.01 + randFator.NextDouble() * 0.08;

            var curvatura = Math.Pow(tickFinal * fator1, 1.002);

            var tickRefinado = (long)Math.Round(curvatura);

            if (tickRefinado < 100_000_000_000_000_000)
                tickRefinado += 100_000_000_000_000_000;
            else if (tickRefinado > 999_999_999_999_999_999)
            {
                // Normaliza para manter 18 dígitos
                tickRefinado = tickRefinado % 1_000_000_000_000_000_000;
                if (tickRefinado < 100_000_000_000_000_000)
                    tickRefinado += 100_000_000_000_000_000;
            }

            return tickRefinado;
        }

    }

    public class InstanceManager
    {
        

    }
}
