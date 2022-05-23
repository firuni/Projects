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
    public partial class RealizationPage : Page
    {
        public RealizationPage()
        {
            InitializeComponent();
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditRealizationPage((sender as Button).DataContext as Realization));
        }

        private void AddRealizationClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditRealizationPage(null));
        }

        private void DeleteRealizationClick(object sender, RoutedEventArgs e)
        {
            var realizationRemoving = dGridRealization.SelectedItems.Cast<Realization>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {realizationRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    LiteraryPleasureEntities.GetContext().Realizations.RemoveRange(realizationRemoving);
                    LiteraryPleasureEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    dGridRealization.ItemsSource = LiteraryPleasureEntities.GetContext().Realizations.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var currentRealization = LiteraryPleasureEntities.GetContext().Realizations.ToList();
            var tbx = sender as TextBox;

            if (tbx.Text != "")
            {
                currentRealization = currentRealization.Where(p => p.Reader.Фамилия.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
                dGridRealization.ItemsSource = currentRealization.OrderBy(p => p.Reader.Фамилия).ToList();
            }
            else
            {
                dGridRealization.ItemsSource = LiteraryPleasureEntities.GetContext().Realizations.ToList();
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LiteraryPleasureEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                dGridRealization.ItemsSource = LiteraryPleasureEntities.GetContext().Realizations.ToList();
            }
        }
    }
}
