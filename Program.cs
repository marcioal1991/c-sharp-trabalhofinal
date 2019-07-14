using System;

namespace TrabalhoFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Manager trab = new Manager("./products.xml");
            Console.WriteLine("Digite o produto que deseja");
            string search = Console.ReadLine();
            trab.search(search);
        }
    }
}
