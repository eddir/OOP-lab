using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OOP_lab
{
    /// <summary>
    /// Задача 10 - наследование.
    /// </summary>
    class Task10 : Task
    {

        public Task10()
        {
            number = 'b';
            description = "Описать базовый класс CStr и производный от CStr класс CStr_С .";
        }

        public override void Start()
        {
            CStr_C comment = new CStr_C("//Example");

            while (true)
            {
                Console.WriteLine("Выберите пункт:");
                Console.WriteLine("1. Ввод комментария.");
                Console.WriteLine("2. Перевести комментарий в верхний регистр.");
                Console.WriteLine("3. Перевести комментарий в нижний регистр.");
                Console.WriteLine("4. Поиск символа.");
                Console.WriteLine("5. Сложение.");
                Console.WriteLine("6. Вычитание.");
                Console.WriteLine("7. Просмотреть комментарий.");
                Console.WriteLine("8. Выход.");

                byte option = byte.Parse(Console.ReadKey().KeyChar.ToString());
                Console.WriteLine("\n");

                // Ввод комментария
                if (option == 1)
                {
                    Console.WriteLine("Начните вводите однострочный или многострочный комментарий. Для завершения нажмите дважды enter. Комментарий должен начинаться с управляющего символа.");

                    string text = "";
                    string line;
                    try
                    {
                        do
                        {
                            line = Console.ReadLine();
                            text += line + "\n";

                        } while (line.Trim() != "");

                        comment = new CStr_C(text);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Возникла ошибка ввода. Будет использовано пустое значение.");
                        comment = new CStr_C();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Вы ввели слишком длинное значение. Допускаются 255 символов.");
                    }

                    Console.WriteLine("Комментарий создан.");

                } // Перевести комментарий в верхний регистр
                else if (option == 2)
                {
                    comment.ToUpperCase();
                    comment.Dump();

                } // Перевести комментарий в нижний регистр
                else if (option == 3)
                {
                    comment.ToLowerCase();
                    comment.Dump();

                } // Поиск символа
                else if (option == 4)
                {
                    Console.WriteLine("Введите символ:");

                    short index = comment.IndexOf(Console.ReadKey().KeyChar);

                    if (index == -1)
                    {
                        Console.WriteLine("\nСимвол не найден.\n");
                    }
                    else
                    {
                        Console.WriteLine("\nСимвол найден на позиции {0}.\n", index);
                    }
                } // Сложение
                else if (option == 5)
                {
                    Console.WriteLine("Введите второй комментарий для сложения");

                    comment += CStr_C.FromKeyboard();

                    Console.WriteLine("Результат:\n" + (string) comment);
                } // Вычитание
                else if (option == 6)
                {
                    Console.WriteLine("Введите второй комментарий для вычитания");

                    comment -= CStr_C.FromKeyboard();

                    Console.WriteLine("Результат:\n" + (string) comment);

                } // Просмотреть комментарий
                else if (option == 7)
                {
                    comment.Dump();
                }
                else
                {
                    Console.WriteLine("Выход в главное меню.");
                    return;
                }
                Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// Класс строки
    /// </summary>
    internal class CStr
    {
        /// <summary>
        /// Массив символов
        /// </summary>
        protected char[] chars;

        /// <summary>
        /// Длина строки
        /// </summary>
        protected byte length;

        public char[] Chars
        {
            get
            {
                return this.chars;
            }
            set
            {
                if (value.Length > 255)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.chars = value;
                this.length = (byte)value.Length;
            }
        }

        public byte Length
        {
            get;
        }

        /// <summary>
        /// Конкатенация строк
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static CStr operator +(CStr string1, CStr string2)
        {
            return new CStr(string1 + string2);
        }

        /// <summary>
        /// Сравнение строк как обычные строки логическим оператором ==
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static bool operator ==(CStr string1, CStr string2)
        {
            return string1.Equals(string2);
        }

        /// <summary>
        /// Сравнение строк с отрицанием логичесикм оператором !=
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static bool operator !=(CStr string1, CStr string2)
        {
            return !string1.Equals(string2);
        }

        /// <summary>
        /// Неявный перевод строк в экземпляр класса обычной строки
        /// </summary>
        /// <param name="cstring"></param>
        public static implicit operator string(CStr cstring)
        {
            return cstring.Chars.ToString();
        }

        /// <summary>
        /// Сравнение данного экземпляра строки с экземпляром обычной строки
        /// </summary>
        /// <param name="string1"></param>
        /// <returns></returns>
        public override bool Equals(Object string1)
        {
            return (string)this == (string)string1;
        }

        /// <summary>
        /// Сравнение двух строк между собой
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public new bool Equals(Object string1, Object string2)
        {
            return (string)string1 == (string)string2;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Конструктрутор пустой строки
        /// </summary>
        public CStr()
        {
            this.Chars = new char[] { };
        }

        /// <summary>
        /// Конструктор из обычной строки
        /// </summary>
        /// <param name="symbols"></param>
        public CStr(string symbols)
        {

            this.Chars = symbols.ToCharArray();
        }

        /// <summary>
        /// Конструктор из одного символа
        /// </summary>
        /// <param name="symbol"></param>
        public CStr(char symbol)
        {
            this.Chars = new char[] { symbol };
        }

        /// <summary>
        /// Очистка строки
        /// </summary>
        public void Clear()
        {
            this.Chars = new char[] { };
        }
    }

    /// <summary>
    /// Класс комментария с предком строки. Реализует однострочный и многострочный комментарии.
    /// </summary>
    class CStr_C : CStr
    {
        /// <summary>
        /// Пустая строка, не комментарий
        /// </summary>
        const byte EMPTY = 0;

        /// <summary>
        /// Комментарий в одну строчку с двумя слешами в начале
        /// </summary>
        const byte ONELINE = 1;

        /// <summary>
        /// Многострочный комментарий, начинающийся на /* и окончивающийся на */
        /// </summary>
        const byte MULTILINE = 2;

        /// <summary>
        /// Тип комментария. Допускается EMPTY, ONELINE и MULTILINE
        /// </summary>
        private byte type;

        /// <summary>
        /// Свойство для работы с массивом символов комментария
        /// </summary>
        public new char[] Chars
        {
            get
            {
                return base.Chars;
            }
            set
            {
                //TODO: исключить управляющие символы из массива символов chars
                base.Chars = value;

                if (this.length > 2 && value[0] == '/' && value[1] == '/')
                {
                    this.type = ONELINE;
                }
                else if (this.length > 4 && value[0] == '/' || value[1] == '*' && value[this.length - 2] == '*' && value[this.length - 1] == '/')
                {
                    this.type = MULTILINE;
                }
                else
                {
                    this.type = EMPTY;
                    this.length = 0;
                    this.chars = new char[] { };
                }
            }
        }

        /// <summary>
        /// Оператор объединения комментариев.
        /// </summary>
        /// <param name="comment1"></param>
        /// <param name="comment2"></param>
        /// <returns>Многострочный комментарий</returns>
        public static CStr_C operator +(CStr_C comment1, CStr_C comment2)
        {
            return new CStr_C("/*" + (string) comment1 + (string) comment2 + "*/");
        }

        /// <summary>
        /// Оператор вычитания из одного комментария символы другого комментария. 
        /// </summary>
        /// <param name="comment1">Первый комментарий</param>
        /// <param name="comment2">Второй комментарий</param>
        /// <returns>Объединённый комментарий, тип которого многострочный, если один из комментариев многострочный, или однострочный в противном случае.</returns>
        public static CStr_C operator -(CStr_C comment1, CStr_C comment2)
        {
            char[] symbols = new char[comment1.length];
            int size = 0;

            foreach (char symbol1 in (string)comment1)
            {
                bool add = true;

                foreach (char symbol2 in (string)comment2)
                {
                    if (symbol1 == symbol2)
                    {
                        add = false;
                        break;
                    }
                }

                if (add)
                {
                    symbols[size++] = symbol1;
                }
            }

            string body;

            if (comment1.type == MULTILINE || comment2.type == MULTILINE)
            {
                body = "/*" + new string(symbols) + "*/";
            }
            else
            {
                body = "//" + new string(symbols);
            }

            return new CStr_C(body);
        }

        /// <summary>
        /// Оператор перевода комментария в строку. При этом удаляются управляющие символы "//" и "/* */".
        /// </summary>
        /// <param name="comment"></param>
        public static explicit operator string(CStr_C comment)
        {

            if (comment.type == ONELINE)
            {
                return (new string(comment.chars)).Substring(2);
            }
            else if (comment.type == MULTILINE)
            {
                return (new string(comment.chars)).Substring(2, comment.length - 4);
            }

            return "";
        }

        /// <summary>
        /// Конструктор пустого комментария
        /// </summary>
        public CStr_C()
        {
            this.Chars = new char[] { };
        }

        /// <summary>
        /// Конструтор комментария из массива символов.
        /// </summary>
        /// <param name="symbols"></param>
        public CStr_C(char[] symbols)
        {
            this.Chars = symbols;
        }

        /// <summary>
        /// Конструктор комментария из строки.
        /// </summary>
        /// <param name="symbols"></param>
        public CStr_C(string symbols)
        {
            this.Chars = symbols.Trim().ToCharArray();
        }

        /// <summary>
        /// перевод тела комментария в верхний регистр.
        /// </summary>
        public void ToUpperCase()
        {
            for (short i = 0; i < this.length; i++)
            {
                this.chars[i] = char.ToUpper(this.chars[i]);
            }
        }

        /// <summary>
        /// Перевод тела комментария в нижний регистр.
        /// </summary>
        public void ToLowerCase()
        {
            for (short i = 0; i < this.length; i++)
            {
                this.chars[i] = char.ToLower(this.chars[i]);
            }
        }

        /// <summary>
        /// Поиск первого вхождения символа в текущем комментарии.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public short IndexOf(char symbol)
        {
            for (short i = 0; i < this.length; i++)
            {
                if (this.chars[i] == symbol)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Ввод комментария из консоли
        /// </summary>
        /// <returns>Экземпляр класса комментария введённого пользователем</returns>
        /// <exception cref="ArgumentOutOfRangeException">Введён слишком длинный комментарий</exception>
        public static CStr_C FromKeyboard()
        {
            string text = "";
            string line;
            try
            {
                do
                {
                    line = Console.ReadLine();
                    text += line;

                } while (line.Trim() != "");

                return new CStr_C(text);
            }
            catch (IOException)
            {
                Console.WriteLine("Возникла ошибка ввода. Будет использовано пустое значение.");
                return new CStr_C();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Вы ввели слишком длинное значение. Допускаются 255 символов.");
            }
        }

        public void Dump()
        {
            Console.WriteLine((string)this);
        }
    }
}
