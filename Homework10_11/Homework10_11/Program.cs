namespace Homework10_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите функцию:");
            Console.WriteLine("1.");
            Console.WriteLine("2.");
            Console.WriteLine("3. Тренажёр для заучивания формул");
            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1: Console.WriteLine("Владимир Путин - молодец");
                    break;
                case 2: Console.WriteLine("Политик, лидер и борец!");
                    break;
                case 3: Console.Clear();
                    Console.WriteLine("Добро пожаловать в тренажёр для заучивания формул!");
                    Console.WriteLine("Вам необходимо взять черновик. На экране будут появляться названия формул");
                    Console.WriteLine("Ваша задача - записать формулу на черновик, затем сравнить её с правильной формулой и указать правильно ли вы её написали");
                    Console.WriteLine("Выберите тему для тренировки:");
                    Console.WriteLine("1.");
                    Console.WriteLine("2.");
                    Console.WriteLine("3.");
                    //Должна быть проверка вводимого значения
                    int b = int.Parse(Console.ReadLine());
                    Trainer Train = new Trainer("../../../BankC.txt", b);
                    Train.Training();
                    break;
            }
        }
    }
}
