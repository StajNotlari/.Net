using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringHesapMakinesi
{
    class Program
    {
        static int[] sayilar = new int[20];
        static string[] isaretler = new string[19];

        static int isaretlerIndex, sayilarIndex;



        static void DenklemiDizilereDoldur(string denklem)
        {
            isaretlerIndex = 0;
            sayilarIndex = 0;

            foreach (var tekKarakter in denklem)
            {
                int gecici;
                bool sayiMi = int.TryParse(tekKarakter.ToString(), out gecici);

                if (sayiMi)
                    sayilar[sayilarIndex++] = gecici;
                else
                    isaretler[isaretlerIndex++] = tekKarakter.ToString();
            }
        }

        static double DizilerdekiDatadanSonucGetir()
        {
            return 0.0;
        }

        static void Main(string[] args)
        {
            //Console.Write("Denklem giriniz: ");
            //string denklem = Console.ReadLine();

            string denklem = "7/8+9+5";
            DenklemiDizilereDoldur(denklem);
            double sonuc = DizilerdekiDatadanSonucGetir();
            Console.WriteLine("Sonuc: " + sonuc);

            Console.ReadKey();
        }
    }
}
