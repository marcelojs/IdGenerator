namespace TimeEntropy
{
    public static class TimeEntropyGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long GeneratorIdDateTickId()
        {
            var generator = new Generator();
            var tick = generator.GerarTick().ToString();
            var date = generator.DataInicio;
            return long.Parse($"{date.Year}{date.Month:D2}{date.Day:D2}{tick.Substring(tick.Length - 10)}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long GeneratorIdFullTickId()
        {
            var generator = new Generator();
            return generator.GerarTick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GeneratorIdDateFullTickTextId()
        {
            var generator = new Generator();
            var tickTemporaty = generator.GerarTick();
            var date = generator.DataInicio;
            return $"{date.Year}{date.Month:D2}{date.Day:D2}-{tickTemporaty}";
        }
    }
}
