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
    public partial class RegWindow : Window
    {
        User thisBufferUser = new User();
        public RegWindow()
        {
            InitializeComponent();
        }

        private void Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Click_OK(object sender, RoutedEventArgs e)
        {
            thisBufferUser.Login = TextBox_Login.Text;
            thisBufferUser.Password = TextBox_Password.Password;
            try
            {
                using (ApplicationContext DbContext = new ApplicationContext())
                {
                    DbContext.Users.Add(thisBufferUser);
                    DbContext.SaveChanges();
                }

                MainWindow viewMainWindow = new MainWindow();
                this.Close();
                viewMainWindow.Show();
            }
            catch (Exception exeptionAddUser)
            {
                MessegeWindow messegeWindow = new MessegeWindow("Ошибка!", "Не удалось добавить пользователя: " + exeptionAddUser.Message);
                messegeWindow.ShowDialog();
            }
            finally
            {
                this.Close();
            }

        }

        private void Click_Regi(object sender, RoutedEventArgs e)
        {

        }
    }
}
