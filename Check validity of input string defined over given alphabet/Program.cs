using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabetString;
            Console.Write("Enter alphabet=");
            
            //Input string of alphabets
            alphabetString = Console.ReadLine();

            //Convert string to character Array
            char[] charArray = alphabetString.ToCharArray();

            //remove unnecessary characters such as comma and curly brackets and store only aphabets in a string array
            string[] alphabets = getAlphabetArray(charArray);

            //Checks if the input alphabets are valid or not
            bool flag = checkAlphabetValidity(alphabets);
            if (flag)
            {

                //if alphabets are valid input a string and check if the input string is valid over given alphabets
                Console.WriteLine("\nEnter string ");
                string myString = Console.ReadLine();
                checkStringValidity(alphabets, myString);


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

            // @params alphabetArray was initialized to be of size 100. 
            // the arrayFormatter function formats this array to have its size same as the number of alphabets. 
            return arrayFormatter(alphabetArray, arrayPointer);

        }


        static string[] arrayFormatter(string[] array, int arrayPointer)
        {
            string[] finalArray = new string[arrayPointer + 1];
            for (int i = 0; i <= arrayPointer; i++)
                finalArray[i] = array[i];

            return finalArray;
        }

        static bool checkAlphabetValidity(string[] alphabets)
        {
            bool flag = true;
            if(alphabets[0]==null)
                flag=false;
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
                            if (tempA.Substring(0, tempA.Length) == tempB.Substring(0, tempA.Length))
                            {
                                flag = false;
                                break;
                            }
                        }
                        else
                        {
                            if (tempA.Substring(0, tempB.Length) == tempB.Substring(0, tempB.Length))
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

        static void checkStringValidity(string[] alphabetArray, string myString)
        {

            string temp = "";
            int arrIndex = 0;
            bool flag = true;
            bool check = false;
            while (flag)
            {
                temp = temp + myString[arrIndex];

                for (int i = 0; i < alphabetArray.Length; i++)
                {
                    if (temp.Length == alphabetArray[i].Length)
                    {
                        if (temp == alphabetArray[i])
                        {
                            myString = myString.Substring(arrIndex + 1);

                            if (myString == "" || myString == null)
                            {
                                check = true;
                                flag = false;
                                break;
                            }
                            temp = "";
                            arrIndex = -1;
                            break;

                        }
                    }
                }
                if (temp == myString)
                {
                    break;
                }
                arrIndex++;

            }

            if (check)
            {
                Console.WriteLine("\nString is valid");
            }
            if (!check)
            {
                Console.WriteLine("\nString is invalid");
            }


        }
    }
}
