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
    public partial class EditRealizationPage : Page
    {
        public Realization currentRealization = new Realization();

        public EditRealizationPage(Realization selectedRealization)
        {
            InitializeComponent();

            if (selectedRealization != null)
            {
                currentRealization = selectedRealization;
            }

            DataContext = currentRealization;
            cBoxBooks.ItemsSource = LiteraryPleasureEntities.GetContext().Books.ToList();
            cBoxReaders.ItemsSource = LiteraryPleasureEntities.GetContext().Readers.ToList();
            cBoxEmployees.ItemsSource = LiteraryPleasureEntities.GetContext().Employees.ToList();
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (currentRealization.Book == null)
                errors.AppendLine("Заполните поле Книга");
            if (currentRealization.Reader == null)
                errors.AppendLine("Заполните поле Читатель");
            if (currentRealization.Employee == null)
                errors.AppendLine("Заполните поле Сотрудник");
            if (currentRealization.Дата_выдачи == null)
                errors.AppendLine("Заполните поле Дата выдачи");
            if (currentRealization.Дата_возврата == null)
                errors.AppendLine("Заполните поле Дата возврата");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentRealization.Код_реализации == 0)
            {
                LiteraryPleasureEntities.GetContext().Realizations.Add(currentRealization);
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
