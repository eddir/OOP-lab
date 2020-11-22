using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_lab
{
    class Task8: Task
    {

        public Task8()
        {
            number = '8';
            description = "Строки (поиск вхождений в словах).";
        }

        public override void Start()
        {
            bool forceTestArgs = false;
            string text = "", word = "";

            Console.WriteLine("Даны текстовая строка и слово (например, ab). Напечатать все слова, входящие в эту текстовую строку, " +
                "заканчивающиеся на буквы задан-ного слова (например, abcdab, ab, kab), используя методы класса String или StringBuilder.\n");

            if (!Program.TEST_MODE)
            {
                try
                {
                    Console.WriteLine("Введите строку:");
                    text = Console.ReadLine();

                    Console.WriteLine("Введите слово:");
                    word = Console.ReadLine();
                } catch
                {
                    Console.WriteLine("Не получилось считать введённые данные. Будут использованы значения по умолчанию.");
                    forceTestArgs = true;
                }
            }
            
            if (Program.TEST_MODE || forceTestArgs) {
                text = "ab acb cab bbbab ababab abbbbb bcab";
                word = "ab";
            }

            try
            {
                List<String> results = SearchWordsEndsWith(text, word);

                Console.WriteLine("Найдены слова:");

                for (int i = 0; i < results.Count; i++)
                {
                    Console.Write(results[i]);

                    if (i < results.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("Не удалось выполнить поиск. " + e.Message);
            }
        }

        public static List<String> SearchWordsEndsWith(string haystack, string needle)
        {
            if (haystack.Length < 1 || needle.Length < 1)
            {
                throw new ArgumentException("Строка и слова не могут быть пустыми.");
            }

            string[] words = haystack.Split(" ");
            List<String> results = new List<String>();

            foreach (String word in words)
            {
                if (word.EndsWith(needle))
                {
                    results.Add(word);
                }
            }
            return results;
        }

    }
}
