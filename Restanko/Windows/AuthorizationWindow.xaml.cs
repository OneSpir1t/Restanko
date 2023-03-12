using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Restanko.Entities;
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
using System.Windows.Shapes;

namespace Restanko.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private User User { get; set; }

        public AuthorizationWindow()
        {
            InitializeComponent();
            RestankoContext.restankoContext.Database.OpenConnection();
        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(Login_TextBox.Text) && !string.IsNullOrEmpty(Password_TextBox.Text))
            {

                User = RestankoContext.restankoContext.Users.FirstOrDefault(u => u.Login == Login_TextBox.Text && u.Password == Password_TextBox.Text);
                if (User != null)
                {
                    if(User.Role.Id == 1)
                    {
                        GoToNextWindow();
                        MessageBox.Show("Вы вошли как Администратор");
                    }
                    if (User.Role.Id == 2)
                    {
                        GoToNextWindow();
                        MessageBox.Show("Вы вошли как Менеджер", "Уведомление");
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не найден", "Уведомление");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление");
            }
        }

        private void GoToNextWindow()
        {
            Login_TextBox.Focus();
            Hide();
            Password_TextBox.Text = default;
            Login_TextBox.Text = default;
            var mw = new UserWindow(User);
            mw.Owner = this;
            mw.Show();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login_TextBox.Focus();
        }

        private void Login_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Login_TextBox.IsFocused)
            {
                if (!string.IsNullOrEmpty(Login_TextBox.Text))
                {
                    Password_TextBox.Focus();
                }
                else
                {
                    MessageBox.Show("Введите логин!", "Уведомление");
                }
            }
        }

        private void Password_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrEmpty(Password_TextBox.Text))
                {
                    LogIn_Button_Click(sender, e);
                }
                else
                {
                    if (string.IsNullOrEmpty(Login_TextBox.Text))
                    {
                        Login_TextBox.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Введите пароль!", "Уведомление");
                    }
                }
            }
        }

        private void Password_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Login_TextBox.Text))
            {
                Login_TextBox.Focus();
            }
        }
    }
}
