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
    public partial class ReadersPage : Page
    {
        public ReadersPage()
        {
            InitializeComponent();
        }

        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var currentReader = LiteraryPleasureEntities.GetContext().Readers.ToList();
            var tbx = sender as TextBox;

            if (tbx.Text != "")
            {
                currentReader = currentReader.Where(p => p.Фамилия.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
                dGridReaders.ItemsSource = currentReader.OrderBy(p => p.Фамилия).ToList();
            }
            else
            {
                dGridReaders.ItemsSource = LiteraryPleasureEntities.GetContext().Readers.ToList();
            }
        }

        private void AddReadersClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditReadersPage(null));
        }

        private void DeleteReadersClick(object sender, RoutedEventArgs e)
        {
            var readerRemoving = dGridReaders.SelectedItems.Cast<Reader>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {readerRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    LiteraryPleasureEntities.GetContext().Readers.RemoveRange(readerRemoving);
                    LiteraryPleasureEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    dGridReaders.ItemsSource = LiteraryPleasureEntities.GetContext().Readers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditReadersPage((sender as Button).DataContext as Reader));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LiteraryPleasureEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                dGridReaders.ItemsSource = LiteraryPleasureEntities.GetContext().Readers.ToList();
            }
        }
    }
}
