using OOP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OOP_lab
{
    class Task6
    {
        public static void Start()
        {
            try
            {
                Console.WriteLine("Для целочисленного массива, сгенерированного случайным образом, определить, " +
                    "образуют ли его элементы  неубывающую (>=) последовательность.\n");

                Random rnd = new Random();
                byte size = 5;
                byte[] array = new byte[size];

                for (byte i = 0; i < size; i++)
                {
                    array[i] = (byte)(rnd.Next() % 100);
                }
                bool increases = true;
                byte previous = array[0];

                foreach (byte item in array)
                {
                    if (item < previous)
                    {
                        increases = false;
                    }
                }

                for (byte i = 0; i < size; i++)
                {
                    Console.Write("{0}", array[i]);
                    if (i < size - 1)
                    {
                        Console.Write(", ");
                    }
                }

                if (increases)
                {
                    Console.WriteLine("\n\nПоследовтельность неубывающая");
                }
                else
                {

                    Console.WriteLine("\n\nПоследовтельность не является неубывающей");
                }
            } catch (Exception e)
            {
                Program.WriteError("Не удалось проанализироват массив.", e);
            }
        }
    }
}
