using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWord
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Bir kelime giriniz: ");
            string okuma = Console.ReadLine();//kullnıcıdan bir satır oku
            
            Console.WriteLine("\n");

            string gecici = "omer\"savas\" 'ın evi " + " as\ndasd" + "\t\tsss";
            Console.WriteLine(gecici+"\n\n");

            //Console.WriteLine("\n");

            Console.Write("Girdiğiniz kelime: ");
            Console.WriteLine(okuma+"\n\n");

            //Console.WriteLine("\n");

            Console.Write("Girdiğiniz kelime ");
            Console.Write(okuma.Length);
            Console.WriteLine(" karakter\n\n");

            //Console.WriteLine("\n");

            Console.Write("bir kelime ");
            Console.Write(" aynı satırda 2. kısım");
            Console.WriteLine("bu satır bitince aşağı inecek");
            Console.Write("bu alt satırda bir yazı");
            //Console.Write(okuma.Length);

            Console.ReadKey();

            float 
        }
    }
}
