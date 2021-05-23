using System;using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите сведения о работнике (ФИО, № кабинета, рабочие дни недели, рабочее время, время перерыва):");
        //string input = Console.ReadLine();
        string input = "Иванов Иван Иванович, кабинет 97, понедельник-Четверг, 06:00-18:00,13:00-14:00";
        //string input = " 107каб, алла, 00:00-18:00, 13:00-14:00, ПН-ср";
        Console.WriteLine(Parse.ParseData(input));
        Console.ReadKey();
    }
}

class ParseTime
{
    // Выделить из строки в числа
    public static int Minimal(string chars)
    {
        string digits = Regex.Replace(chars, @"[^0-9]+", "");
        if(int.TryParse(digits, out int result))
            Console.WriteLine();
        return result; 
    }
}

class Gen
{
    //Преобразует строку. Ищет обозначение дней недели.
    public static string WorkDays(string value)
    {
        value = value.ToLower();

        string myvalue = "";
        switch (value)
        {
            case "пн-вт":
                return "Понедельник-Вторник";
            case "понедельник-вторник":
                return value;

            case "пн-ср":
                return "Понедельник-Среда";
            case "понедельник-среда":
                return value;

            case "пн-чт":
                return "Понедельник-Четверг";
            case "понедельник-четверг":
                return value;

            case "пн-пт":
                return "Понедельник-Пятница";
                            case "понедельник-пятница":
                return value;

            case "пн":
                return "Понедельник";
            case "вт":
                return "Вторник";
            case "ср":
                return "Среда";
            case "чт":
                return "Четверг";
            case "пт":
                return "Пятница";
        }
        return myvalue;
    }
}
class Parse
{
    //Разбивает входную сторку для занесения в графы журнала.
    public static string ParseData(string source)
    {
        //your code
        Console.WriteLine("Вы ввели:" +"\n" + "\"" + source + "\"");
        string[] mystring = source.Split(',');
        Regex letterCase = new Regex("[а-яА-Яa-zA-Z]");
        Regex digitCase = new Regex("[0-9]");
        Regex signCase = new Regex("[-]");
        Regex tokenCase = new Regex("[:]");

        string name = "";
        string cab = "";
        string days = "";
        string time1 = "";
        string time2 = "";
        int i = 0; // Счетчик для tokens
        foreach (var msr in mystring)
        {
            string ms = msr.Trim();
            // Поймать фамилию работника
            if (letterCase.IsMatch(ms) && !digitCase.IsMatch(ms) && !signCase.IsMatch(ms) && ms.Length > 3)
               name = ms;
            // Поймать номер кабинета
            if (digitCase.IsMatch(ms) && !signCase.IsMatch(ms) && !tokenCase.IsMatch(ms))
                cab = Regex.Replace(ms, @"[^0-9]+", "");
            // Поймать рабочие дни
            if (letterCase.IsMatch(ms) && signCase.IsMatch(ms) || ms.Length ==2)
                days = Gen.WorkDays(ms);

            // Поймать рабочее время
            if (tokenCase.IsMatch(ms) && !letterCase.IsMatch(ms) && i == 0)
            {
                time1 = ms;   
                i ++;
            }

            // Поймать перерыв на обед
            else if (tokenCase.IsMatch(ms) && !letterCase.IsMatch(ms) && i != 0)
                time2 = ms;
        }

        // Поменять местами времена, если перерыв указан раньше
        if (ParseTime.Minimal(time1) > ParseTime.Minimal(time2))
        {
            string vol = time1;
            time1 = time2;
            time2 = vol;
        }

        if (name.Length < 4) name = "Не указано";
        if (cab.Length < 1) cab = "Не указано";
        if (days.Length < 2) days = "Не указано";
        if (time1.Length < 6) time1 = "Не указано";
        if (time2.Length < 6) time2 = "Не указано";

        Console.WriteLine("Работник: " + name);
        Console.WriteLine("Кабинет: " + cab);
        Console.WriteLine("Рабочие дни: " + days);
        Console.WriteLine("Время работы: "    + time1);
        Console.WriteLine("Обеденное время: " + time2);

        return "Hello!";
    }
}