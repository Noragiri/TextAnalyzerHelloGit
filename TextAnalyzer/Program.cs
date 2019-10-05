﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TextAnalyzer
{
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
                Console.WriteLine("1. Download file from the web");
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
                        CountLetters();
                        Console.ReadKey();
                        break;
                    case 3:
                        CountWord();
                        Console.ReadKey();
                        break;
                    case 4:
                        CountPunctuation();
                        Console.ReadKey();
                        break;
                    case 5:
                        CountSentences();
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
            Client.DownloadFile("https://s3.zylowski.net/public/input/1.txt", "C:1.txt");
            Console.WriteLine("File downloaded succesfully");
            Console.ReadKey();
        }

        public static int CountLetters()
        {
            if (File.Exists("1.txt"))
            {
                string text = File.ReadAllText("1.txt");
                Console.WriteLine("Number of letters in file: " + text.Count(char.IsLetter));
                return text.Count(char.IsLetter);
            }
            else
            {
                Console.WriteLine("No file found");
                return 0;
            }

        }
        public static int CountWord()
        {
            if (File.Exists("1.txt"))
            {

                StreamReader sr = new StreamReader("1.txt");

                int counter = 0;
                string delim = " ;,."; //maybe some more delimiters like ?! and so on
                string[] fields = null;
                string line = null;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine(); //each time you read a line you should split it into the words
                    line.Trim();
                    fields = line.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    counter += fields.Length; //and just add how many of them there is
                }


                sr.Close();
                Console.WriteLine("The word count is {0}", counter);
                
                return counter;
            }
            else
            {
                Console.WriteLine("No file found");
                
                return 0;
            }


        }
        public static int CountPunctuation()
        {
            if (File.Exists("1.txt"))
            {
                string text = File.ReadAllText("1.txt");
                Console.WriteLine("Number of punctuation in file: " + text.Count(char.IsPunctuation));
                
                return text.Count(char.IsPunctuation);
            }
            else
            {
                Console.WriteLine("No file found");
                
                return 0;
            }

        }
        public static int CountSentences()
        {
            if (File.Exists("1.txt"))
            {

                StreamReader sr = new StreamReader("1.txt");

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
                Console.WriteLine("The sentence count is {0}", counter);

               
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
            string text = File.ReadAllText("1.txt");
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
            string[] lines = { "Letter count: "+CountLetters().ToString(), "Word count: " + CountWord().ToString(), "Punctuation count: " + CountPunctuation().ToString(), "Sentences count: " + CountSentences().ToString() };
            using (StreamWriter outputFile = new StreamWriter("Statystyki.txt"))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }
    }

}

