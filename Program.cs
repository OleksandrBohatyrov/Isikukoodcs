using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isikukood
{
    public class Program
    {
        public static void Main()
        {


            while (true)
            {
                Console.WriteLine("Sisestage ID-kood (11-kohaline) või väljumiseks \"q\": ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "q")
                    break;

                if (IdCode.IsValidIdCode(input))
                {
                    IdCode id = new IdCode(input);
                    Console.WriteLine($"Sünniaasta: {id.GetFullYear()}");
                    Console.WriteLine($"Sünniaeg: {id.GetBirthDate():dd.MM.yyyy}");
                    Console.WriteLine($"Paul: {id.GetGender()}");
                }
                else
                {
                    Console.WriteLine("Vale identifitseerimiskood. Proovige uuesti.");
                }
            }
            //Console.WriteLine(new IdCode("27605030298").GetFullYear());  // 1876
            //Console.WriteLine(new IdCode("37605030299").GetFullYear());  // 1976
            //Console.WriteLine(new IdCode("50005200009").GetFullYear());  // 2000
            //Console.WriteLine(new IdCode("27605030298").GetBirthDate());  // 03.05.1876
            //Console.WriteLine(new IdCode("37605030299").GetBirthDate());  // 03.05.1976
            //Console.WriteLine(new IdCode("50005200009").GetBirthDate());  // 20.05.2000


            //Console.WriteLine(new IdCode("a").IsValid());  // False
            //Console.WriteLine(new IdCode("123").IsValid());  // False
            //Console.WriteLine(new IdCode("37605030299").IsValid());  // True
            //                                                         // 30th February
            //Console.WriteLine(new IdCode("37602300299").IsValid());  // False
            //Console.WriteLine(new IdCode("52002290299").IsValid());  // False
            //Console.WriteLine(new IdCode("50002290231").IsValid());  // True
            //Console.WriteLine(new IdCode("30002290231").IsValid());  // False

            //// control number 2nd round
            //Console.WriteLine(new IdCode("51809170123").IsValid());  // True
            //Console.WriteLine(new IdCode("39806302730").IsValid());  // True

            //// control number 3rd round
            //Console.WriteLine(new IdCode("60102031670").IsValid());  // True
            //Console.WriteLine(new IdCode("39106060750").IsValid());  // True
        }
    }
}