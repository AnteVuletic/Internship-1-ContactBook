using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var userDirectory = new Dictionary<string, Tuple<string, string, string>>();
            var choiceForNavigatingMenu = 0 ;
            do
            {
                if (choiceForNavigatingMenu == 0)
                {
                    Console.WriteLine(" _______________________________________________ ");
                    Console.WriteLine("|                                               |");
                    Console.WriteLine("| 1. Add new contact                            |");
                    Console.WriteLine("| 2. Modify contact name,address or phonenumber |");
                    Console.WriteLine("| 3. Delete contact                             |");
                    Console.WriteLine("| 4. Search by number                           |");
                    Console.WriteLine("| 5. Search by name                             |");
                    Console.WriteLine("| 6. Display all contacts                       |");
                    Console.WriteLine("| 7. Stop application                           |");
                    Console.WriteLine("|_______________________________________________|");
                    Console.WriteLine();
                    Console.WriteLine("Your choice is:");
                    choiceForNavigatingMenu = int.Parse(Console.ReadLine());
                }
                else {
                    switch (choiceForNavigatingMenu)
                    {
                        case 1:
                            {
                                string tmpNumberFirstInput, tmpNumberSecondInput, tmpName, tmpSurname, tmpAddress = " ";
                                Console.WriteLine("Please enter your name:");
                                tmpName = Console.ReadLine();
                                Console.WriteLine("Please enter your surname:");
                                tmpSurname = Console.ReadLine();
                                Console.WriteLine("Please enter your address:");
                                tmpAddress = Console.ReadLine();
                                do {
                                    Console.WriteLine("Please enter your number:");
                                    tmpNumberFirstInput = Console.ReadLine();
                                    if(tmpNumberFirstInput != "0")
                                    {
                                        Console.WriteLine("Please re-enter your number:");
                                        tmpNumberSecondInput = Console.ReadLine();
                                        if (tmpNumberFirstInput != tmpNumberSecondInput)
                                        {
                                            Console.WriteLine("Numbers do not match. Please try again. Optionally opt out by typing in 0 as number.");
                                        }
                                    }
                                    else
                                    {
                                        tmpNumberSecondInput = tmpNumberFirstInput;
                                    }
                                    
                                } while (!tmpNumberFirstInput.Equals(tmpNumberSecondInput));
                                if(tmpNumberFirstInput != "0")
                                {
                                    var returnedTuple = AddNewContact(tmpNumberFirstInput,tmpName,tmpSurname,tmpAddress);
                                    userDirectory.Add(returnedTuple.Item1, returnedTuple.Item2);
                                    Console.WriteLine("Succesfully added contact to directory.");
                                    choiceForNavigatingMenu = 0;
                                }
                                else
                                {
                                    choiceForNavigatingMenu = 0;
                                }
                                break;
                            }
                        case 2:
                            {
                                
                                break;
                            }
                        case 3:
                            break;
                        case 4:
                            {
                                Console.WriteLine("Please enter the number you want to search: ");
                                var numberInputed = Console.ReadLine();
                                var searchedDictionary = SearchDictionaryByNumber(userDirectory, numberInputed);
                                if (searchedDictionary.Count == 0)
                                {
                                    Console.WriteLine("Your dictionary does not contain an contact with this number.");
                                    choiceForNavigatingMenu = 0;
                                }
                                else
                                {
                                    PrintDirectory(searchedDictionary);
                                    choiceForNavigatingMenu = 0;
                                }
                                break;
                            }
                        case 5:
                            break;
                        case 6:
                            {
                                if(userDirectory.Count == 0)
                                {
                                    Console.WriteLine("There are no exsisting contacts.");
                                    choiceForNavigatingMenu = 0;
                                }
                                else
                                {
                                    PrintDirectory(userDirectory);
                                    choiceForNavigatingMenu = 0;
                                }       
                                break;
                            }
                        default:
                            Console.WriteLine("You have picked an invalid menu option.");
                            break;
                    }
                }
              

            } while (choiceForNavigatingMenu != 7);
        }
        static Dictionary<string,Tuple<string,string,string>> SearchDictionaryByNumber(Dictionary<string,Tuple<string,string,string>> argDictionaryPassed,string numberPassed)
        {
            var searchedDictionary = new Dictionary<string, Tuple<string, string, string>>();
            foreach (var item in argDictionaryPassed)
            {
                if (item.Key == numberPassed)
                    searchedDictionary.Add(item.Key, new Tuple<string, string, string>(item.Value.Item1, item.Value.Item2, item.Value.Item3));
            }
            return searchedDictionary;
        }
        static void PrintDirectory(Dictionary<string, Tuple<string, string, string>> argDirectoryPassed)
        {
            foreach (var item in argDirectoryPassed)
            {
                Console.WriteLine($"{item.Key} : {item.Value.Item1} : {item.Value.Item2} : {item.Value.Item3}");
            }
        }
        static Tuple<string, Tuple<string, string, string>> AddNewContact(string argKeyPassed, string argNamePassed, string argSurnamePassed, string argAddressPassed)
        {
            argKeyPassed = CheckAndAdjustNumber(argKeyPassed);
            argNamePassed = CheckAndAdjustName(argNamePassed);
            argSurnamePassed = CheckAndAdjustSurname(argSurnamePassed);
            argAddressPassed = CheckAndAdjustAddress(argAddressPassed);
            return new Tuple<string,Tuple<string,string,string>>(argKeyPassed,new Tuple<string,string,string>(argNamePassed,argSurnamePassed,argAddressPassed));
        }
        static string CheckAndAdjustNumber(string argNumberPassed)
        {
            if (argNumberPassed.Contains(' '))
            {
                argNumberPassed.Trim(' ');
            }
            return argNumberPassed;
        }
        static string CheckAndAdjustName(string argNamePassed)
        {
            if(argNamePassed.Contains(' '))
            {
                argNamePassed.Trim(' ');
            }
            if (char.IsLower(argNamePassed[0]))
            {
                var tmpString = new StringBuilder(argNamePassed);
                tmpString[0] = char.ToUpper(tmpString[0]);
                argNamePassed = tmpString.ToString();
            }
            
            return argNamePassed;
        }
        static string CheckAndAdjustSurname(string argSurnamePassed)
        {
            if( argSurnamePassed.Contains(' '))
            {
                argSurnamePassed.Trim(' ');
            }
            return argSurnamePassed;
        }
        static string CheckAndAdjustAddress(string argAddressPassed)
        {
            if (argAddressPassed[0] == ' ')
            {
                argAddressPassed.TrimStart(' ');
            }
            if (argAddressPassed[argAddressPassed.Length - 1] == ' ')
            {
                argAddressPassed.TrimEnd(' ');
            }
            return argAddressPassed;
        }
    }
}

