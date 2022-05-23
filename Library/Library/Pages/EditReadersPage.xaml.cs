using Library.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Pages
{
    public partial class EditReadersPage : Page
    {
        public Reader currentReader = new Reader();
        public EditReadersPage(Reader selectedReader)
        {
            InitializeComponent();

            if (selectedReader != null)
            {
                currentReader = selectedReader;
            }

            DataContext = currentReader;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentReader.Фамилия))
                errors.AppendLine("Заполните поле Фамилия");
            if (string.IsNullOrWhiteSpace(currentReader.Имя))
                errors.AppendLine("Заполните поле Имя");
            if (string.IsNullOrWhiteSpace(currentReader.Отчество))
                errors.AppendLine("Заполните поле Отчество");
            if (string.IsNullOrWhiteSpace(currentReader.Телефон))
                errors.AppendLine("Заполните поле Телефон");
            if (string.IsNullOrWhiteSpace(currentReader.Паспорт))
                errors.AppendLine("Заполните поле Паспорт");
            if (string.IsNullOrWhiteSpace(currentReader.Адрес))
                errors.AppendLine("Заполните поле Адрес");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentReader.Код_читателя == 0)
            {
                LiteraryPleasureEntities.GetContext().Readers.Add(currentReader);
            }

            try
            {
                LiteraryPleasureEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
