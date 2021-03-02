using System;
using System.Collections.Generic;
using System.Text;

namespace Words
{
    class Program
    {
        static string MainWord;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            GameCycle();
        }


        //игровой цикл, после завершения игры можно начать заново
        static void GameCycle()
        {
            MainWord = begin();
            game(1); //игра начинается с первого игрока

            Console.WriteLine("Нажмите 1, чтобы начать заново, нажмите любую клавишу для выхода из игры");

            switch (Console.ReadLine())
            {
                case "1":
                    GameCycle();
                    break;
                default:
                    break;
            }

        }


        //ввод игрового слова, при неправильном вводе появляется соответсвующее сообщение и предлагается ввести слово заново
        static string begin()
        {
            Console.Write("Введите слово русскими буквами, от 8 до 30 символов: ");
            string word = Console.ReadLine();
            if (!valid(word, 1))
            {
                Console.WriteLine("Неверный формат!");
                return begin();
            }
            else return word;
        }


        //проверка игрового слова, оно должно состоять только из букв латинского алфавита и иметь длину в указанном диапазоне
        static bool valid(string str, int len)
        {
            if (str == String.Empty)
                return false;

            char letter = str[0];

            if ((letter >= 'А' && letter <= 'Я') || (letter >= 'а' && letter <= 'я'))
            {
                if (str.Length == 1) //если последний просмотренный символ верный и слово нужной длины, то слово подходит
                    if (len >= 8 && len <= 30)
                        return true;
                    else
                        return false;

                //слово постепенно укорачивается, пока не останется одна буква, также вычисляется итоговая длина слова
                return valid(str.Substring(1), len + 1);
            }
            else
                return false;
        }


        //игровой процесс, игроки поочередно вводят слова по установленным правилам, при проигрыше одного из них игра заканчивается
        static void game(int n)
        {
            Console.Write($"Игрок{n}: ");
            string InputWord = Console.ReadLine();
            if (check(InputWord, MainWord))
                if (n == 2)  //смена игроков
                    game(1);
                else game(2);
            else
            {
                Console.WriteLine($"Игрок{n} проиграл.");
                return;
            }
        }


        //проверка введенного игроком слова, оно должно включать в себя только буквы игрового слова
        //буква не может повторяться большее число раз, чем в исходном игровом слове 
        static bool check(string InputWord, string mainword)
        {
            if (InputWord.Length == 0 || !mainword.Contains(InputWord[0]))
                return false;
            else
            {
                if (InputWord.Length == 1)
                    return true;
                else
                {
                    int index = mainword.IndexOf(InputWord[0]);
                    return check(InputWord.Substring(1), mainword.Remove(index, 1));
                }
            }
        }


    }
}
