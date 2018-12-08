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
            var choiceForNavigatingMenu = 0;
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
                else
                {
                    switch (choiceForNavigatingMenu)
                    {
                        case 1:
                            {
                                string tmpName, tmpSurname, tmpAddress = " ";
                                Console.WriteLine("Please enter your name:");
                                tmpName = Console.ReadLine();
                                Console.WriteLine("Please enter your surname:");
                                tmpSurname = Console.ReadLine();
                                Console.WriteLine("Please enter your address:");
                                tmpAddress = Console.ReadLine();

                                var numberInputed = TestNumberEntry();

                                if (numberInputed != "0")
                                {
                                    if(SearchDictionaryByNumber(userDirectory,numberInputed) == null)
                                    {
                                        var returnedTuple = AddNewContact(numberInputed, tmpName, tmpSurname, tmpAddress);
                                        userDirectory.Add(returnedTuple.Item1, returnedTuple.Item2);
                                        Console.WriteLine("Succesfully added contact to directory.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There's already an contact with that number.");
                                    }
                                }
                                    choiceForNavigatingMenu = 0;
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Please enter the number of the contact you wish to modify:");
                                var numberInputed = TestNumberEntry();
                                if (numberInputed != "0")
                                {
                                    if (userDirectory.ContainsKey(numberInputed))
                                    {

                                        Console.WriteLine("Please enter which value you would like to modify[choices: name,surname,address,phonenumber]:");
                                        var choiceEntered = Console.ReadLine();
                                        Console.WriteLine($"Please enter an new value for field {choiceEntered}:");
                                        var enteredValueOnConsole = Console.ReadLine();
                                        userDirectory = ModifyExistingContact(userDirectory, enteredValueOnConsole, choiceEntered, numberInputed);                                                                       
                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("There's no such contact in your dictionary.");
                                    }
                                }
                                choiceForNavigatingMenu = 0;
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Please enter the number of the contact you wish to modify:");
                                var numberInputed = TestNumberEntry();
                                if (numberInputed != "0")
                                {
                                    var searchedDictionary = SearchDictionaryByNumber(userDirectory, numberInputed);
                                    if (searchedDictionary != null)
                                    {
                                        userDirectory = DeleteContact(userDirectory, numberInputed);
                                    }
                                    else
                                    {
                                        Console.WriteLine("The contact you're trying to delete doesn't exsist.");
                                    }
                                }
                                    choiceForNavigatingMenu = 0;
                                break;
                            }

                        case 4:
                            {
                                Console.WriteLine("Please enter the number you want to search: ");
                                var numberInputed = Console.ReadLine();
                                var searchedDictionary = SearchDictionaryByNumber(userDirectory, numberInputed);
                                if (searchedDictionary == null)
                                {
                                    Console.WriteLine("Your dictionary does not contain an contact with this number.");
                                }
                                else
                                {
                                    Console.WriteLine($"{searchedDictionary.Item1} : {searchedDictionary.Item2}");
                                }
                                choiceForNavigatingMenu = 0;
                                break;
                            }
                        case 5:
                            {
                                Console.WriteLine("Please enter name or part of name of the contact you're searching for:");
                                var parameterInputed = Console.ReadLine();
                                var searchedDictionary = SearchDictionaryByName(userDirectory, parameterInputed);
                                if (searchedDictionary.Count == 0)
                                {
                                    Console.WriteLine("Your search result came up empty, there is no such contact in your directory.");
                                }
                                else
                                {
                                    PrintDictionary(searchedDictionary);
                                }
                                choiceForNavigatingMenu = 0;
                                break;
                            }

                        case 6:
                            {
                                if (userDirectory.Count == 0)
                                {
                                    Console.WriteLine("There are no exsisting contacts.");
                                }
                                else
                                {
                                    PrintDictionary(userDirectory);
                                }
                                choiceForNavigatingMenu = 0;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("You have picked an invalid menu option.");
                                choiceForNavigatingMenu = 0;
                            }
                            break;
                    }
                }


            } while (choiceForNavigatingMenu != 7);
        }
        static string TestNumberEntry()
        {
            string tmpNumberFirstInput, tmpNumberSecondInput = " ";
            do
            {
                Console.WriteLine("Please enter your number:");
                tmpNumberFirstInput = Console.ReadLine();
                if (tmpNumberFirstInput != "0")
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

            } while (!(tmpNumberFirstInput == tmpNumberSecondInput));
            return tmpNumberFirstInput;
        }
        static Dictionary<string, Tuple<string, string, string>> SearchDictionaryByName(Dictionary<string, Tuple<string, string, string>> argDictionaryPassed, string argSearchParameterPassed)
        {
            var searchedDictionary = new Dictionary<string, Tuple<string, string, string>>();
            foreach (var item in argDictionaryPassed)
            {
                if (item.Value.Item1.Contains(argSearchParameterPassed) || item.Value.Item2.Contains(argSearchParameterPassed))
                    searchedDictionary.Add(item.Key, new Tuple<string, string, string>(item.Value.Item1, item.Value.Item2, item.Value.Item3));
            }
            return searchedDictionary;
        }
        static Tuple<string, Tuple<string, string, string>> SearchDictionaryByNumber(Dictionary<string, Tuple<string, string, string>> argDictionaryPassed, string numberPassed)
        {
            if(argDictionaryPassed.ContainsKey(numberPassed))
            {
                return new Tuple<string, Tuple<string, string, string>>(numberPassed, new Tuple<string, string, string>(argDictionaryPassed[numberPassed].Item1, argDictionaryPassed[numberPassed].Item2, argDictionaryPassed[numberPassed].Item3));
            }
            else
            {
                return null;
            }
        }
        static void PrintDictionary(Dictionary<string, Tuple<string, string, string>> argDirectoryPassed)
        {
            //var sortedDictionary = from entry in argDirectoryPassed orderby entry.Value ascending select entry.Value.Item1;
            foreach (var item in argDirectoryPassed.OrderBy((item)=> item.Value.Item2).ThenBy((item) => item.Value.Item1))
            {
                Console.WriteLine($"{item.Key} : {item.Value.Item1} : {item.Value.Item2} : {item.Value.Item3}");
            }
        }
        static Tuple<string, Tuple<string, string, string>> AddNewContact(string argKeyPassed, string argNamePassed, string argSurnamePassed, string argAddressPassed)
        {
            argKeyPassed = CheckAndAdjustNumber(argKeyPassed);
            argNamePassed = CheckAndAdjustInput(argNamePassed);
            argSurnamePassed = CheckAndAdjustInput(argSurnamePassed);
            argAddressPassed = CheckAndAdjustAddress(argAddressPassed);
            return new Tuple<string, Tuple<string, string, string>>(argKeyPassed, new Tuple<string, string, string>(argNamePassed, argSurnamePassed, argAddressPassed));
        }
        static Dictionary<string, Tuple<string, string, string>> DeleteContact(Dictionary<string, Tuple<string, string, string>> argDictionaryPassed, string argKeyPassed)
        {
            var modifiedDictionary = new Dictionary<string, Tuple<string, string, string>>();
            foreach (var item in argDictionaryPassed)
            {
                if (item.Key != argKeyPassed)
                {
                    var tmpTuple = AddNewContact(item.Key, item.Value.Item1, item.Value.Item2, item.Value.Item3);
                    modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                }
            }
            return modifiedDictionary;
        }
        static Dictionary<string, Tuple<string, string, string>> ModifyExistingContact(Dictionary<string, Tuple<string, string, string>> argDictionaryPassed, string entryOfModification, string argChoicePicked, string argKeyPassed)
        {
            var modifiedDictionary = new Dictionary<string, Tuple<string, string, string>>();
            switch (argChoicePicked)
            {
                case "name":
                    {
                        foreach (var item in argDictionaryPassed)
                        {
                            if (item.Key != argKeyPassed)
                            {
                                var tmpTuple = AddNewContact(item.Key, item.Value.Item1, item.Value.Item2, item.Value.Item3);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }
                            else
                            {
                                var tmpTuple = AddNewContact(item.Key, entryOfModification, item.Value.Item2, item.Value.Item3);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }

                        }
                        break;
                    }
                case "surname":
                    {
                        foreach (var item in argDictionaryPassed)
                        {
                            if (item.Key != argKeyPassed)
                            {
                                var tmpTuple = AddNewContact(item.Key, item.Value.Item1, item.Value.Item2, item.Value.Item3);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }
                            else
                            {
                                var tmpTuple = AddNewContact(item.Key, item.Value.Item1, entryOfModification, item.Value.Item3);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }

                        }
                        break;
                    }
                case "address":
                    {
                        foreach (var item in argDictionaryPassed)
                        {
                            if (item.Key != argKeyPassed)
                            {
                                var tmpTuple = AddNewContact(item.Key, item.Value.Item1, item.Value.Item2, item.Value.Item3);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }
                            else
                            {
                                var tmpTuple = AddNewContact(item.Key, item.Value.Item1, item.Value.Item2, entryOfModification);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }

                        }
                        break;
                    }
                case "phonenumber":
                    {
                        foreach (var item in argDictionaryPassed)
                        {
                            if (item.Key != argKeyPassed)
                            {
                                var tmpTuple = AddNewContact(item.Key, item.Value.Item1, item.Value.Item2, item.Value.Item3);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }
                            else
                            {
                                var tmpTuple = AddNewContact(entryOfModification, item.Value.Item1, item.Value.Item2, item.Value.Item3);
                                modifiedDictionary.Add(tmpTuple.Item1, tmpTuple.Item2);
                            }

                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("This is not an valid choice.");
                        break;
                    }

            }
            return modifiedDictionary;

        }
        static string CheckAndAdjustNumber(string argNumberPassed)
        {
            if (argNumberPassed.Contains(' '))
            {
                argNumberPassed.Trim(' ');
            }
            return argNumberPassed;
        }
        static string CheckAndAdjustInput(string argInputPassed)
        {
            if (argInputPassed.Contains(' '))
            {
                argInputPassed.Trim(' ');
            }
            if (char.IsLower(argInputPassed[0]))
            {
                var tmpString = new StringBuilder(argInputPassed);
                tmpString[0] = char.ToUpper(tmpString[0]);
                argInputPassed = tmpString.ToString();
            }

            return argInputPassed;
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