using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringLenghtCounterBot.Services
{
    /// <summary>
    /// Объект, отвечающий за разбор входяших строк в зависимости от указанной операции
    /// </summary>
    internal class StringProcessor : IStringProcessor
    {
        /// <summary>
        /// Обработка сообщения в зависимости от кода операции
        /// </summary>
        /// <param name="operationCode">Код операции</param>
        /// <param name="inputString">Исходная строка</param>
        /// <returns></returns>
        public string ProcessString(string operationCode, string inputString)
        {
            switch (operationCode)
            {
                case "sum":
                    return TryToSum(inputString);
                case "cnt":
                    return GetLength(inputString);
                default:
                    return "Выберите действие в меню";
            }
        }

        /// <summary>
        /// Выполнение попытки суммирования
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string TryToSum(string text)
        {
            bool containsLetters = new Regex(@"[^0-9,^\s]").IsMatch(text);

            if (!containsLetters && !string.IsNullOrEmpty(text))
            {
                long tempValue = 0;

                string[] temp = text.Split(' ');

                for (int i = 0; i < temp.Length; i++)
                {
                    tempValue += (long)Convert.ToDouble(temp[i]);
                }
                return "Сумма чисел равна " + tempValue.ToString();
            }
            else
            {
                return "Введите натуральные числа через пробел";
            }
        }

        /// <summary>
        /// Подсчет символов в строке
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetLength(string text) => "Количество символов в строке " + text.Length.ToString();

    }
}
