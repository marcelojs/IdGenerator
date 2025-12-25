using System;

namespace TimeEntropy
{
    internal static class Generator
    {
        private static readonly Random _random = new Random();

        public static Guid Id = Guid.NewGuid();

        public static DateTime DataInicio { get; private set; }

        internal static long GerarChave()
        {
            Console.WriteLine($"[TimeEntropy] Gerando chave com Id: {Id}");

            var data3 = DateTime.Now;
            data3 = data3.Date;
            var anoDestino = data3.Year - 1000;

            DataInicio = ValidarDataDeAnoBissexto(data3, anoDestino);

            var timeResult = data3 - DataInicio;

            var fator = _random.NextDouble();
            var data2 = DataInicio.AddTicks((long)(timeResult.Ticks * fator));

            var potencia = _random.Next(5, 10);
            var resultPotencia = (long)Math.Pow(data3.Day, potencia);

            var tickData2Final = data2.Ticks + resultPotencia;
            var data2Final = new DateTime(tickData2Final, DateTimeKind.Local);

            var tickDateInicio = DataInicio.Ticks;
            var tickDataMeio = data2Final.Ticks;
            var tickDataAtual = data3.Ticks;

            if (tickDataMeio < tickDateInicio || tickDataMeio > tickDataAtual)
            {
                Console.WriteLine("Data do meio fora do intervalo, será reajustada por data pivo");
                tickDataMeio = AjusteTickPorDataPivo(data2Final, DataInicio, data3);
            }

            return tickDataMeio;
        }

        private static DateTime ValidarDataDeAnoBissexto(DateTime dataEmAvaliacao, int anoDestino)
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

        private static long AjusteTickPorDataPivo(DateTime dataMeio, DateTime dataInicio, DateTime dataAtual)
        {
            var dataPivo = ValidarDataDeAnoBissexto(dataMeio, DateTime.Now.Year - 500);

            var tickFinal = RefinarParteFinalChave(dataPivo, dataInicio, dataAtual);

            return tickFinal;
        }

        private static long RefinarParteFinalChave(DateTime dataPivo, DateTime dataInicio, DateTime dataAtual)
        {
            long tickFinalRefinado;

            do
            {
                // 1. Pega o tick da data pivô direto
                var tickFinal = dataPivo.Ticks;
                var diaAleatorio = _random.Next(1, 32);
                int potenciaDia = _random.Next(5, 10);

                // 2. Entropia pelo horário (segundos do dia)
                var fatorHorario = 0.0001 + _random.NextDouble() * 0.001;
                long deslocamentoHorario = (long)((dataPivo.Hour * 3600 + dataPivo.Minute * 60 + dataPivo.Second) * fatorHorario);

                // 3. Entropia pelo dia do mês (forte)
                long deslocamentoDia = (long)Math.Pow(diaAleatorio, potenciaDia);

                // 4. Combina os dois deslocamentos
                tickFinalRefinado = tickFinal + deslocamentoHorario + deslocamentoDia;
            }
            while (tickFinalRefinado < dataInicio.Ticks || tickFinalRefinado > dataAtual.Ticks);

            return tickFinalRefinado;
        }
    }
}
