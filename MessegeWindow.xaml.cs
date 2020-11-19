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

namespace Course_work_doc_lib
{
    /// <summary>
    /// Пространство для обработки ошибок
    /// </summary>
    public partial class MessegeWindow : Window
    {
        public string valueReturn = "";
        public MessegeWindow(string messageTitle, string messgeBody)
        {
            InitializeComponent();
            TextBlock_MessageTitle.Text = messageTitle;
            TextBlock_MessageBody.Text = messgeBody;
            TextBlock_MessageErrorsId.IsEnabled = false;
        }

        public MessegeWindow(string messageTitle, string messgeBody, string messageErrorId)
        {
            InitializeComponent();
            TextBlock_MessageTitle.Text = messageTitle;
            TextBlock_MessageBody.Text = messgeBody;
            TextBlock_MessageErrorsId.Text = messageErrorId;
        }

        public MessegeWindow(string messageTitle, string messgeBody, bool itsTextInputWindow)
        {
            InitializeComponent();
            TextBlock_MessageTitle.Text = messageTitle;
            TextBlock_MessageBody.Text = messgeBody;
            TextBlock_MessageErrorsId.IsEnabled = false;
            TextBox_ReturnValue.Visibility = Visibility.Visible;
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Click_Ok(object sender, RoutedEventArgs e)
        {
            valueReturn = TextBox_ReturnValue.Text;
            Application.Current.Properties["lastReturnValue"] = valueReturn;
            this.Close();
        }
    }
}
