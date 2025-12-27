using TimeEntropy;

namespace TimeEntropyConsoleTest
{
    //Classe cliente para testes
    internal class Program
    {
        public static void Main(string[] args)
        {
            var teste = TimeEntropyGenerator.GeneratorIdDateFullTickTextId();
            Console.WriteLine($"Chave gerada: {teste}");
        }
    }
}
