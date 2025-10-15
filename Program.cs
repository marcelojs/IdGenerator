namespace id_generator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var data1 = new DateTime(1000, 01, 02);
            var data3 = DateTime.Now;

            var timeResult = data3 - data1;

            var fator = new Random().NextDouble();

            Console.WriteLine($"Fator {fator}");

            var data2 = data1.AddTicks((long)(timeResult.Ticks * fator));
            
            var randPotencia = new Random();

            var potencia = randPotencia.Next(9, 11);

            Console.WriteLine($"Potencia escolhida {potencia}");

            var resultPotencia = (long)Math.Pow(data3.Day, potencia);

            Console.WriteLine($"Resultado da potencia.: {-resultPotencia}");
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
        }
    }
}
