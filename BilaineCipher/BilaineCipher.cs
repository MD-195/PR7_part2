using System;
using System.Text;

namespace BilaineCipherApp
{
    /// <summary>
    /// Класс для шифрования и дешифрования методом "Рельсовая изгородь" (Rail Fence Cipher)
    /// </summary>
    public class BilainCipher
    {
        /// <summary>
        /// Шифрование текста методом зигзага (Rail Fence Cipher)
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="rails">Количество рельсов (строк)</param>
        /// <returns>Зашифрованная строка</returns>
        public string Encrypt(string text, int rails)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (rails < 2) throw new ArgumentException("Количество рельсов должно быть не менее 2", nameof(rails));
            if (string.IsNullOrEmpty(text)) return "";

            char[][] fence = new char[rails][];
            for (int i = 0; i < rails; i++)
            {
                fence[i] = new char[text.Length];
                for (int j = 0; j < text.Length; j++)
                {
                    fence[i][j] = '\0'; 
                }
            }

            int row = 0;
            int col = 0;
            bool down = true;

            for (int i = 0; i < text.Length; i++)
            {
                fence[row][col] = text[i];
                col++;

                if (down)
                {
                    if (row + 1 < rails)
                        row++;
                    else
                    {
                        down = false;
                        row--;
                    }
                }
                else
                {
                    if (row - 1 >= 0)
                        row--;
                    else
                    {
                        down = true;
                        row++;
                    }
                }
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < text.Length; j++)
                {
                    if (fence[i][j] != '\0')
                        result.Append(fence[i][j]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Дешифрование текста, зашифрованного методом зигзага (Rail Fence Cipher)
        /// </summary>
        /// <param name="cipher">Зашифрованный текст</param>
        /// <param name="rails">Количество рельсов</param>
        /// <returns>Расшифрованная строка</returns>
        public string Decrypt(string cipher, int rails)
        {
            if (cipher == null) throw new ArgumentNullException(nameof(cipher));
            if (rails < 2) throw new ArgumentException("Количество рельсов должно быть не менее 2", nameof(rails));
            if (string.IsNullOrEmpty(cipher)) return "";

            int length = cipher.Length;

            char[][] fence = new char[rails][];
            for (int i = 0; i < rails; i++)
            {
                fence[i] = new char[length];
                for (int j = 0; j < length; j++)
                {
                    fence[i][j] = '\0';
                }
            }

            int row = 0;
            int col = 0;
            bool down = true;

            for (int i = 0; i < length; i++)
            {
                if (fence[row][col] == '\0')
                    fence[row][col] = '*'; 
                col++;

                if (down)
                {
                    if (row + 1 < rails)
                        row++;
                    else
                    {
                        down = false;
                        row--;
                    }
                }
                else
                {
                    if (row - 1 >= 0)
                        row--;
                    else
                    {
                        down = true;
                        row++;
                    }
                }
            }

            int index = 0;
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (fence[i][j] == '*' && index < length)
                    {
                        fence[i][j] = cipher[index++];
                    }
                }
            }

            StringBuilder result = new StringBuilder();
            row = 0;
            col = 0;
            down = true;

            for (int i = 0; i < length; i++)
            {
                result.Append(fence[row][col]);
                col++;

                if (down)
                {
                    if (row + 1 < rails)
                        row++;
                    else
                    {
                        down = false;
                        row--;
                    }
                }
                else
                {
                    if (row - 1 >= 0)
                        row--;
                    else
                    {
                        down = true;
                        row++;
                    }
                }
            }

            return result.ToString();
        }
    }
}