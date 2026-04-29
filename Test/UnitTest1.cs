using BilaineCipherApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BilainCipherTests
{
    [TestClass]
    public class BilainCipherTests
    {
        private BilainCipher _cipher;

        [TestInitialize]
        public void Setup()
        {
            _cipher = new BilainCipher();
        }

        /// <summary>
        /// Тест шифрования: HELLO с 3 рельсами
        /// Правильное распределение:
        /// H . . . O
        /// . E . L .
        /// . . L . .
        /// Результат: H O E L L -> "HOELL"
        /// </summary>
        [TestMethod]
        public void TestEncryptEnglish3Rails()
        {
            string result = _cipher.Encrypt("HELLO", 3);
            Assert.AreEqual("HOELL", result); 
        }

        /// <summary>
        /// Тест дешифрования: "HOELL" с 3 рельсами -> "HELLO"
        /// </summary>
        [TestMethod]
        public void TestDecryptEnglish3Rails()
        {
            string result = _cipher.Decrypt("HOELL", 3);
            Assert.AreEqual("HELLO", result);
        }

        /// <summary>
        /// Тест шифрования: HELLO с 2 рельсами
        /// Распределение:
        /// H . L . O
        /// . E . L .
        /// Результат: "HLOEL"
        /// </summary>
        [TestMethod]
        public void TestEncryptEnglish2Rails()
        {
            string result = _cipher.Encrypt("HELLO", 2);
            Assert.AreEqual("HLOEL", result);
        }

        /// <summary>
        /// Тест шифрования русского текста с 2 рельсами
        /// ПРИВЕТ с 2 рельсами:
        /// П . И . Е
        /// . Р . В . Т
        /// Результат: "ПИЕРВТ"
        /// </summary>
        [TestMethod]
        public void TestEncryptRussian2Rails()
        {
            string result = _cipher.Encrypt("ПРИВЕТ", 2);
            Assert.AreEqual("ПИЕРВТ", result);
        }

        /// <summary>
        /// Тест дешифрования русского текста
        /// </summary>
        [TestMethod]
        public void TestDecryptRussian2Rails()
        {
            string result = _cipher.Decrypt("ПИЕРВТ", 2);
            Assert.AreEqual("ПРИВЕТ", result);
        }

        /// <summary>
        /// Тест обратимости шифрования
        /// </summary>
        [TestMethod]
        public void TestEncryptDecryptRoundTrip()
        {
            string original = "HELLO WORLD";
            int rails = 3;
            string encrypted = _cipher.Encrypt(original, rails);
            string decrypted = _cipher.Decrypt(encrypted, rails);
            Assert.AreEqual(original, decrypted);
        }

        /// <summary>
        /// Тест с фразой из примера на dencode.com
        /// Исходный текст: "THIS_IS_A_SECRET_MESSAGE", рельсы = 4
        /// Ожидаемый результат: "TSCEHI_ERMSEI_ASE_SGS_TA"
        /// </summary>
        [TestMethod]
        public void TestEncryptWithDencodeExample()
        {
            string original = "THIS_IS_A_SECRET_MESSAGE";
            int rails = 4;
            string encrypted = _cipher.Encrypt(original, rails);
            Assert.AreEqual("TSCEHI_ERMSEI_ASE_SGS_TA", encrypted);
        }

        /// <summary>
        /// Тест дешифрования с фразой из примера на dencode.com
        /// </summary>
        [TestMethod]
        public void TestDecryptWithDencodeExample()
        {
            string cipherText = "TSCEHI_ERMSEI_ASE_SGS_TA";
            int rails = 4;
            string decrypted = _cipher.Decrypt(cipherText, rails);
            Assert.AreEqual("THIS_IS_A_SECRET_MESSAGE", decrypted);
        }

        /// <summary>
        /// Тест: количество рельсов равно длине текста
        /// </summary>
        [TestMethod]
        public void TestRailsEqualToLength()
        {
            string result = _cipher.Encrypt("ABC", 3);
            Assert.AreEqual("ABC", result);
        }

        /// <summary>
        /// Тест: количество рельсов больше длины текста
        /// </summary>
        [TestMethod]
        public void TestRailsGreaterThanLength()
        {
            string result = _cipher.Encrypt("AB", 5);
            Assert.AreEqual("AB", result);
        }

        /// <summary>
        /// Тест: пустая строка
        /// </summary>
        [TestMethod]
        public void TestEmptyString()
        {
            string result = _cipher.Encrypt("", 3);
            Assert.AreEqual("", result);
            string resultDecrypt = _cipher.Decrypt("", 3);
            Assert.AreEqual("", resultDecrypt);
        }

        /// <summary>
        /// Тест: null входное значение
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullInputEncrypt()
        {
            _cipher.Encrypt(null, 3);
        }

        /// <summary>
        /// Тест: null входное значение для дешифрования
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullInputDecrypt()
        {
            _cipher.Decrypt(null, 3);
        }

        /// <summary>
        /// Тест: некорректное количество рельсов (1)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidRailsEncrypt()
        {
            _cipher.Encrypt("HELLO", 1);
        }

        /// <summary>
        /// Тест: сохранение пробелов (полный цикл)
        /// </summary>
        [TestMethod]
        public void TestPreserveSpaces()
        {
            string original = "HELLO WORLD";
            int rails = 3;
            string decrypted = _cipher.Decrypt(_cipher.Encrypt(original, rails), rails);
            Assert.AreEqual(original, decrypted);
        }

        /// <summary>
        /// Тест: сохранение знаков препинания
        /// </summary>
        [TestMethod]
        public void TestPreservePunctuation()
        {
            string original = "HELLO, WORLD!";
            int rails = 3;
            string decrypted = _cipher.Decrypt(_cipher.Encrypt(original, rails), rails);
            Assert.AreEqual(original, decrypted);
        }

        /// <summary>
        /// Тест: смешанный русский и английский текст
        /// </summary>
        [TestMethod]
        public void TestMixedLanguages()
        {
            string original = "HelloПривет";
            int rails = 3;
            string decrypted = _cipher.Decrypt(_cipher.Encrypt(original, rails), rails);
            Assert.AreEqual(original, decrypted);
        }

        /// <summary>
        /// Тест: цифры в тексте
        /// </summary>
        [TestMethod]
        public void TestNumbers()
        {
            string original = "ABC123DEF";
            int rails = 3;
            string decrypted = _cipher.Decrypt(_cipher.Encrypt(original, rails), rails);
            Assert.AreEqual(original, decrypted);
        }

        /// <summary>
        /// Тест: длинный текст с большим количеством рельсов
        /// </summary>
        [TestMethod]
        public void TestLongTextWithManyRails()
        {
            string original = "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG";
            int rails = 5;
            string decrypted = _cipher.Decrypt(_cipher.Encrypt(original, rails), rails);
            Assert.AreEqual(original, decrypted);
        }
    }
}