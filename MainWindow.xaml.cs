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
using Course_work_doc_lib.Model;

namespace Course_work_doc_lib
{
    /// <summary>
    /// Это гланое окно - через него осуществляется функций авторизаций и регистраций
    /// </summary>
    public partial class MainWindow : Window
    {
        User thisBufferUser = new User(); // Буфферный пользователь для добавления
        #region
        public MainWindow()
        {
            InitializeComponent();
        } //Конструктор окна

        private bool Authorization(User introducedUser)
        {
            try
            {
                using (ApplicationContext dbContext = new ApplicationContext())
                {
                    var buffDB = dbContext.Users.ToList();
                    foreach (User allUser in buffDB)
                    {
                        if (allUser.Login == introducedUser.Login && allUser.Password == introducedUser.Password)
                        {
                            Application.Current.Properties["thisAuthorizedUserId"] = allUser.ID;
                            Application.Current.Properties["thisAuthorizedUserName"] = allUser.Login.ToString();
                            return true;
                        }
                    }
                    MessegeWindow messageWindow1 = new MessegeWindow("Ошибка", "Веденные данные не верны!");
                    messageWindow1.ShowDialog();
                    return false;
                }
            }
            catch (Exception e)
            {
                MessegeWindow messageWindow2 = new MessegeWindow("Ошибка", "Ошибка при авторизаций! ");
                messageWindow2.ShowDialog();
                return false;
            }
        } // Функция авторизации

        private void Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Событие закрытия

        private void Click_OK(object sender, RoutedEventArgs e)
        {
            thisBufferUser.Login = TextBox_Login.Text;
            thisBufferUser.Password = TextBox_Password.Password;
            if (Authorization(thisBufferUser) == true)
            {
                ViewMainWindow viewMainWindow = new ViewMainWindow();
                this.Close();
                viewMainWindow.Show();
            }
        } //Событие отправки

        private void Click_Regi(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            this.Close();
        } //Событие открытия регистраций
        #endregion
        // Методы и события страницы
    }
}
