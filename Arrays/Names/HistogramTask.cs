namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            int numberOfDays = 31;
            var daysLabel = new string[numberOfDays]; // x-axis
            for (int i = 0; i < numberOfDays; i++)
            {
                daysLabel[i] = (i + 1).ToString();
            }

            var birthsCount = new double[numberOfDays]; // y-axis
            foreach (var individual in names)
            {
                if (individual.Name == name && individual.BirthDate.Day != 1)
                    birthsCount[individual.BirthDate.Day - 1]++;
            }

            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name),
         daysLabel, birthsCount);
        }
    }
}