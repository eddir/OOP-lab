using OOP;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_lab
{
    class Task9 : Task
    {

        public Task9()
        {
            number = 9;
            description = "Описать класс СТУДЕНТ";
        }

        public override void Start()
        {
            bool forceTestArguments = false;
            string fio = "", group = "";
            DateTime birthdate = new DateTime();
            if (!Program.TEST_MODE)
            {
                try
                {
                    Console.WriteLine("Введите ФИО студента:");
                    fio = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("Введите год рождения:");
                        birthdate = DateTime.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Не удалось распознать дату. Будут ипользованы значения по умолчанию.");
                        forceTestArguments = true;
                    }

                    if (!forceTestArguments)
                    {
                        Console.WriteLine("Введите группу студента:");
                        group = Console.ReadLine();

                    }
                }
                catch
                {
                    Console.WriteLine("Не удалось распознать введённые значения. Будут использованы значения по умолчанию.");
                    forceTestArguments = true;
                }
            }

            if (Program.TEST_MODE || forceTestArguments)
            {
                fio = "Александр Дмитриевич Крячков";
                group = "бПИНЖ-61";
                birthdate = new DateTime(1999, 6, 1);
            }

            try
            {
                Student student = new Student(fio, birthdate, group);
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошло ошибка в ходе работе с классом СТУДЕНТ. " + e.Message);
            }
        }

        class Student
        {
            private string fio;

            private DateTime birthdate;

            private string group;

            public Student(string fio, DateTime birthdate, string group)
            {
                SetFio(fio);
                SetBirthdate(birthdate);
                SetGroup(group);
            }

            public void SetFio(string fio)
            {

                fio = fio.Trim();

                if (fio.Length < 1)
                {
                    throw new ArgumentException("ФИО не может быть пустым.");
                }

                if (fio.Split(" ").Length < 2)
                {
                    throw new ArgumentException("ФИО должно содержать имя и фамилию, разделённые пробелом.");
                }

                this.fio = fio;
            }

            public void SetBirthdate(DateTime birthdate)
            {
                if (birthdate.Year < DateTime.Now.Year - 100)
                {
                    throw new ArgumentException("В школу не принимаются люди старше 100 лет.");
                }

                this.birthdate = birthdate;
            }

            public void SetGroup(string group)
            {
                group = group.Trim();

                if (group.Length < 1)
                {
                    throw new ArgumentException("Группа не может быть пустой.");
                }

                this.group = group;
            }

            public string getFio()
            {
                return this.fio;
            }

            public DateTime getBirthdate()
            {
                return this.birthdate;
            }

            public string getGroup()
            {
                return this.group;
            }

            public int DifferenceAge(Student student)
            {
                return this.birthdate.Year - student.birthdate.Year;
            }

            public static List<Student> RemoveStudent(List<Student> students, Student student)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    if (students[i].getFio().Equals(student.getFio())) {
                        students.RemoveAt(i);
                        break;
                    }
                }

                return students;
            }
        }
    }
}
