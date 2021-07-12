using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringHesapMakinesi
{
    class Program
    {
        static void EkranaVerilenDatayiYaz(string data)
        {
            Console.WriteLine(data);
        }

        static void EkranaOmersavasYaz()
        {
            EkranaVerilenDatayiYaz("omersavas");
        }


        static int topla(int a, int b)
        {
            int toplam = a + b;
            return toplam;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
                EkranaOmersavasYaz();


            int sonuc = topla(5, 6);
            EkranaVerilenDatayiYaz(sonuc.ToString());

            Console.ReadKey();
        }
    }
}
