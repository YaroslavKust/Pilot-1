using System;
using System.Text;

namespace Words
{
    class Program
    {
        /// <summary>
        /// game word
        /// </summary>
        static string mainWord;


        /// <summary>
        /// errors that may occur when validating the game word
        /// </summary>
        enum ValidationErrors
        {
            /// <summary>word is valid</summary>
            Valid,

            /// <summary> word is short</summary>
            TooShort,

            /// <summary>word is long</summary>
            TooLong,

            /// <summary> there are wrong symbols in the word</summary>
            WrongSymbol,

            /// <summary>word is empty</summary>
            Empty
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            GameCycle();
        }


       /// <summary>
       /// game cycle, after completing the game, you can start again
       /// </summary>
        static void GameCycle()
        {
            mainWord = EnterWord();
            StartGame(1); //the game starts from first player

            Console.WriteLine("Нажмите 1 для перезапуска игры, нажмите любую клавишу для выхода");

            switch (Console.ReadLine())
            {
                case "1":
                    GameCycle();
                    break;
                default:
                    break;
            }

        }


        ///<summary>
        ///entering a game word, if you enter it incorrectly, a corresponding message appears 
        ///and you are prompted to enter the word again
        ///</summary>
        static string EnterWord()
        {
            Console.Write("Введите слово, от 8 до 30 букв: ");
            string word = Console.ReadLine();

            switch (Validate(word, 1))
            {
                case ValidationErrors.Valid:
                    return word;

                case ValidationErrors.Empty:
                    Console.WriteLine("Введена пустая строка! Попробуйте снова.");
                    return EnterWord();

                case ValidationErrors.TooShort:
                    Console.WriteLine("Слово слишком короткое! Попробуйте снова.");
                    return EnterWord();

                case ValidationErrors.TooLong:
                    Console.WriteLine("Слово слишком длинное! Попробуйте снова.");
                    return EnterWord();

                case ValidationErrors.WrongSymbol:
                    Console.WriteLine("Неверные символы! Попробуйте снова.");
                    return EnterWord();

                default:
                    Console.WriteLine("Неизвестная ошибка! Попробуйте снова.");
                    return EnterWord();
            }
        }


        /// <summary>
        /// checking the game word, it must consist only of letters and have a length in the specified range
        /// </summary>
        /// <param name="inputString">input word</param>
        /// <param name="len">length of the word</param>
        static ValidationErrors Validate(string inputString, int len)
        {
            if (inputString == String.Empty)
                return ValidationErrors.Empty;

            char letter = inputString[0];
            
            if (Char.IsLetter(letter))
            {
                //if the last viewed character is correct and the word is the correct length, then the word is valid
                if (inputString.Length == 1)
                    if (len < 8)
                    {
                        return ValidationErrors.TooShort;
                    }
                    else if (len > 30)
                    {
                        return ValidationErrors.TooLong;
                    }
                    else 
                        return ValidationErrors.Valid;

               //the word is gradually shortened until there is only one letter left 
               //the total length of the word is also calculated
                return Validate(inputString.Substring(1), len + 1);
            }
            else
            {
                return ValidationErrors.WrongSymbol;
            }
        }


        /// <summary>
        /// gameplay, players alternately enter words according to the established rules, 
        /// if you lose one of them, the game ends
        /// </summary>
        /// <param name="playerNumber">number of active player</param>
        static void StartGame(int playerNumber)
        {
            Console.Write($"Игрок{playerNumber}: ");
            string inputWord = Console.ReadLine();
            if (Check(inputWord, mainWord))
                if (playerNumber == 2)  //change player
                    StartGame(1);
                else StartGame(2);
            else
            {
                Console.WriteLine($"Игрок{playerNumber} проиграл.");
                return;
            }
        }


        /// <summary>
        /// checking the word entered by the player, it must include only the letters of the game word
        /// the letter cannot be repeated more times than in the original game word
        /// </summary>
        /// <param name="inputWord"> word that player entered</param>
        /// <param name="gameWord">game word</param> 
        static bool Check(string inputWord, string gameWord)
        {
            if (inputWord.Length == 0 || !gameWord.Contains(inputWord[0]))
                return false;
            else
            {
                if (inputWord.Length == 1)
                    return true;
                else
                {
                    int index = gameWord.IndexOf(inputWord[0]);
                    return Check(inputWord.Substring(1), gameWord.Remove(index, 1));
                }
            }
        }


    }
}
