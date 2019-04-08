using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
         
            //Input alphabet string   
            string alphabetString;
            Console.Write("Enter alphabet=");
            alphabetString = Console.ReadLine();

            //Convert alphabet string to character array
            char[] charArray = alphabetString.ToCharArray();

            //Remove unnecessary characters such as commas, curly brackrts
            string[] alphabets = getAlphabetArray(charArray);

            //Check if the input strings are valid
            
            bool flag = checkValidity(alphabets);

            
            if (flag)
            {
                //If valid then generate all possible strings of given length defined over given alphabets
                Console.WriteLine("\nEnter choice from below\n1: Set of all strings\t\t2: Set of strings of speccified length");
                
                int choice = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("");
                
                if (choice == 1)
                {
                    Console.WriteLine("^");

                    generateStrings(alphabets, alphabets.Length, 1);
                    generateStrings(alphabets, alphabets.Length, 2);
                    generateStrings(alphabets, alphabets.Length, 3);
                    generateStrings(alphabets, alphabets.Length, 4);

                    Console.WriteLine(".\n.\n.\n");

                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter Length");
                    
                    int stringLength = Convert.ToInt16(Console.ReadLine());
                    if (stringLength == 0)
                    {
                        Console.WriteLine("^");
                    }
                    else
                    generateStrings(alphabets, alphabets.Length, stringLength);
                }
                
                
            }
        }

        

        static string[] getAlphabetArray(char[] charArray)
        {
            string[] alphabetArray = new string[100];
            int arrayPointer = 0;
            string temp = null;
            for (int i = 0; i < charArray.Length; i++)
            {

                if (charArray[i] == ',' && temp != null)
                {
                    alphabetArray[arrayPointer] = temp;
                    temp = null;
                    arrayPointer++;
                }

                else if (charArray[i] != ',')
                {
                    if (charArray[i] != '{')
                    {
                        if (charArray[i] != '}')
                        {
                            temp = temp + charArray[i];
                        }
                    }

                }

            }
            if (temp != null)
            {
                alphabetArray[arrayPointer] = temp;
            }
            return arrayFormatter(alphabetArray, arrayPointer);

        }


        static string[] arrayFormatter(string[] array, int arrayPointer)
        {
            string[] finalArray = new string[arrayPointer + 1];
            for (int i = 0; i <= arrayPointer; i++)
                finalArray[i] = array[i];

            return finalArray;
        }

        static bool checkValidity(string[] alphabets)
        {
            bool flag = true;
            if (alphabets[0] == null)
                flag = false;
            for (int i = 0; i < alphabets.Length; i++)
            {
                if (flag == false)
                    break;
                string tempA = alphabets[i];
                string tempB = null;
                for (int j = 0; j < alphabets.Length; j++)
                {
                    if (flag == false)
                        break;
                    tempB = alphabets[j];
                    if (j != i)
                    {
                        if (tempA.Length <= tempB.Length)
                        {
                            if (subString(tempA, 0, tempA.Length) == subString(tempB, 0, tempA.Length))
                            {
                                flag = false;
                                break;
                            }
                        }
                        else
                        {
                            if (subString(tempA, 0, tempB.Length) == subString(tempB, 0, tempB.Length))
                            {
                                flag = false;
                                break;
                            }
                        }

                    }
                }
            }

            if (flag)
            {
                Console.WriteLine("\nGiven alphabet is valid");
            }
            else if (!flag)
            {
                Console.WriteLine("\nGiven alphabet is invalid");
            }

            return flag;
        }

        
        static string subString(string str, int startIndex, int stopIndex)
        {
            string subString = "";
            for (int i = startIndex; i < stopIndex; i++)
            {
                subString = subString + str[i];

            }
            return subString;
        }


        private static void generateStrings(string[] alphabets, int length, int stringLength)
        {
            int stringArrayLength = 1;
            int numberOfAlphabets = 1;
            //calculate no of possible Srings
            for (int i = 0; i < stringLength; i++)
            {
                stringArrayLength = stringArrayLength * length;
                numberOfAlphabets = numberOfAlphabets * length;
            }

            numberOfAlphabets=numberOfAlphabets / length;
            string[] stringArray = new string[stringArrayLength];

            

            for (int j = 0; j < stringLength; j++)
            {
                int count = 0;

                while (count != stringArrayLength)
                {
                    for (int k = 0; k < length; k++)
                    {
                        count = concatenate(stringArray, numberOfAlphabets, alphabets[k], count);
                    }
                }

                numberOfAlphabets = numberOfAlphabets / length;
            }

            
            //prints the string array on console
            printStringArray(stringArray);
        }

        private static int concatenate(string[] stringArray, int numberOfAlphabets, string alphabet, int count)
        {

            for (int i = 0; i < numberOfAlphabets; i++)
            {
                stringArray[count] = stringArray[count] + alphabet;
                count++;
            }
            return count;
        }


        private static void printStringArray(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

    }
}
