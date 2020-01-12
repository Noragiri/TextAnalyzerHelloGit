using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TextAnalyzer
{
    static class varia
    {
        public static string filen = "1.txt";
    }

    class Program
    {

        static void Main(string[] args)
        {
            MainMenu();

        }

        public static void MainMenu()
        {

            int choice;
            do
            {
                Console.Clear(); //Clear console each time menu is loaded
                Console.WriteLine("1. Download file from the web or use existing local file");
                Console.WriteLine("2. Count number of letters in file");
                Console.WriteLine("3. Count number of words in file");
                Console.WriteLine("4. Count number of punctuations in file");
                Console.WriteLine("5. Count number of sentences in file");
                Console.WriteLine("6. Generate report about usage of each letter (A-Z)");
                Console.WriteLine("7. Save statitics from options 2-5 in statystyki.txt");
                Console.WriteLine("8. Exit the program");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Filedown();
                        break;
                    case 2:
                        CountLetters(0);
                        Console.ReadKey();
                        break;
                    case 3:
                        CountWord(0);
                        Console.ReadKey();
                        break;
                    case 4:
                        CountPunctuation(0);
                        Console.ReadKey();
                        break;
                    case 5:
                        CountSentences(0);
                        Console.ReadKey();
                        break;
                    case 6:
                        LetterOccurrance();
                        Console.ReadKey();
                        break;
                    case 7:
                        Statistics();
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.WriteLine("Program closing, press any key to exit");
                        File.Delete(varia.filen);
                        File.Delete("Statystyki.txt");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Incorrect choice");
                        break;
                }
            } while (choice != 8);
        }
        public static void Filedown()
        {
            WebClient Client = new WebClient();
            Console.WriteLine("Do you want to download the file from net? [Y/N]");
            string answ = Console.ReadLine();

            if (answ == "Y" || answ == "y")
            {
                Console.WriteLine("Please provide web path for file :");
                string path = "https://s3.zylowski.net/public/input/1.txt";
                path = Console.ReadLine();
                try
                {
                    Client.DownloadFile(path, "1.txt");
                    Console.WriteLine("File downloaded succesfully");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Blank web path");
                }
                catch (WebException e)
                {
                    Console.WriteLine("Incorrect web path");
                }
            }
            else if (answ == "N" || answ == "n")
            {
                Console.WriteLine("Please provide name of the file :");
                varia.filen = Console.ReadLine();
                if (File.Exists(varia.filen))
                {
                    Console.WriteLine("File found succesfully");
                }
                else
                {
                    Console.WriteLine("File not found");
                }

            }
            Console.ReadKey();
        }

        public static int[] CountLetters(int x)
        {
            if (File.Exists("1.txt"))
            {
                string text = File.ReadAllText("1.txt");
                int i, len, vowel_count, cons_count;
                vowel_count = 0;
                cons_count = 0;
                len = text.Length;
                for (i = 0; i < len; i++)
                {
                    if (text[i] == 'a' || text[i] == 'e' || text[i] == 'i' || text[i] == 'o' || text[i] == 'u' || text[i] == 'A'
                       || text[i] == 'E' || text[i] == 'I' || text[i] == 'O' || text[i] == 'U')
                    {
                        vowel_count++;
                    }
                    else if ((text[i] >= 'a' && text[i] <= 'z') || (text[i] >= 'A' && text[i] <= 'Z'))
                    {
                        cons_count++;
                    }
                }
                if (x == 0)
                {
                    Console.WriteLine("Vowel in the string: {0}", vowel_count);
                    Console.WriteLine("Consonant in the string: {0}", cons_count);
                }


                int[] LetterCount = new int[] { vowel_count, cons_count };
                return LetterCount;
            }
            else
            {
                Console.WriteLine("No file found");
                return null;
            }

        }
        public static int CountWord(int x)
        {
            if (File.Exists(varia.filen))
            {

                StreamReader sr = new StreamReader(varia.filen);

                int counter = 0;
                string delim = " ;,."; //maybe some more delimiters like ?! and so on
                string[] fields = null;
                string line = null;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine(); //each time you read a line you should split it into the words
                    line.Trim();
                    fields = line.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in fields)
                    {
                        if (item.Length > 1)
                        {
                            counter++;
                        }
                    }
                }


                sr.Close();
                if (x == 0)
                {
                    Console.WriteLine("The word count is {0}", counter);
                }


                return counter;
            }
            else
            {
                Console.WriteLine("No file found");

                return 0;
            }


        }
        public static int CountPunctuation(int x)
        {
            if (File.Exists(varia.filen))
            {
                int counter = 0;
                string text = File.ReadAllText(varia.filen);
                foreach (char item in text)
                {
                    if (item == '.' || item == '?')
                    {
                        counter++;
                    }
                }
                if (x == 0)
                {
                    Console.WriteLine("Number of punctuation(dots and question marks) in file: " + counter);
                }

                return counter;
            }
            else
            {
                Console.WriteLine("No file found");

                return 0;
            }

        }
        public static int CountSentences(int x)
        {
            if (File.Exists(varia.filen))
            {

                StreamReader sr = new StreamReader(varia.filen);

                int counter = 0;
                string delim = "?."; //maybe some more delimiters like ?! and so on
                string[] fields = null;
                string line = null;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();//each time you read a line you should split it into the words
                    line.Trim();
                    fields = line.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    counter += fields.Length; //and just add how many of them there is
                }


                sr.Close();
                if (x == 0)
                {
                    Console.WriteLine("The sentence count is {0}", counter);
                }



                return counter;
            }
            else
            {
                Console.WriteLine("No file found");

                return 0;
            }
        }
        public static void LetterOccurrance()
        {
            string text = File.ReadAllText(varia.filen);
            text = text.ToUpper();
            for (int i = 65; i <= 90; i++)
            {
                int res = 0;
                for (int g = 0; g < text.Length; g++)
                {
                    if (text[g] == (char)i)
                        res++;
                }
                Console.WriteLine((char)i + ":" + res);

            }

        }
        public static void Statistics()
        {
            Console.WriteLine("Output to file: ");
            string[] lines = { "Vowel count: " + CountLetters(1)[0].ToString(), "Consonant count: " + CountLetters(1)[1].ToString(), "Word count: " + CountWord(1).ToString(), "Punctuation count(dots and question marks): " + CountPunctuation(1).ToString(), "Sentences count: " + CountSentences(1).ToString() };
            using (StreamWriter outputFile = new StreamWriter("Statystyki.txt"))
            {
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    outputFile.WriteLine(line);
                }

            }
        }
    }

}