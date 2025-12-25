using TimeEntropy;

namespace TimeEntropyConsoleTest
{
    //Classe cliente para testes
    internal class Program
    {
        public static void Main(string[] args)
        {
            var teste = TimeEntropyGenerator.GeneratorId();
            Console.WriteLine($"Chave gerada: {teste}");
        }
    }
}
