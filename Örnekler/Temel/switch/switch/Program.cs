using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @switch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Bir sayi girin: ");
            string gecici = Console.ReadLine();
            int sayi = Convert.ToInt32(gecici);

            if(sayi == 1)
                Console.WriteLine("Bir");
            else if(sayi == 2)
                Console.WriteLine("İki");
            else
                Console.WriteLine("1 yada 2 değil");

            switch (sayi)
            {
                case 1:
                    Console.WriteLine("Bir");
                    break;
                case 2:
                    Console.WriteLine("İki");
                    break;
                default:
                    Console.WriteLine("1 yada 2 değil");
                    break;
            }

            Console.ReadKey();
        }
    }
}
