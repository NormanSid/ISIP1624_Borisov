// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Serialization;

class Program
{
    struct Expense
    {
        public string Name;
        public double Amount;
    }
    static void Main() 
    {
        int n;
        Console.WriteLine("Сколько операций? (2-40):");
        while (true)
        {
            try
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n >= 2 && n <= 40)
                    break;
                else
                    Console.WriteLine("Введи число от 2 до 40:");
            }
            catch //для ошибки
            {
                Console.WriteLine("Ошибка! Введи корректное число:");
            }
        }
        Expense[] expenses = new Expense[n]; //массив
        for (int i = 0; i < n; i++)
        {
            while (true)
            {
                Console.Write($"Введи {i + 1}-ю оперцию (Название; сумма):");
                string input = Console.ReadLine();
                string[] parts = input.Split(';'); //разделяет строку по символу ; на части
                if (parts.Length == 2) // проверка что введно две части
                {
                    string name = parts[0].Trim();
                    try
                    {
                        double amount = Convert.ToDouble(parts[1].Trim());
                        if (amount >= 0)
                        {
                            expenses[i].Name = name;
                            expenses[i].Amount = amount; //запись данных в массив
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Сумма должна быть неотрицательной");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Введи корректную сумму");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат. Используй 'Имя; сумма'");
                }
            }
        }
        while (true)
        {
            Console.WriteLine("\nMenu:"); //меню
            Console.WriteLine("1 - Вывод данных");
            Console.WriteLine("2 - Статистика");
            Console.WriteLine("3 - Сортировка по цене");
            Console.WriteLine("4 - Конвертация валюты");
            Console.WriteLine("5 - Поиск по названию");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите пункт меню: ");

            string choice = Console.ReadLine();

            if (choice == "0")
                break;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nВсе расходы:");
                    foreach (var e in expenses) //цикл по каждому элементу массива
                        Console.WriteLine($"{e.Name} - {e.Amount} руб");
                    break;

                case "2":
                    double sum = 0;
                    double max = expenses[0].Amount;
                    double min = expenses[0].Amount;
                    foreach (var e in expenses)
                    {
                        sum += e.Amount;
                        if (e.Amount > max) max = e.Amount; //обновление минимума и максимума
                        if (e.Amount < min) min = e.Amount;
                    }
                    double average = sum / expenses.Length; 
                    Console.WriteLine($"Сумма: {sum} руб");
                    Console.WriteLine($"Среднее: {average:F2} руб");
                    Console.WriteLine($"Максимальное: {max} руб");
                    Console.WriteLine($"Минимальное: {min} руб");
                    break;

                case "3":
                    for (int i = 0; i < expenses.Length - 1; i++)
                    {
                        for (int j = 0; j < expenses.Length - i - 1; j++)
                        {
                            if (expenses[j].Amount > expenses[j + 1].Amount)
                            {
                                var temp = expenses[j];
                                expenses[j] = expenses[j + 1];
                                expenses[j + 1] = temp;
                            }
                        }
                    }
                    Console.WriteLine("Отсортировано по цене");
                    break;

                case "4":
                    Console.WriteLine("Конвертация валюты");
                    Console.WriteLine("1 - Введите курс вручную");
                    Console.WriteLine("2 - Выбрать из списка (USD=80, EUR=90)");
                    Console.Write("Выберите вариант: ");
                    string convChoice = Console.ReadLine();
                    double rate = 0;
                    if (convChoice == "1")
                    {
                        while (true)
                        {
                            Console.Write("Введите курс конвертации: ");
                            try
                            {
                                rate = Convert.ToDouble(Console.ReadLine());
                                if (rate > 0)
                                    break;
                                else
                                    Console.WriteLine("Курс должен быть положительным числом");
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка! Введи число");
                            }
                        }
                    }
                    else if (convChoice == "2")
                    {
                        Console.WriteLine("Выбери валюту:");
                        Console.WriteLine("1 - USD (80)");
                        Console.WriteLine("2 - EUR (90)");
                        Console.Write("Ваш выбор: ");
                        string curr = Console.ReadLine();
                        switch (curr)
                        {
                            case "1": rate = 80; break;
                            case "2": rate = 90; break;
                            default:
                                Console.WriteLine("Неверный выбор, отмена конвертации");
                                continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор, отмена конвертации");
                        continue;
                    }
                    Console.WriteLine("Результат конвертации:");
                    foreach (var e in expenses)
                    {
                        double converted = e.Amount * rate; //умножение суммы на курс
                        Console.WriteLine($"{e.Name} - {converted:F2}");
                    }
                    break;

                case "5":
                    Console.Write("Введите текст для поиска:");
                    string search = Console.ReadLine().ToLower(); //читает и переводит в нижний регистр
                    bool found = false;
                    foreach (var e in expenses)
                    {
                        if (e.Name.ToLower().Contains(search))
                        {
                            Console.WriteLine($"{e.Name} - {e.Amount} руб");
                            found = true;
                        }
                    }
                    if (!found)
                        Console.WriteLine("Совпадений не найдено");
                    break;

                default:
                    Console.WriteLine("Неверный пункт меню");
                    break;
                    }
            }
        }
    }
