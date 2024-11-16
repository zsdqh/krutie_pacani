using KrytieParni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrytieParni
{
    class Program
    {
        public static void Main(string[] args)
        {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                int n = 10;

                for (int i = n / 2; i <= n; i += 2)
                {
                    for (int j = 1; j < n - i; j += 2)
                    {
                        Console.Write(" ");
                    }

                    for (int j = 1; j <= i; j++)
                    {
                        Console.Write("*");
                    }

                    for (int j = 1; j <= n - i; j++)
                    {
                        Console.Write(" ");
                    }

                    for (int j = 1; j <= i; j++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                }

                for (int i = n; i >= 1; i--)
                {
                    for (int j = i; j < n; j++)
                    {
                        Console.Write(" ");
                    }

                    for (int j = 1; j <= (i * 2) - 1; j++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                }
            
        }

    }
}

