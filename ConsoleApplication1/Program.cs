using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AutomationTesting
{
    public class TestTask
    {

        public static int DEFAULT_COUNT = 10;

        // function to read <rows> lines from file <fileName>
        public List<string> ReadFile(string fileName, int rows = 10)
        {
            // empty list
            List<string> ret = new List<string>();
            try
            {

                // read all content of file to string
                string text = System.IO.File.ReadAllText(fileName);

                // regular expression, means "one or more tabs symbols, or one or more new lines"
                string sReg = @"\t+|\n+|(\r\n)+";
                Regex reg = new Regex(sReg);

                // split file content by reqular expression
                string[] split = Regex.Split(text, sReg);

                // because Regex.Split method includes in result array delimeters itself -> we need to remove delimeters from array
                split = Array.FindAll(split, l => !reg.IsMatch(l));

                // fill resulting List
                for (int i = 0; i < rows && i < split.Length; i++)
                {
                    ret.Add(split[i].Trim());
                }

            }
            catch (Exception e)
            {
                if (e is System.IO.DirectoryNotFoundException || e is System.IO.FileNotFoundException)
                {
                    Console.WriteLine("File not fould");
                }
                else
                if (e is System.UnauthorizedAccessException || e is System.Security.SecurityException)
                {
                    Console.WriteLine("Haven't access to read file");
                }
                else
                if (e is System.IO.IOException)
                {
                    Console.WriteLine("Cannot read file");
                }
                else
                {
                    Console.WriteLine("Error while trying to read file");
                }
            }

            return ret;
        }

        // entry point
        static void Main(string[] args)
        {

            // get file name
            string fileName = "";
            while (fileName.Length == 0)
            { // repeat until file name is entered
                Console.Write("Please, enter full file name: ");
                fileName = Console.ReadLine();
                if (fileName.Length == 0)
                {
                    Console.WriteLine("File name is mandatory");
                }
            }

            // get count of lines
            int count = TestTask.DEFAULT_COUNT;
            Console.Write("Please, enter count of lines (default {0}): ", TestTask.DEFAULT_COUNT);
            string sCount = Console.ReadLine();

            // if count of lines is not entered
            if (sCount.Length == 0)
            {
                Console.WriteLine("Nothing entered, using default value {0}", count);
            }
            else

            // if entered count of lines is not a number
            if (!Int32.TryParse(sCount, out count))
            {
                count = TestTask.DEFAULT_COUNT;
                Console.WriteLine("\"{0}\" doesn't look like number, using default value {1}", sCount, count);
            }

            Console.WriteLine();
            Console.WriteLine("Read {0} lines from file \"{1}\"", count, fileName);
            Console.WriteLine("---");
            Console.WriteLine();

            // create instance of class TestTask because method ReadFile is not static
            TestTask task = new TestTask();

            // read lines from file
            List<string> lines = task.ReadFile(fileName, count);

            // print readed lines to console
            foreach (var line in lines)
            {
                Console.WriteLine("{0}", line);
            }
        }

    }
}

