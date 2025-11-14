using System.Numerics;

namespace id_generator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var generator = new Generator();

            generator.GerarChave();
            var tick2 = generator.TickDataMeio;
            var result2 = RefinarParteFinalChave2(tick2);
            Console.WriteLine(result2);
        }

        public static long RefinarParteFinalChave2(long tickFinal)
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

    public class Generator
    {
        public long TickDateInicio { get; private set; }
        public long TickDataMeio { get; private set; }
        public long TickDataAtual { get; private set; }
        public string? TickFinalProcessado { get; private set; }
        public DateTime DataInicio { get; private set; }

        public void GerarChave()
        {
            var data3 = DateTime.Now;
            data3 = data3.Date;
            var anoDestino = data3.Year - 1000;
            var isBisexto = DateTime.IsLeapYear(anoDestino);

            // Verifica se a data original é 29/02
            if (data3.Month == 2 && data3.Day == 29)
            {
                // Se o ano destino for bissexto, mantém 29/02
                if (isBisexto)
                    DataInicio = new DateTime(anoDestino, 2, 29);
                else
                    DataInicio = new DateTime(anoDestino, 2, 28); // fallback seguro
            }
            else
                DataInicio = new DateTime(anoDestino, data3.Month, data3.Day);

            var timeResult = data3 - DataInicio;

            var fator = new Random().NextDouble();
            var data2 = DataInicio.AddTicks((long)(timeResult.Ticks * fator));

            var randPotencia = new Random();

            var potencia = randPotencia.Next(5, 10);
            var resultPotencia = (long)Math.Pow(data3.Day, potencia);

            var tickData2Final = data2.Ticks + resultPotencia;
            var data2Final = new DateTime(tickData2Final, DateTimeKind.Local);

            TickDateInicio = DataInicio.Ticks;
            TickDataMeio = data2Final.Ticks;
            TickDataAtual = data3.Ticks;
        }

        public static long AjusteTickPorDataPivo(DateTime dataMeio)
        {
            long tick = 0;
            var dataPivo = DateTime.Now;
            var anoDestino = dataPivo.Year - 500;
            var isBisexto = DateTime.IsLeapYear(anoDestino);

            if (dataPivo.Month == 2 && dataPivo.Day == 29)
            {
                // Se o ano destino for bissexto, mantém 29/02
                if (isBisexto)
                    dataPivo = new DateTime(anoDestino, 2, 29);
                else
                    dataPivo = new DateTime(anoDestino, 2, 28); 
            }
            else
                dataPivo = new DateTime(anoDestino, dataPivo.Month, dataPivo.Day);



            return tick;
        }
        
        
    }

    public class InstanceManager
    {
        

    }
}
