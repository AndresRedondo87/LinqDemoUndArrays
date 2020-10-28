using System;
using System.Collections.Generic;   //ERFORDERLICH FÜR LINQ und IEnumerables
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



            /// Linq mit Listen
            /// StandardabfrageOperationen
            /// https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview
            /// 
            UniversityManager um = new UniversityManager();
            um.MaleStudents();
            um.FemaleStudents();


            Console.WriteLine();
            Console.WriteLine();
            // BEISPIEL AUS INTERNET


            // Sortieren https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/concepts/linq/sorting-data
            string[] words1 = { "the", "quick", "brown", "fox", "jumps" };

            IEnumerable<string> query1 = from word in words1
                                        orderby word.Length
                                        select word;

            foreach (string str in query1)
                Console.WriteLine(str);

            /* This code produces the following output:  

                the  
                fox  
                quick  
                brown  
                jumps  
            */
            /// https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview
            /// 
            string sentence = "the quick brown fox jumps over the lazy dog";
            Console.WriteLine(sentence);
            // Split the string into individual words to create a collection.  
            string[] words = sentence.Split(' ');

            // Using query expression syntax.  
            var query = from word in words
                        group word by word.Length into gr
                        orderby gr.Key
                        select new { Length = gr.Key, Words = gr };

            //// Using method-based query syntax.  
            //var query2 = words.
            //    GroupBy(w => w.Length, w => w.ToUpper()).
            //    Select(g => new { Length = g.Key, Words = g }).
            //    OrderBy(o => o.Length);

            foreach (var obj in query)
            {
                Console.WriteLine("Words of length {0}:", obj.Length);
                foreach (string word in obj.Words)
                    Console.WriteLine(word);
            }
            //--------------------------------- BEISPIEL AUS INTERNET----- ENDE

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
            // IN SQL SIND DIE "FROM,IN,SELECT usw.. GROßGESCHRIEBEN.
            // IN LINQ ABER MUSS ALLES KLEINGESCHRIEBEN SEIN!

            ///Meine einfache ausgabe von den Ungerade Zahlen
            Console.WriteLine(oddNumbers);// das gibt uns die Typ, klasse, aber nicht den inhalt...

            foreach (int oddNumber in oddNumbers)
            {
                Console.WriteLine(oddNumber);
            }
        }
    }

    // UM DIE Verbindung zwischen Unis und Studenten zu setzen:
    public class UniversityManager
    {
        public List<University> universities;
        public List<Student> students;

        // Konstruktor
        public UniversityManager()
        {
            //initialisieren
            universities = new List<University>();
            students = new List<Student>();

            // Befüllen der UniversitätenList
            universities.Add(new University { Id = 1, Name = "Yale" });
            universities.Add(new University { Id = 2, Name = "Valladolid" });


            // Befüllen der StudentsList
            students.Add(new Student { Id = 1, Name = "Carla", Gender = "female", Age = 18, UniversityId = 1 });
            students.Add(new Student { Id = 2, Name = "Tony", Gender = "male", Age = 21, UniversityId = 1 });
            students.Add(new Student { Id = 3, Name = "Leyla", Gender = "female", Age = 19, UniversityId = 2 });
            students.Add(new Student { Id = 4, Name = "James", Gender = "male", Age = 25, UniversityId = 2 });
            students.Add(new Student { Id = 5, Name = "Linda", Gender = "female", Age = 20, UniversityId = 2 });
            students.Add(new Student { Id = 6, Name = "James2", Gender = "male", Age = 26, UniversityId = 2 });
            students.Add(new Student { Id = 7, Name = "James3", Gender = "male", Age = 23, UniversityId = 2 });
            students.Add(new Student { Id = 8, Name = "James4", Gender = "male", Age = 22, UniversityId = 2 });
            students.Add(new Student { Id = 9, Name = "James5", Gender = "male", Age = 27, UniversityId = 2 });
        }

        // Noch eine Methode um alle die Männlichen anzuzeigen:

        public void MaleStudents()
        {
            IEnumerable<Student> allMaleStudents = from student in students where student.Gender.ToUpper() == "MALE" select student;
            //ToUpper für male, Male, MALE, MAle,maLE... zu erkennen
            Console.WriteLine("Männliche Studenten:");// das gibt uns die Typ, klasse, aber nicht den inhalt...

            //foreach (var student in allMaleStudents)        
            //var funktioniert auch immernoch, könnte aber problematisch werden bei der Erkennung
            foreach (Student student in allMaleStudents)
            {
                //Console.WriteLine(student.Name); //das war meine Option
                student.Print();    // so ist es vollständiger
            }
        }

        /// "HERAUSFORDDERUNG"" genau das gleiche aber für weiblichen....GENIALES BEISPIEL!!!--- MEGA BLÖD!!
        public void FemaleStudents()
        {
            IEnumerable<Student> allFemaleStudents = from student in students where student.Gender.ToUpper() == "FEMALE" select student;
            Console.WriteLine("Weibliche Studenten:");// das gibt uns die Typ, klasse, aber nicht den inhalt...
            //foreach (var student in allMaleStudents)        
            foreach (Student student in allFemaleStudents)
            {
                student.Print();
            }
        }
    }
}


public class University
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Print()
    {
        Console.WriteLine($"Universität {Name} mit der Id {Id}");
    }
}


public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }


    // FOREIGN KEY... VERBINDUNG ZWISCHEN KLASSEN??? Via UniversityManager Klasse
    public int UniversityId { get; set; }

    public void Print()
    {
        Console.WriteLine($"Student {Name} mit der Id {Id}, gender {Gender} " +
            $"is {Age} Jahre alt, von der Universität mit Id {UniversityId}");
    }
}

