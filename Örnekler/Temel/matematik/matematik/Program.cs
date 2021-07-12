using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matematik
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Faktoryeli alinacak sayiyi giriniz: ");

            string gecici = Console.ReadLine();
            int sayi = Convert.ToInt32(gecici);

            int sonuc = 1;

            for (int i = 1; i <= sayi; i++)
            {
                sonuc = sonuc * i;
            }

            Console.WriteLine("Sonuc: " + sonuc + "\n\n\n");


            Console.WriteLine("Bir sayı giriniz: ");
            gecici = Console.ReadLine();
            sayi = Convert.ToInt32(gecici);

            if(sayi > 0)
            {
                Console.WriteLine("Sayı pozitiftir.");
            }
            else if(sayi == 0)
            {
                Console.WriteLine("Sayı notrdur.");
            }
            else
            {
                Console.WriteLine("Sayı negatiftir");
            }



            if (sayi%2==0)
            {
                Console.WriteLine("Sayı çifttir.");
            }
            else
            {
                Console.WriteLine("Sayı tektir.");
            }

            Console.ReadKey();
        }
    }
}
