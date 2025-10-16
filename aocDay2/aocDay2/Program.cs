using System.Linq.Expressions;

namespace aocDay2 {
    internal class Program {
        static void Main(string[] args)
        {
            var reports = new List<List<int>>() {
                { new List<int> { 7, 6, 4, 2, 1 } },
                { new List<int> { 1, 2, 7, 8, 9 } },
                { new List<int> { 9, 7, 6, 2, 1 } },
                { new List<int> { 1, 3, 2, 4, 5 } },
                { new List<int> { 8, 6, 4, 4, 1 } },
                { new List<int> { 1, 3, 6, 7, 9 } }
            };

            PrintSafeReports(reports);
        }

        private static void PrintSafeReports(List<List<int>> reports)
        {
            var safeReports = new Dictionary<int, bool>();

            foreach (var report in reports.Select((values, index) => new { values, index }))
            {
                var areReportValuesDecreasing = false;
                var aux = 0;

                safeReports.Add(report.index, false);

                for (var i = 0; i < report.values.Count; i++) 
                {
                    if (i == 0 && report.values.Count > 1)
                    {
                        areReportValuesDecreasing = report.values[i] > report.values[i + 1];

                        aux = report.values[i];

                        continue;
                    }
                    
                    if (!IsSafe(aux, report.values[i], areReportValuesDecreasing, aux > report.values[i]))
                    {
                        safeReports[report.index] = false;

                        break;
                    }

                    safeReports[report.index] = true;

                    aux = report.values[i];
                }
            }

            Console.WriteLine($"The number of safe reports are {safeReports.Count(sr => sr.Value)}");
        }

        private static bool IsSafe(int value1, int value2, bool areReportValuesDecreasing, bool isCurrentSequenceDecreasing)
        {
            if ((areReportValuesDecreasing && !isCurrentSequenceDecreasing) || (!areReportValuesDecreasing && isCurrentSequenceDecreasing))
            {
                return false;
            }

            if (value1 == value2)
            {
                return false;
            }
            
            if (isCurrentSequenceDecreasing)
            {
                return value1 - value2 >= 1 && value1 - value2 <= 3;
            }

            return value2 - value1 >= 1 && value2 - value1 <= 3;
        }
    }
}
