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
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();

            var allPosition = LiteraryPleasureEntities.GetContext().Positions.ToList();
            allPosition.Insert(0, new Position
            {
                Наименование = "Все должности"
            });

            CBoxSortEmployees.ItemsSource = allPosition;
            CBoxSortEmployees.SelectedIndex = 0;
            EmployeesSearch();
        }

        private void EmployeesSearch()
        {
            var currentEmployees = LiteraryPleasureEntities.GetContext().Employees.ToList();

            // Сортировка по типу
            if (CBoxSortEmployees.SelectedIndex > 0)
            {
                currentEmployees = currentEmployees.Where(p => p.Код_должности == CBoxSortEmployees.SelectedIndex).ToList();
            }

            // Поиск по фамилии
            currentEmployees = currentEmployees.Where(p => p.Фамилия.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            employeesLV.ItemsSource = currentEmployees.OrderBy(p => p.Фамилия).ToList();
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditEmployeesPage((sender as Button).DataContext as Employee));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LiteraryPleasureEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                employeesLV.ItemsSource = LiteraryPleasureEntities.GetContext().Employees.ToList();
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmployeesSearch();
        }

        private void CBoxSortEmployeesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmployeesSearch();
        }

        private void AddEmployeesClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditEmployeesPage(null)); // Переход на страницу добавления нового сотрудника
        }

        private void DeleteEmployeesClick(object sender, RoutedEventArgs e)
        {
            var employeeRemoving = employeesLV.SelectedItems.Cast<Employee>().ToList(); // Получаем список сотрудников для удаления

            if (MessageBox.Show($"Вы точно хотите удалить следующие {employeeRemoving.Count()} элементов", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    LiteraryPleasureEntities.GetContext().Employees.RemoveRange(employeeRemoving);
                    LiteraryPleasureEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    employeesLV.ItemsSource = LiteraryPleasureEntities.GetContext().Employees.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
