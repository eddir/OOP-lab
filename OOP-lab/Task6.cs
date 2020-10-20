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

                int size = 6;
                byte[] array = Program.RandomArray(size);
                bool increases = true;
                byte previous = array[0];

                foreach (byte item in array)
                {
                    if (item < previous)
                    {
                        increases = false;
                    }
                }

                Program.printArray(array);

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
