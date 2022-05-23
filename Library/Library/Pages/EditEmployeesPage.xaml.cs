using Library.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class EditEmployeesPage : Page
    {
        private static Employee currentEmployee = new Employee();
        public EditEmployeesPage(Employee selectedEmployee)
        {
            InitializeComponent();

            if (selectedEmployee != null)
            {
                currentEmployee = selectedEmployee;
            }

            DataContext = currentEmployee;
            cBoxPosition.ItemsSource = LiteraryPleasureEntities.GetContext().Positions.ToList();
        }

        private void UploadImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.InitialDirectory = @"C:\Users\Уфимцев Олег\source\repos\Library\Library\Icons\";
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                DataContext = currentEmployee;
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

                using (LiteraryPleasureEntities imageEntities = new LiteraryPleasureEntities())
                {
                    currentEmployee.Картинка = new FileInfo(op.FileName).Name;
                    imgName.Text = new FileInfo(op.FileName).Name;
                    currentEmployee.Изображение = File.ReadAllBytes(op.FileName);
                }
            }
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (currentEmployee.Position == null)
                errors.AppendLine("Заполните поле Должность");
            if (string.IsNullOrWhiteSpace(currentEmployee.Фамилия))
                errors.AppendLine("Заполните поле Фамилия");
            if (string.IsNullOrWhiteSpace(currentEmployee.Имя))
                errors.AppendLine("Заполните поле Имя");
            if (string.IsNullOrWhiteSpace(currentEmployee.Отчество))
                errors.AppendLine("Заполните поле Отчество");
            if (string.IsNullOrWhiteSpace(currentEmployee.Телефон))
                errors.AppendLine("Заполните поле Телефон");
            if (string.IsNullOrWhiteSpace(currentEmployee.Пароль))
                errors.AppendLine("Заполните поле Пароль");

            currentEmployee.Дата_регистрации = DateTime.Now;

            if (errors.Length > 0) // Если есть ошибка, то выводит её
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentEmployee.Код_сотрудника == 0) // Если всё хорошо, то выводит экземпляр данного сотрудника
            {
                LiteraryPleasureEntities.GetContext().Employees.Add(currentEmployee);
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
