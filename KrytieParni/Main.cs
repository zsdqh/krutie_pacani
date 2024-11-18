using KrytieParni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Создать один объект типа ResearchTeam, преобразовать данные в текстовый вид с помощью метода ToShortString() и вывести данные.
            ResearchTeam researchTeam = new ResearchTeam("Исследование вирусов", "Научный институт", 663, TimeFrame.TwoYears);
            Console.WriteLine("Краткая информация о группе исследований:");
            Console.WriteLine(researchTeam.ToShortString());

            // 2. Вывести значения индексатора для значений индекса TimeFrame.Year, TimeFrame.TwoYears, TimeFrame.Long.
            Console.WriteLine("\nПроверка индексатора:");
            Console.WriteLine($"Год: {researchTeam[TimeFrame.Year]}");
            Console.WriteLine($"Два года: {researchTeam[TimeFrame.TwoYears]}");
            Console.WriteLine($"Долгосрочный: {researchTeam[TimeFrame.Long]}");

            // 3. Присвоить значения всем определенным в типе ResearchTeam свойствам, преобразовать данные в текстовый вид с помощью метода ToString() и вывести данные.
            Paper[] papers = new Paper[]
            {
                new Paper("Анализ вируса", new Person("Вася", "Тупов", new DateTime(1985, 6, 15)), new DateTime(2022, 10, 5)),
                new Paper("Методы лечения", new Person("Юля", "Интелектова", new DateTime(1990, 3, 22)), new DateTime(2023, 2, 18))
            };
            researchTeam.Publications = papers;
            Console.WriteLine("\nПолная информация о группе исследований:");
            Console.WriteLine(researchTeam.ToString());

            // 4. С помощью метода AddPapers (params Paper []) добавить элементы в список публикаций и вывести данные объекта ResearchTeam.
            researchTeam.AddPapers(
                new Paper("Иммунная реакция", new Person("Иван", "Сидоров", new DateTime(1988, 11, 30)), new DateTime(2023, 6, 10)),
                new Paper("Вакцинация", new Person("Елена", "Васильева", new DateTime(1982, 4, 8)), new DateTime(2023, 1, 12))
            );
            Console.WriteLine("\nОбновленный список публикаций:");
            Console.WriteLine(researchTeam.ToString());

            // 5. Вывести значение свойства, которое возвращает ссылку на публикацию с самой поздней датой выхода.
            Console.WriteLine("\nПубликация с самой поздней датой выхода:");
            Console.WriteLine(researchTeam.LatestPaper.ToString());

            // 6. Сравнить время выполнения операций с элементами одномерного, двумерного прямоугольного и двумерного ступенчатого массивов с одинаковым числом элементов типа Paper.
            int size = 10000;
            Paper[] oneDimensionalArray = new Paper[size];
            Paper[,] twoDimensionalRectangularArray = new Paper[100, 100];
            Paper[][] twoDimensionalJaggedArray = new Paper[100][];
            for (int i = 0; i < 100; i++)
            {
                twoDimensionalJaggedArray[i] = new Paper[100];
            }

            // Заполнение массивов для тестирования
            for (int i = 0; i < size; i++)
            {
                Paper paper = new Paper($"Публикация {i}", new Person("Автор", "Фамилия", new DateTime(1990, 1, 1)), new DateTime(2022,5,6));
                oneDimensionalArray[i] = paper;
                twoDimensionalRectangularArray[i / 100, i % 100] = paper;
                twoDimensionalJaggedArray[i / 100][i % 100] = paper;
            }

            // Измерение времени доступа к элементам одномерного массива
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                var temp = oneDimensionalArray[i];
            }
            watch.Stop();
            var oneDimensionalTime = watch.ElapsedMilliseconds;
            Console.WriteLine($"\nВремя доступа к элементам одномерного массива: {oneDimensionalTime} мс");

            // Измерение времени доступа к элементам двумерного прямоугольного массива
            watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                var temp = twoDimensionalRectangularArray[i / 100, i % 100];
            }
            watch.Stop();
            var twoDimensionalRectangularTime = watch.ElapsedMilliseconds;
            Console.WriteLine($"Время доступа к элементам двумерного прямоугольного массива: {twoDimensionalRectangularTime} мс");

            // Измерение времени доступа к элементам двумерного ступенчатого массива
            watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                var temp = twoDimensionalJaggedArray[i / 100][i % 100];
            }
            watch.Stop();
            var twoDimensionalJaggedTime = watch.ElapsedMilliseconds;
            Console.WriteLine($"Время доступа к элементам двумерного ступенчатого массива: {twoDimensionalJaggedTime} мс");
        }
    }
}
