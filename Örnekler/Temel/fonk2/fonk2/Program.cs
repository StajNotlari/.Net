using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fonk2
{
    class Program
    {
        static void EkranaVerilenDatayiYaz(string data)

        {
            Console.WriteLine(data);
        }
    
        static void EkranaArazbabayevYaz()

        {
            EkranaVerilenDatayiYaz("araz babayev");
        }

        static int Topla(int a, int b)
        {
            return a + b;
        }
        static void Main()
        {
            int a = Topla(2, 5);
            Console.Write(a);
            Console.ReadKey();
        }
    }
  }

