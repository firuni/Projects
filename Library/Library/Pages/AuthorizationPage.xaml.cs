using Library.Classes;
using Library.Pages;
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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private Employee currentEmployee = new Employee();
        public AuthorizationPage()
        {
            InitializeComponent();
            cBoxLogine.ItemsSource = LiteraryPleasureEntities.GetContext().Employees.ToList();
        }

        private void BtnEnterClick(object sender, RoutedEventArgs e)
        {
            if (cBoxLogine.Text.Length > 0 || pBoxPassword.Password.Length > 0)
            {
                using (LiteraryPleasureEntities BS = new LiteraryPleasureEntities())
                {
                    currentEmployee = BS.Employees.Where(p => p.Фамилия == cBoxLogine.Text && p.Пароль == pBoxPassword.Password).FirstOrDefault();

                    if (currentEmployee != null)
                    {
                        switch (currentEmployee.Код_должности.ToString())
                        {
                            case "1":
                                Manager.MainFrame.Navigate(new DirectorPage(currentEmployee));
                                break;

                            case "2":
                                Manager.MainFrame.Navigate(new ManagerPage(currentEmployee));
                                break;

                            case "3":
                                Manager.MainFrame.Navigate(new ConsultantPage(currentEmployee));
                                break;
                        }
                    }

                    else
                    {
                        MessageBox.Show("Ничего не получилось! Введен не правильно логин или пароль");
                    }
                }
            }

            else
            {
                MessageBox.Show("Заполните поля Логин и Пароль");
            }
        }
    }
}
