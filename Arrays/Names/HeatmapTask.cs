namespace Names
{
    internal static class HeatmapTask
    {
        public static void LabelTheAxis(int count, int num, string[] labels)
        {
            for (int i = 0; i < count; i++)
            {
                labels[i] = (i + num).ToString(); ;
            }
        }

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            int countOfDays = 30;
            int countOfMonths = 12;
            var daysLabels = new string[countOfDays]; // x-axis 
            var monthsLabels = new string[countOfMonths]; // y-axis 

            LabelTheAxis(countOfDays, 2, daysLabels);
            LabelTheAxis(countOfMonths, 1, monthsLabels);

            double[,] intensityMap = new double[countOfDays, countOfMonths];

            for (int i = 0; i < countOfDays; i++)
            {
                int[] data = new int[12];
                foreach (var individual in names)
                    if (individual.BirthDate.Day == i + 2)
                        data[individual.BirthDate.Month - 1] = data[individual.BirthDate.Month - 1] + 1;

                int j = 0;
                while (j < 12) { intensityMap[i, j] = data[j]; j++; }
            }

            return new HeatmapData("Пример карты интенсивностей",
   intensityMap, daysLabels, monthsLabels);
        }
    }
}
