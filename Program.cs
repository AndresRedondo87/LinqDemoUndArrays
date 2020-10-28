using System;
using System.Collections.Generic;   //ERFORDERLICH FÜR LINQ
using System.Linq;                  //ERFORDERLICH FÜR LINQ
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LinqDemoUndArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            OddNumbers(numbers);

            Console.ReadKey();
        }

         static void OddNumbers(int[] numbers)
        {
            Console.WriteLine("Ungerade Zahlen");

            /// Linq ist wie SQL in C# zu integrieren
            /// hier machen wir wie eine for schleife für die array numbers.
            /// Von den ganzen Array, für jede eintrag (from number in numbers)
            /// checken wir ob es gerade oder ungerade ist: "% 2 != 0"
            /// Die ungeraden ("where number % 2 != 0") werden ausgewählt via SELECT
            /// Und natürlich in den oddNumbers gespeichert
            IEnumerable<int> oddNumbers = from number in numbers where number % 2 != 0 select number;


            ///Meine einfache ausgabe von den Ungerade Zahlen
            Console.WriteLine(oddNumbers);// das gibt uns die Typ, klasse, aber nicht den inhalt...

            foreach (int oddNumber in oddNumbers)
            {
                Console.WriteLine(oddNumber);
            }
        }
    }
}
