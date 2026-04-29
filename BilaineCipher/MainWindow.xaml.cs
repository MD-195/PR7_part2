using BilaineCipherApp;
using System;
using System.Windows;

namespace BilaineCipherApp
{
    /// <summary>
    /// Окно приложения для шифрования/дешифрования методом Рельсовой изгороди
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BilainCipher _cipher;

        public MainWindow()
        {
            InitializeComponent();
            _cipher = new BilainCipher();
        }

        /// <summary>
        /// Обработчик кнопки "Зашифровать"
        /// </summary>
        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = txtInput.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    ShowError("Введите текст для шифрования");
                    return;
                }

                if (!ValidateRails(out int rails))
                    return;

                string result = _cipher.Encrypt(text, rails);
                txtResult.Text = result;
                statusMessage.Content = $"✓ Шифрование выполнено успешно. Рельсов: {rails}";
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Расшифровать"
        /// </summary>
        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cipherText = txtInput.Text;
                if (string.IsNullOrWhiteSpace(cipherText))
                {
                    ShowError("Введите текст для расшифрования");
                    return;
                }

                if (!ValidateRails(out int rails))
                    return;

                string result = _cipher.Decrypt(cipherText, rails);
                txtResult.Text = result;
                statusMessage.Content = $"✓ Расшифрование выполнено успешно. Рельсов: {rails}";
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Очистить"
        /// </summary>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtResult.Clear();
            txtRails.Text = "3";
            statusMessage.Content = "Готов к работе";
        }

        /// <summary>
        /// Проверка корректности количества рельсов
        /// </summary>
        private bool ValidateRails(out int rails)
        {
            if (!int.TryParse(txtRails.Text, out rails))
            {
                ShowError("Количество рельсов должно быть целым числом");
                return false;
            }

            if (rails < 2 || rails > 10)
            {
                ShowError("Количество рельсов должно быть от 2 до 10");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Отображение ошибки
        /// </summary>
        private void ShowError(string message)
        {
            statusMessage.Content = $"✗ {message}";
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}