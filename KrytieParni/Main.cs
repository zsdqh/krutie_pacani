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
            Team team1 = new Team("Исследовательская группа", 1);
            Team team2 = new Team("Исследовательская группа", 1);
            Console.WriteLine($"Ссылки на объекты team1 и team2 равны: {ReferenceEquals(team1, team2)}");
            Console.WriteLine($"Объекты team1 и team2 равны: {team1.Equals(team2)}");
            Console.WriteLine($"Хэш-коды team1 и team2: {team1.GetHashCode()}, {team2.GetHashCode()}");
            try
            {
                team1.Id = -1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            ResearchTeam researchTeam = new ResearchTeam("Исследование вирусов", "Научный институт", 663, TimeFrame.TwoYears);
            researchTeam.AddPapers(new List<Paper>
                {
                    new Paper("Анализ вируса", new Person("Вася", "Тупов", new DateTime(1985, 6, 15)), new DateTime(2022, 10, 5)),
                    new Paper("Методы лечения", new Person("Юля", "Интелектова", new DateTime(1990, 3, 22)), new DateTime(2023, 2, 18))
                });
            researchTeam.AddMembers(new List<Person>
                {
                    new Person("Иван", "Сидоров", new DateTime(1988, 11, 30)),
                    new Person("Елена", "Васильева", new DateTime(1982, 4, 8))
                });
            Console.WriteLine("\nДанные объекта ResearchTeam:");
            Console.WriteLine(researchTeam.ToString());

            Console.WriteLine("\nЗначение свойства Team для объекта ResearchTeam:");
            Console.WriteLine(researchTeam.GetBaseTeam.ToString());

            ResearchTeam deepCopyTeam = (ResearchTeam)researchTeam.DeepCopy();
            Console.WriteLine("\nСоздана глубокая копия объекта ResearchTeam:");
            Console.WriteLine(deepCopyTeam.ToString());

            researchTeam.Research = "Новое исследование вирусов";
            researchTeam.AddPapers(new List<Paper>
                {
                    new Paper("Новые методы лечения", new Person("Ольга", "Козлова", new DateTime(1985, 6, 15)), new DateTime(2023, 3, 10))
                });
            Console.WriteLine("\nИзмененный исходный объект ResearchTeam:");
            Console.WriteLine(researchTeam.ToString());
            Console.WriteLine("\nГлубокая копия объекта ResearchTeam (неизмененный):");
            Console.WriteLine(deepCopyTeam.ToString());
            Console.WriteLine("\nСписок участников проекта, которые не имеют публикаций:");
            foreach (Person person in researchTeam.PersonsWithoutPublications())
            {
                Console.WriteLine(person);
            }
            Console.WriteLine("\nСписок всех публикаций, вышедших за последние два года:");
            foreach (Paper paper in researchTeam.PapersLessThanN(2))
            {
                Console.WriteLine(paper);
            }
            Console.WriteLine("\nСписок участников проекта, у которых есть публикации:");
            foreach (Person person in researchTeam)
            {
                Console.WriteLine(person);
            }
            Console.WriteLine("\nСписок участников проекта, имеющих более одной публикации:");
            foreach (Person person in researchTeam.PersonsWithMany())
            {
                Console.WriteLine(person);
            }
            Console.WriteLine("\nСписок публикаций, вышедших за последний год:");
            foreach (Paper paper in researchTeam.LastYear())
            {
                Console.WriteLine(paper);
            }


            int size = 10000;
            Paper[] oneDimensionalArray = new Paper[size];
            Paper[,] twoDimensionalRectangularArray = new Paper[100, 100];
            Paper[][] twoDimensionalJaggedArray = new Paper[100][];
            for (int i = 0; i < 100; i++)
            {
                twoDimensionalJaggedArray[i] = new Paper[100];
            }

            for (int i = 0; i < size; i++)
            {
                Paper paper = new Paper($"Публикация {i}", new Person("Автор", "Фамилия", new DateTime(1990, 1, 1)), new DateTime(2022, 5, 6));
                oneDimensionalArray[i] = paper;
                twoDimensionalRectangularArray[i / 100, i % 100] = paper;
                twoDimensionalJaggedArray[i / 100][i % 100] = paper;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                var temp = oneDimensionalArray[i];
            }
            watch.Stop();
            var oneDimensionalTime = watch.ElapsedTicks;
            Console.WriteLine($"\nВремя доступа к элементам одномерного массива: {oneDimensionalTime} мс");

            watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                var temp = twoDimensionalRectangularArray[i / 100, i % 100];
            }
            watch.Stop();
            var twoDimensionalRectangularTime = watch.ElapsedTicks;
            Console.WriteLine($"Время доступа к элементам двумерного прямоугольного массива: {twoDimensionalRectangularTime} мс");

            watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                var temp = twoDimensionalJaggedArray[i / 100][i % 100];
            }
            watch.Stop();
            var twoDimensionalJaggedTime = watch.ElapsedTicks;
            Console.WriteLine($"Время доступа к элементам двумерного ступенчатого массива: {twoDimensionalJaggedTime} мс");
        }
    }
}