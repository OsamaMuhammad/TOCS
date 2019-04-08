using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitionTable
{
    class Program
    {
        static void Main(string[] args)
        {
            //Input alphabets
            string alphabetString;
            Console.Write("Enter alphabet=");
            alphabetString = Console.ReadLine();
            char[] charArray = alphabetString.ToCharArray();
            string[] alphabets = getAlphabetArray(charArray);

            //Checks if alphabet is valid or not
            bool flag = checkValidity(alphabets);
            if (flag)
            {
                //if alphabet is valid input transition table
                int[,] transitionTable=generateTransitiontable(alphabets);

                //input final states of transition table
                int[] finalStates = finalStatesInputHelper();

                //Input strng to be checked over transition table
                Console.Write("Input String: ");
                string str = Console.ReadLine();
                string[] stringAlphabets = tokenize(alphabets, str);

                //prints transition table on console
                printTransitiontable(transitionTable, alphabets);

                //Checks if the string is valid over given transition table
                checkStringValidity(transitionTable, finalStates, stringAlphabets, alphabets);
            }
        }


        static int[,] generateTransitiontable(string[] alphabets)
        {
            int numberOfStates = 0;
            Console.WriteLine("Enter no of states");
            numberOfStates = Convert.ToInt16(Console.ReadLine());
            int[,] transitionTable = new int[numberOfStates, alphabets.Length];
            Console.WriteLine("Enter transitions:");
            for(int i = 0; i < numberOfStates; i++)
            {
                Console.WriteLine("S"+i+":");
                for(int j = 0; j < alphabets.Length; j++)
                {
                    Console.Write("Enter transition for "+ alphabets[j]+": ");
                    transitionTable[i, j] = Convert.ToInt16(Console.ReadLine());
                   
                }
            }
            return transitionTable;

        }

        static int[] finalStatesInputHelper()
        {

            Console.WriteLine("Enter number of final states");
            int numberOfFinalStates = Convert.ToInt16(Console.ReadLine());
            int[] finalStates = new int[numberOfFinalStates];
            for (int k = 0; k < numberOfFinalStates; k++)
            {
                Console.Write("Enter final state: ");
                finalStates[k] = Convert.ToInt16(Console.ReadLine());
            }

            return finalStates;

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
        static string[] tokenize(string[] alphabetArray, string myString)
        {

            string temp = "";
            int arrIndex = 0;
            bool flag = true;
            bool check = false;
            string[] stringAlphabets = new string[100];
            int stringLength = 0;
            if (myString == "")
            {
                check = true;
                flag = false;
            }
            while (flag)
            {
                if (!flag)

                    break;

                temp = temp + myString[arrIndex];

                for (int i = 0; i < alphabetArray.Length; i++)
                {
                    if (temp.Length == alphabetArray[i].Length)
                    {
                        if (temp == alphabetArray[i])
                        {
                            stringAlphabets[stringLength] = subString(myString, 0, arrIndex + 1);
                            myString = subString(myString, arrIndex + 1, myString.Length);
                            stringLength++;

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

          

            return arrayFormatter(stringAlphabets, stringLength-1);
        }

        static void checkStringValidity(int[,] transitionTable,int[] finalStates,string[] stringAlphabets,string[] alphabet)
        {
            int currentState=0;
            int currentAlphabetIndex=0;
            bool flag = false;
            
            for(int i = 0; i < stringAlphabets.Length; i++)
            {
                currentState = transitionTable[currentState, currentAlphabetIndex];
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (stringAlphabets[i] == "")
                    {
                        currentAlphabetIndex = 0;
                    }
                    
                    if (stringAlphabets[i] == alphabet[j])
                    {
                        currentAlphabetIndex = j;
                        break;
                    }
                }
                
            }

            for(int k = 0; k < finalStates.Length; k++)
            {
                if (currentState == finalStates[k])
                {
                    flag = true;
                }
            }

            if (flag)
            {
                Console.WriteLine("\nString is valid over given DFA");
                
            }
            if (!flag)
            {
                Console.WriteLine("\nString is invalid over given DFA");
            }

        }

        static void printTransitiontable(int[,] transitionTable, string[] alphabets)
        {
            Console.WriteLine();
            string p = "     |  ";
            for(int a = 0; a < alphabets.Length; a++)
            {
                p += alphabets[a] + "  |  ";
            }
            Console.WriteLine(p);
            for(int b = 0; b < p.Length-2; b++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            
            for(int i = 0; i < transitionTable.Length / alphabets.Length; i++)
            {
                Console.Write("S"+i+":  |  ");
                for(int j = 0; j < alphabets.Length; j++)
                {
                    Console.Write(transitionTable[i,j]+"  |  ");
                }
                Console.WriteLine();

                for (int b = 0; b < p.Length - 2; b++)
                {
                    Console.Write("-");
                }

                Console.WriteLine();
                
            }
        }

        
    }
}


