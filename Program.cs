namespace id_generator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var generator = new Generator();

            generator.GerarChave();
            
        }


    }

    public class Generator
    {
        public long TickDateInicio { get; private set; }
        public long TickDataMeio { get; private set; }
        public long TickDataAtual { get; private set; }

        public string? TickFinalProcessado { get; private set; }

        public void GerarChave()
        {
            var data3 = DateTime.Now;
            data3 = data3.Date;
            var anoDestino = data3.Year - 1000;
            var isBisexto = DateTime.IsLeapYear(anoDestino);

            DateTime data1;

            // Verifica se a data original é 29/02
            if (data3.Month == 2 && data3.Day == 29)
            {
                // Se o ano destino for bissexto, mantém 29/02
                if (isBisexto)
                    data1 = new DateTime(anoDestino, 2, 29);
                else
                    data1 = new DateTime(anoDestino, 2, 28); // fallback seguro
            }
            else
            {
                // Mantém dia e mês original
                data1 = new DateTime(anoDestino, data3.Month, data3.Day);
            }

            var timeResult = data3 - data1;

            var fator = new Random().NextDouble();

            Console.WriteLine($"Fator {fator}");

            var data2 = data1.AddTicks((long)(timeResult.Ticks * fator));

            var randPotencia = new Random();

            var potencia = randPotencia.Next(5, 11);

            Console.WriteLine($"Potencia escolhida {potencia}");

            var resultPotencia = (long)Math.Pow(data3.Day, potencia);

            Console.WriteLine($"Resultado da potencia.: {resultPotencia}");
            Console.WriteLine($"Resultado do dia {data3.Day} de potencia {potencia} .: {resultPotencia}");
            Console.WriteLine("Data 1 Ticks.: " + data1.Ticks);
            Console.WriteLine("Data 1.: " + data1.ToString("dd/MM/yyyy"));
            Console.WriteLine("Data 3 Ticks.: " + data3.Ticks);
            Console.WriteLine("Data 3.: " + data3.ToString("dd/MM/yyyy"));
            Console.WriteLine("Data 2 resultado do calculo entre data1 e data 3");
            Console.WriteLine("Data 2 Ticks.: " + data2.Ticks);
            Console.WriteLine("Data 2.: " + data2.ToString("dd/MM/yyyy"));

            var tickData2Final = data2.Ticks + resultPotencia;
            var data2Final = new DateTime(tickData2Final, DateTimeKind.Local);

            Console.WriteLine("Data 2 Final Ticks .: " + data2Final.Ticks);
            Console.WriteLine("Data 2 Final .: " + data2Final.ToString("dd/MM/yyyy hh:mm:ss"));

            Console.WriteLine($"Composição das chaves.: {data1.Ticks} - {data2Final.Ticks} - {data3.Ticks}");

            TickFinalProcessado = $"{data1.Ticks}{data2.Ticks}{data3.Ticks}";
            Console.WriteLine($"Chave final.: {TickFinalProcessado}");
            Console.WriteLine($"Tamanhdo da chave {TickFinalProcessado.Length}");

            TickDateInicio = data1.Ticks;
            TickDataMeio = data2Final.Ticks;
            TickDataAtual = data3.Ticks;
        }


    }

    public class InstanceManager
    {
        

    }
}
