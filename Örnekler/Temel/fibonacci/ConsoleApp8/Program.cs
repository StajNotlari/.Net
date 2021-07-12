using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dizi = new int[2];

            for (int i = 0; i < dizi.Length; i++)
                dizi[i] = 0;

            dizi[1] = 1;

            Console.Write("Dizinin kaç elemanini hesaplayalim: ");
            string gecici = Console.ReadLine();
            int sinir = Convert.ToInt32(gecici);

            for (int i = 3; i <= sinir; i++)
            {
                int g = dizi[0] + dizi[1];
                dizi[0] = dizi[1];
                dizi[1] = g;
            }

            //int toplam = dizi[0] + dizi[1];

            /*int toplam = 0;
            for (int i = 0; i < dizi.Length; i++)
                toplam += dizi[i];*/

            int toplam = dizi.Sum();

            Console.WriteLine("Dizinin " + sinir + ". elemani: "+toplam);

            Console.ReadKey();
        }
    }
}
