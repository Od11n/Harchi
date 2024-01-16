using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    struct Harchi
    {
        public string Name { get; set; }
        public double RequiredWater { get; set; }
        public int Portion { get; set; }
        public int Calories { get; set; }
    }

    static List<Harchi> elements = new List<Harchi>();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Оберiть опцiю:");
            Console.WriteLine("1. Виведення даних на екран.");
            Console.WriteLine("2. Додавання даних з клавiатури.");
            Console.WriteLine("3. Додавання даних з файлу.");
            Console.WriteLine("4. Обробка даних (видалення, редагування).");
            Console.WriteLine("5. Операцiї з даними (пiдсумок, найменьше, найбiльше).");
            Console.WriteLine("6. Збереження даних у файл.");
            Console.WriteLine("7. Вихiд.");

            int choice;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Введiть номер вибору: ");
                string input = Console.ReadLine();

                try
                {
                    choice = int.Parse(input);

                    switch (choice)
                    {
                        case 1:
                            DisplayData();
                            isValidInput = true;
                            break;
                        case 2:
                            AddDataFromKeyboard();
                            isValidInput = true;
                            break;
                        case 3:
                            AddDataFromFile();
                            isValidInput = true;
                            break;
                        case 4:
                            ProcessData();
                            isValidInput = true;
                            break;
                        case 5:
                            DataOperations();
                            isValidInput = true;
                            break;
                        case 6:
                            SaveDataToFile();
                            isValidInput = true;
                            break;
                        case 7:
                            exit = true;
                            isValidInput = true;
                            break;
                        default:
                            Console.WriteLine("Невiрний вибiр. Спробуйте ще раз.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Невiрний формат вводу. Спробуйте ще раз.");
                }
            }
        }
    }

    static void DisplayData()
    {
        foreach (Harchi element in elements)
        {
            Console.WriteLine($"Назва крупи: {element.Name}, Необхiдна кiлькiсть води: {element.RequiredWater}," +
                $" Кiлькiсть грам на порцiю: {element.Portion}, Калорiї: {element.Calories} на 100 грам");
        }
    }
    static void AddDataFromKeyboard()
    {
        Harchi newElement = new Harchi();

        Console.Write("Введiть назву крупи: ");
        newElement.Name = Console.ReadLine();

        Console.Write("Введiть потрiбну кiлькiсть води: ");
        double water;
        try
        {
            water = double.Parse(Console.ReadLine());

            if (water >= 0)
            {
                newElement.RequiredWater = water;
                Console.WriteLine("Данi додано успiшно.");
            }
            else
            {
                Console.WriteLine("Будь ласка, введiть не вiд'ємне значення.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Невiрний формат значення.");
        }
        Console.Write("Введiть кiлькiсть в граммах на 1 порцiю: ");
        int por;
        try
        {
            por = int.Parse(Console.ReadLine());

            if (por >= 0)
            {
                newElement.Portion = por;
                Console.WriteLine("Данi додано успiшно.");
            }
            else
            {
                Console.WriteLine("Будь ласка, введiть не вiд'ємне значення.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Невiрний формат значення.");
        }
        Console.Write("Введiть Кiлькiсть Ккалорiй: ");
        int value;
        try
        {
            value = int.Parse(Console.ReadLine());

            if (value >= 0)
            {
                newElement.Calories = value;
                elements.Add(newElement);
                Console.WriteLine("Данi додано успiшно.");
            }
            else
            {
                Console.WriteLine("Будь ласка, введiть не вiд'ємне значення.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Невiрний формат значення.");
        }
    }
    static void AddDataFromFile()
    {
        try
        {
            string filePath = "d:\\temp\\Harchi.json";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не iснує. Додавання даних з файлу неможливе.");
                return;
            }

            string fileContents = File.ReadAllText(filePath);
            List<Harchi> loadedData = JsonSerializer.Deserialize<List<Harchi>>(fileContents);

            if (loadedData != null)
            {
                elements.AddRange(loadedData);
                Console.WriteLine("Данi додано з файлу успiшно.");
            }
            else
            {
                Console.WriteLine("Не вдалося розпiзнати данi в файлi.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка при зчитуваннi даних з файлу: {e.Message}");
        }
    }
    static void ProcessData()
    {
        Console.WriteLine("Оберiть опцiю:");
        Console.WriteLine("1. Видалення елемента.");
        Console.WriteLine("2. Редагування елемента.");
        Console.WriteLine("3. Повернутися назад.");

        int choice;
        bool isValidInput = false;

        while (!isValidInput)
        {
            Console.Write("Введiть номер вибору: ");
            string input = Console.ReadLine();

            try
            {
                choice = int.Parse(input);

                switch (choice)
                {
                    case 1:
                        RemoveElement();
                        isValidInput = true;
                        break;
                    case 2:
                        EditElement();
                        isValidInput = true;
                        break;
                    case 3:
                        isValidInput = true; 
                        break;
                    default:
                        Console.WriteLine("Невiрний вибiр. Спробуйте ще раз.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Невiрний формат вводу. Спробуйте ще раз.");
            }
        }
    }
    static void RemoveElement()
    {
        Console.Write("Введiть iндекс елемента для видалення: ");
        try
        {
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < elements.Count)
            {
                Harchi elementToRemove = elements[index];
                elements.RemoveAt(index);
                Console.WriteLine("Елемент видалено успiшно.");
            }
            else
            {
                Console.WriteLine("Невiрний iндекс елемента.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Невiрний формат вводу. Спробуйте ще раз.");
        }
    }
    static void EditElement()
    {
        Console.Write("Введiть iндекс елемента для редагування: ");
        try
        {
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < elements.Count)
            {
                Harchi elementToEdit = elements[index];

                Console.WriteLine("Оберiть поле для редагування:");
                Console.WriteLine("1. Назва крупи.");
                Console.WriteLine("2. Потрiбна кiлькiсть води.");
                Console.WriteLine("3. Кiлькiсть грам на порцiю.");
                Console.WriteLine("4. Кiлькiсть калорiй.");

                try
                {
                    int fieldChoice = int.Parse(Console.ReadLine());

                    if (fieldChoice >= 1 && fieldChoice <= 4)
                    {
                        switch (fieldChoice)
                        {
                            case 1:
                                Console.Write("Введiть нову назву крупи: ");
                                elementToEdit.Name = Console.ReadLine();
                                break;
                            case 2:
                                Console.Write("Введiть нову потрiбну кiлькiсть води: ");
                                double requiredWater = double.Parse(Console.ReadLine());
                                if (requiredWater >= 0)
                                    elementToEdit.RequiredWater = requiredWater;
                                else
                                    Console.WriteLine("Невiрний формат чи значення.");
                                break;
                            case 3:
                                Console.Write("Введiть нову кiлькiсть грам на порцiю: ");
                                int portion = int.Parse(Console.ReadLine());
                                if (portion >= 0)
                                    elementToEdit.Portion = portion;
                                else
                                    Console.WriteLine("Невiрний формат чи значення.");
                                break;
                            case 4:
                                Console.Write("Введiть нову кiлькiсть калорiй: ");
                                int calories = int.Parse(Console.ReadLine());
                                if (calories >= 0)
                                    elementToEdit.Calories = calories;
                                else
                                    Console.WriteLine("Невiрний формат чи значення.");
                                break;
                        }
                        elements[index] = elementToEdit;

                        Console.WriteLine("Елемент вiдредаговано успiшно.");
                    }
                    else
                    {
                        Console.WriteLine("Невiрний вибiр поля для редагування.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Невiрний формат вводу. Спробуйте ще раз.");
                }
            }
            else
            {
                Console.WriteLine("Невiрний iндекс елемента.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Невiрний формат вводу iндексу. Спробуйте ще раз.");
        }
    }
    static void DataOperations()
    {
        if (elements.Count == 0)
        {
            Console.WriteLine("Колекція порожня. Немає даних для операцій.");
            return;
        }
        int maxCaloriesIndex = 0;
        int minCaloriesIndex = 0;
        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].Calories > elements[maxCaloriesIndex].Calories)
            {
                maxCaloriesIndex = i;
            }
            if (elements[i].Calories < elements[minCaloriesIndex].Calories)
            {
                minCaloriesIndex = i;
            }
        }
        Console.WriteLine($"Крупа з найбільшою кількістю калорій: {elements[maxCaloriesIndex].Name}, {elements[maxCaloriesIndex].Calories} ккал.");
        Console.WriteLine($"Крупа з найменшою кількістю калорій: {elements[minCaloriesIndex].Name}, {elements[minCaloriesIndex].Calories} ккал.");
    }
    static void SaveDataToFile()
    {
        try
        {
            string filePath = "d:\\temp\\Harchi.json";

            if (File.Exists(filePath))
            {
                Console.Write("Файл вже iснує. Хочете перезаписати його? (Y/N): ");
                string input = Console.ReadLine();

                if (input.Trim().ToUpper() != "Y")
                {
                    Console.WriteLine("Данi не були збереженi.");
                    return;
                }
            }
            string elementsJson = JsonSerializer.Serialize(elements);
            File.WriteAllText(filePath, elementsJson);

            Console.WriteLine("Данi збережено у файлi.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка при збереженнi даних у файл: {e.Message}");
        }
    }
}

