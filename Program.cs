namespace id_generator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var generator = new Generator();

            generator.GerarChave();
            RefinarParteFinalChave(generator.TickDataMeio);

        }

        public static long RefinarParteFinalChave(long tickFinal)
        {
            long tickRefinado = 0;

            /*
            double fator1 = rand.NextDouble() + 1.0;
            double fator2 = rand.NextDouble() + 1.0;
            double baseComFatores = tickBase * fator1 * fator2;
            double curvado = Math.Pow(baseComFatores, 1.01);
            long final = (long)Math.Round(curvado);
            */

            var randFator = new Random();

            var fator1 = randFator.NextDouble() + 1.0;
            var fator2 = randFator.NextDouble() + 1.0;

            var baseResultado = tickFinal * fator1 * fator2;

            var curvatura = Math.Pow(baseResultado, 1.01);

            tickRefinado = (long)Math.Round(curvatura);
                
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

            Console.WriteLine($"Fator {fator}");

            var data2 = DataInicio.AddTicks((long)(timeResult.Ticks * fator));

            var randPotencia = new Random();

            var potencia = randPotencia.Next(5, 11);

            Console.WriteLine($"Potencia escolhida {potencia}");

            var resultPotencia = (long)Math.Pow(data3.Day, potencia);

            Console.WriteLine($"Resultado da potencia.: {resultPotencia}");
            Console.WriteLine($"Resultado do dia {data3.Day} de potencia {potencia} .: {resultPotencia}");
            Console.WriteLine("Data 1 Ticks.: " + DataInicio.Ticks);
            Console.WriteLine("Data 1.: " + DataInicio.ToString("dd/MM/yyyy"));
            Console.WriteLine("Data 3 Ticks.: " + data3.Ticks);
            Console.WriteLine("Data 3.: " + data3.ToString("dd/MM/yyyy"));
            Console.WriteLine("Data 2 resultado do calculo entre data1 e data 3");
            Console.WriteLine("Data 2 Ticks.: " + data2.Ticks);
            Console.WriteLine("Data 2.: " + data2.ToString("dd/MM/yyyy"));

            var tickData2Final = data2.Ticks + resultPotencia;
            var data2Final = new DateTime(tickData2Final, DateTimeKind.Local);

            Console.WriteLine("Data 2 Final Ticks .: " + data2Final.Ticks);
            Console.WriteLine("Data 2 Final .: " + data2Final.ToString("dd/MM/yyyy hh:mm:ss"));

            Console.WriteLine($"Composição das chaves.: {DataInicio.Ticks} - {data2Final.Ticks} - {data3.Ticks}");

            TickFinalProcessado = $"{DataInicio.Ticks}{data2.Ticks}{data3.Ticks}";
            Console.WriteLine($"Chave final.: {TickFinalProcessado}");
            Console.WriteLine($"Tamanhdo da chave {TickFinalProcessado.Length}");

            TickDateInicio = DataInicio.Ticks;
            TickDataMeio = data2Final.Ticks;
            TickDataAtual = data3.Ticks;
        }
    }

    public class InstanceManager
    {
        

    }
}
