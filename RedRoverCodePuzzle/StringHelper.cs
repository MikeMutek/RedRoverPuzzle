using System.Text;

namespace RedRoverCodePuzzle
{
    internal static class StringHelper
    {
        //(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";


        //- id
        //- name
        //- email
        //- type
        //  - id
        //  - name
        //  - customFields
        //      - c1
        //      - c2
        //      - c3
        //- externalId




        private static readonly Dictionary<char, char> _parenthesisPairs = new()
        {
            { '(', ')' },
        };



        /// <summary>
        /// Created On 8/28/2022
        /// Description: Prints to console as it reads the string.  Then, parses the string into a dictionary and prints to console alphabetical
        /// Could have parsed the string into a standard dictionary, but decided to switch things up and parse it a different way.  Because?  Why not!
        /// </summary>
        /// <param name="puzzleString"></param>
        public static void PrintAsItReads(string puzzleString)
        {
            if (string.IsNullOrEmpty(puzzleString))
            {
                Console.WriteLine("nice try");
            }

            List<string> tabs = new();

            StringBuilder stringBuilder = new();
            puzzleString = puzzleString.Replace(" ", "");

            for (int i = 0; i < puzzleString.Length; i++)
            {
                char thisChar = puzzleString[i];

                if (_parenthesisPairs.ContainsKey(thisChar) || thisChar == ',')
                {
                    if (_parenthesisPairs.ContainsKey(thisChar) && i > 0)
                    {
                        tabs.Add("\t");
                    }

                    stringBuilder.Append('\n');
                    stringBuilder.Append(string.Join("", tabs));
                    stringBuilder.Append("- ");
                    continue;
                }

                if (_parenthesisPairs.Values.Contains(thisChar) && tabs.Count > 0)
                {
                    tabs.RemoveAt(0);
                    continue;
                }
                else if(!_parenthesisPairs.Values.Contains(thisChar))
                {
                    stringBuilder.Append(thisChar);
                }

                
            }
            stringBuilder.Append('\n');

            Console.WriteLine(stringBuilder.ToString());
        }



        /// <summary>
        /// Created On 8/28/2022
        /// Description: Builds a SortedDictionary from the string and then print the alphabetically sorted keys
        /// </summary>
        /// <param name="puzzleString"></param>
        public static void PrintAlphabetical(string puzzleString)
        {
            puzzleString = puzzleString.Replace(" ", "");

            SortedDictionary<string, object> parsedPuzzle = ConvertStringToDictionary(puzzleString, 0, out int stepCount);

            WriteDictionaryContents(parsedPuzzle, string.Empty);

        }

        /// <summary>
        /// Created By: MMutek
        /// Created On 8/28/2022
        /// Description: Recursive method to tease apart the string into a dictionary of string and object
        /// </summary>
        /// <param name="puzzleString">Completed string, expects RedRover puzzle format</param>
        /// <param name="startIndex">Recursive parameter, used to keep track of where we are in the string</param>
        /// <param name="stepsTaken">Recursive parameter, returns number of iterations made before returning</param>
        /// <returns></returns>
        private static SortedDictionary<string, object> ConvertStringToDictionary(string puzzleString, int startIndex, out int stepsTaken)
        {
            string thisKey = string.Empty;
            char separater = ',';

            SortedDictionary<string, object> subDictionary = new();

            SortedDictionary<string, object> result = new SortedDictionary<string, object>();

            stepsTaken = 0;

            for (int i = startIndex; i < puzzleString.Length; i++)
            {
                char thisChar = puzzleString[i];
                
                if(string.IsNullOrEmpty(thisKey) && _parenthesisPairs.ContainsKey(thisChar))
                {
                    continue;
                }

                stepsTaken++;

                if (thisChar.Equals(separater))
                {
                    result.Add(thisKey, subDictionary);
                    thisKey = String.Empty;
                    subDictionary = new SortedDictionary<string, object>();

                    continue;
                }

                else if (_parenthesisPairs.ContainsKey(thisChar))
                {
                    subDictionary = ConvertStringToDictionary(puzzleString, i, out int outIndex);

                    stepsTaken += outIndex;
                    i += outIndex;

                    continue;
                }

                else if (_parenthesisPairs.Values.Contains(thisChar))
                {
                    result.Add(thisKey, subDictionary);
                    break;
                }

                else
                {
                    thisKey = $"{thisKey}{thisChar}";
                }
            }


            return result;
        }


        /// <summary>
        /// Created By: MMutek
        /// Created On 8/28/2022
        /// Description: Recursive method to iterate through the dictionary and writes all key's to console
        /// </summary>
        /// <param name="parsedPuzzle"></param>
        /// <param name="indent">Formatting parameter used to keep track how far to indent</param>
        private static void WriteDictionaryContents (SortedDictionary<string, object> parsedPuzzle, string indent)
        {
            Console.WriteLine();
            foreach(var item in parsedPuzzle)
            {
                Console.WriteLine($"{indent}- {item.Key}");

                SortedDictionary<string, object> itemValue = (SortedDictionary<string, object>)item.Value;
                if(itemValue.Count > 0)
                {
                    string updatedIndent = $"{indent}\t";
                    WriteDictionaryContents(itemValue, updatedIndent);
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Created By: MMutek
        /// Created On 8/28/2022
        /// Description: validates input is properly formatted
        /// </summary>
        /// <param name="puzzleString"></param>
        /// <returns></returns>
        public static bool IsValidFormat(string puzzleString)
        {
            //no data, then sure, it counts
            if (puzzleString.Count() == 0)
            {
                return true;
            }

            Stack<char> parenthesisStack = new();

            foreach(char character in puzzleString)
            {
                if (_parenthesisPairs.ContainsKey(character))
                {
                    parenthesisStack.Push(character);
                }
                else if (_parenthesisPairs.Values.Contains(character))
                {
                    if (parenthesisStack.Count() == 0)
                    {
                        return false;
                    }

                    char openingParenthesis = parenthesisStack.Pop();
                    //If it is not the last opening parenthesis, it fails validation
                    if (_parenthesisPairs[openingParenthesis] != character)
                    {
                        return false;
                    }
                }
            }

            return parenthesisStack.Count() == 0;
        }


    }
}
