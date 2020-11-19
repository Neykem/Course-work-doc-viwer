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
using System.Windows.Shapes;
using AcroPDFLib;
using Course_work_doc_lib;
using Course_work_doc_lib.Model;

namespace Course_work_doc_lib
{
    /// <summary>
    /// Главное окно для просмотра документов
    /// </summary>
    public partial class ViewMainWindow : Window
    {
        #region
        string stringURI = "C:/Users/79534/source/repos/Course-work-doc-lib/bin/Debug/lib/"; // Путь по умолчанию
        bool controlItemSelected = false; // Элемент контроля обновления страницы
        #endregion
        // Глобальные переменные окна
        #region
        public ViewMainWindow()
        {
            InitializeComponent();
            try
            {
                try
                {
                    stringURI = stringURI + "test.pdf";
                    Uri useFileUri = new Uri(stringURI);
                    DocViewer.Source = useFileUri;
                }
                catch (Exception exceptionFoundWay)
                {
                    MessegeWindow messegeWindow = new MessegeWindow("Сообщение!", "Путь по умолчанию не удалось найти. Выберете путь корневой дерикторий!");
                    messegeWindow.ShowDialog();

                    var dialogFolderSelect = new System.Windows.Forms.FolderBrowserDialog();
                    dialogFolderSelect.ShowDialog();
                    stringURI = dialogFolderSelect.SelectedPath;
                }
                Reloaded_Window();
            }
            catch (Exception e)
            {
                MessegeWindow messegeWindow = new MessegeWindow("Ошибка!", e.Message , "10X");
                messegeWindow.ShowDialog();
                Reloaded_Window();
            }
        } // Конструктор страницы

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Событие Закрытия окна

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                MessegeWindow messegeWindow = new MessegeWindow("Введите название нового документа!", "", true);
                messegeWindow.Owner = this;
                messegeWindow.ShowDialog();

                string fName = Application.Current.Properties["lastReturnValue"].ToString() + ".pdf";
                string defNameLib = "lib\\";
                string filePath = GetPath();

                string filePathIn = System.IO.Path.Combine(defNameLib, fName);
                File.Copy(filePath, filePathIn);
                try
                {
                    using (ApplicationContext DbContext = new ApplicationContext())
                    {
                        UrlDocs urlDocs = new UrlDocs();
                        urlDocs.url = fName;
                        DbContext.Add(urlDocs);
                        DbContext.SaveChanges();

                        MessegeWindow messege2 = new MessegeWindow("Сообщение:", "Документ успешно добавлен!");
                        messege2.ShowDialog();
                    }
                }
                catch (Exception exeptionDB)
                {
                    MessegeWindow messege = new MessegeWindow("Ошибка", "Не удается из-за исключения: " + exeptionDB.Message);
                    messege.ShowDialog();
                }
                
                Reloaded_Window();

            }
            catch (Exception exceptionIO)
            {
                MessegeWindow messege = new MessegeWindow("Ошибка", "Не удается из-за исключения: " + exceptionIO.Message);
                messege.ShowDialog();
            }
        } // Событие добавления новой записи
        
        private void Selection_NewItem(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (controlItemSelected == false)
                {
                    Application.Current.Properties["lastSelectionItem"] = ListBox_Main.SelectedItem;
                    Reloaded_Window();
                }
            } 
            catch(Exception exceptionSelector)
            {
                MessegeWindow messegeWindow = new MessegeWindow("Ошибка!",  exceptionSelector.Message);
                messegeWindow.ShowDialog();
            }
        } // Событие смены фокуса элемента 

        public string GetPath()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.ShowDialog();
            return dialog.FileName;
        } // Функция получения пути

        public void Reloaded_Window()
        {
            try
            {
                controlItemSelected = true;
                ListBox_Main.Items.Clear();
                controlItemSelected = false;
                Uri useFileUri = new Uri(stringURI);
                DocViewer.Source = useFileUri;

                using (ApplicationContext DbContext = new ApplicationContext())
                {
                    var buffDB = DbContext.UrlDoc.ToList();
                    foreach (UrlDocs allUrl in buffDB)
                    {
                        ListBox_Main.Items.Add(allUrl.url);
                    }
                }
            }
            catch (Exception e)
            {
                MessegeWindow messegeWindow = new MessegeWindow("Ошибка!", e.Message);
                messegeWindow.ShowDialog();
            }
        } // Функция перезагрузки окна
        #endregion 
        //Методы и события страницы
    }
}
