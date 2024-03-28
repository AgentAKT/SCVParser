using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Collections;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CSVParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            collectUIDs.IsEnabled = false;
            deleteDuplicates.IsEnabled = false;
            saveToFile.IsEnabled = false;
        }


        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>

        //    Нажать на кнопку Папка

        List<string> pathFilesList = new List<string>() { }; //Создать список путей к файлам
        List<string> uidsList = new List<string>() { }; //Создать список UIDов
        List<string> onlyUidsList = new List<string>() { }; //Создать список только из UIDов
        List<string> fullUidsList = new List<string>() { }; //Создать ИТОГОВЫЙ список UIDов
        List<string> fullResultUidsList = new List<string>() { }; //Создать список со значениями для записи в файл
        

        int counterFiles = 0; //Найдено .csv файлов   //Удалено дубликатов    //Записано в файл
        int counterRows = 0; //Обработано строк    
        int counterAddUids = 0; //Выбрано UIDов    
        int counterDuplicates = 0; //  Счетчик дубликатов UIDов
        int counterAddInFileUIDs = 0; //  Счетчик UIDов добавленных в файл

        private void pathBtn_Click(object sender, RoutedEventArgs e)
        {
            //    Обнулить счетчик списка файлов
            counterFiles = 0;
            counterAddUids = 0;
            counterDuplicates = 0;
            counterAddInFileUIDs = 0;

            processedFilesLabelValue.Content = 0;
            processedRowsLabelValue.Content = 0;
            collectedUIDsLabelValue.Content = 0;
            deletedUIDsLabelValue.Content = 0;
            writeToFileUIDsLabelValue.Content = 0;

            collectUIDs.IsEnabled = false;
            deleteDuplicates.IsEnabled = false;
            saveToFile.IsEnabled = false;

            pathFilesList.Clear();
            uidsList.Clear();
            onlyUidsList.Clear();
            fullUidsList.Clear();
            fullResultUidsList.Clear();

            //    Получить путь к файлам и записать в текстбокс Путь
            pathTextBox.Text = receivePath();


            //    Получить список файлов и записать список файлов в переменную
            var ListOfFiles = receiveListOfFiles(pathTextBox.Text);


            //    Заполнить список файлов
            writeListOfFiles(ListOfFiles);

            collectUIDs.IsEnabled = true;
        }

        //    Получить путь к папке 
        public static string receivePath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string folderCSV = dialog.SelectedPath;//выбранный путь в переменную
            return folderCSV;

        }

        //    Получить список файлов
        public static string[] receiveListOfFiles(string x)
        {            
            string[] files = Directory.GetFiles(x, "*.csv");
            return files;
        }

        public void writeListOfFiles(string[] x)
        {
            foreach (string s in x)
            {
                pathFilesList.Add(s);
                counterFiles++;
                processedFilesLabelValue.Content = counterFiles;
            }
            if (counterFiles == 0)
            {
                System.Windows.MessageBox.Show("В выбранной папке .csv файлы не найдены");
                pathTextBox.Text = "";
            }

            for (int j = 0; j < pathFilesList.Count; j++)
            {
                Console.WriteLine(pathFilesList[j]);
            }

        }

        //При нажатии на Enter ошибка
        private void pathTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                System.Windows.MessageBox.Show("Не балуйся, я так пока не умею :)");
                pathTextBox.Text = "";
            }
        }


        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>
        /// 

        

        private void collectUIDs_Click(object sender, RoutedEventArgs e)
        {

            uidsList.Clear();
            onlyUidsList.Clear();
            fullUidsList.Clear();
            fullResultUidsList.Clear();

            counterRows = 0; //счетчик строк
            counterAddUids = 0;    //Выбрано UIDов    
            counterDuplicates = 0; // Счетчик дубликатов UIDов
            counterAddInFileUIDs = 0; //  Счетчик UIDов добавленных в файл

            if (checkMeasurements() | checkPeriods() | checktimes() | checkColumns() | checkpathTextBox())
            {

            }

            else
            {
                foreach (string s in pathFilesList)
                {
                    using(var reader = new StreamReader(s))
                    {
                        while (!reader.EndOfStream)
                        {
                            try
                            {
                                //   Прочитать текущую линию
                                var line = reader.ReadLine();

                                //   Разделить строку на массив значений
                                string[] values = line.Split(';');

                                //    Номер колонки переводим в индекс
                                int columnNum = Convert.ToInt32(numberOfColumnTextBox.Text) - 1;

                                //    Проверка UIDа
                                isValidGuid(values[columnNum]);

                                counterRows++;
                                deleteDuplicates.IsEnabled = true;
                            }
                            catch {
                                System.Windows.MessageBox.Show("Кажется, в папке есть пустые файлы, либо файлы только с одним столбцом");
                                break;
                            }
                            

                        }

                    }
                }
            }

            processedRowsLabelValue.Content = counterRows;
            collectedUIDsLabelValue.Content = counterAddUids;
            if (counterAddUids == 0)
            {
                System.Windows.MessageBox.Show("Кажется, в выбранном столбце нет UIDов");
            }
            else if (counterAddUids >1900000)
            {
                System.Windows.MessageBox.Show("Слишком много UIDов: " + counterAddUids + " я столько не переварю :)");
            }
            //for (int j = 0; j < uidsList.Count; j++)
            //{
            //    Console.WriteLine(uidsList[j]);
            //}


        }

        //    Проверка UIDа
        internal void isValidGuid(string uid)
        {

            Guid parsedGuid;
            bool isValidGuid = Guid.TryParse(uid, out parsedGuid);
            if (isValidGuid)
            {
                onlyUidsList.Add(uid);
                //Console.WriteLine(uid);
                counterAddUids++;                              
            }
            else
            {
                //Console.WriteLine("======================Это не UID======================");
            }

        }
        //    Удалить дубликаты
        private void deleteDuplicates_Click(object sender, RoutedEventArgs e)
        {
            if (counterAddUids >= 1200000)
            {
                System.Windows.MessageBox.Show("Я насобирал " + counterAddUids + " UIDов. Это много. Я сейчас зависну (до 15 минут), а потом отвисну. Попей кофе");
            }
            else
            {
                System.Windows.MessageBox.Show("Начал считать, никуда не уходи");
            }

            fullResultUidsList.Clear();

            counterDuplicates = 0; //  Счетчик дубликатов UIDов
            counterAddInFileUIDs = 0; //  Счетчик UIDов добавленных в файл

            foreach (var u in onlyUidsList)
            {
                if (fullUidsList.Contains(u))
                {
                    Console.WriteLine("======================Такой UID уже есть======================");
                    counterDuplicates++;
                }
                else
                {
                    fullUidsList.Add(u);
                    Console.WriteLine(u);
                    counterAddUids++;
                }
            }

            deletedUIDsLabelValue.Content= counterDuplicates;

            saveToFile.IsEnabled = true;
        }

        private void saveToFile_Click(object sender, RoutedEventArgs e)
        {
            int counterAddInFileUIDs = 0;

            if (checkMeasurements() | checkPeriods() | checktimes())
            {

            }
            else
            {
                int counterValue = Convert.ToInt32(numberOfValuesTextBox.Text);

                foreach (string z in fullUidsList)
                {
                    //string resString = z.Trim() + "=";
                    string resString = "";
                    while (counterValue != 0)
                    {
                        //    Собираем строку

                        resString += randomValue() + "*" + randomTime() + "*10000002;";
                        counterValue--;
                    }
                    fullResultUidsList.Add(z.Trim() + "=" + resString + "!");
                    counterValue = Convert.ToInt32(numberOfValuesTextBox.Text);
                    counterAddInFileUIDs++;
                }
            //    Сохранить в файл
             buildFile(counterAddInFileUIDs);
            }
            
        }

        //    Рандомное время
        private int randomTime()
        {
            
            int t1, t2;
            t1 = Convert.ToInt32(timeRandomFromTextBox.Text);
            t2 = Convert.ToInt32(timeRandomToTextBox.Text);

            int timeRnd = rnd.Next(t1, t2);
            return (int)timeRnd;
        }

        Random rnd = new Random();

        private int randomValue()
        {
            int v1, v2;
            v1 = Convert.ToInt32(valuesRandomFromTextBox.Text);
            v2 = Convert.ToInt32(valuesRandomToTextBox.Text);

            int valueRnd = rnd.Next(v1, v2);
            return (int)valueRnd;
        }

        private void buildFile(int counterUIDs)
        {
            writeToFileUIDsLabelValue.Content = counterUIDs;
            Console.WriteLine(writeToFileUIDsLabelValue);
            System.IO.File.WriteAllLines("TI.dat", fullResultUidsList);
            System.Windows.MessageBox.Show("Файл TI.dat сохранен, получено " + counterUIDs + " UIDов");

            collectUIDs.IsEnabled = false;
            deleteDuplicates.IsEnabled = false;
            saveToFile.IsEnabled = false;
        }

        private bool checkMeasurements()
        {
            if (numberOfColumnTextBox.Text == "" || numberOfValuesTextBox.Text == "" || valuesRandomFromTextBox.Text == "" || valuesRandomToTextBox.Text == "" || timeRandomFromTextBox.Text == "" || timeRandomToTextBox.Text == "")
            {
                System.Windows.MessageBox.Show("Заполни пустые поля");
                return true;
            }
            else { return false; }
        }

        private bool checkPeriods()
        {
            if (Convert.ToInt32(valuesRandomFromTextBox.Text) >= Convert.ToInt32(valuesRandomToTextBox.Text) || Convert.ToInt32(timeRandomFromTextBox.Text) >= Convert.ToInt32(timeRandomToTextBox.Text))
            {
                System.Windows.MessageBox.Show("Проверь начало и конец диапазонов");
                return true;
            }
            else { return false; }
        }

        private bool checktimes()
        {
            if (Convert.ToInt32(timeRandomFromTextBox.Text) <= 0 || Convert.ToInt32(timeRandomToTextBox.Text) <= 0)
            {
                System.Windows.MessageBox.Show("Время не может быть меньше 1");
                return true;
            }
            else { return false; }
        }

        private bool checkColumns()
        {
            if (numberOfColumnMeasurements.Text == numberOfColumnTextBox.Text)
            {
                System.Windows.MessageBox.Show("UIDы и замеры должны быть в разных столбцах");
                return true;
            }
            else { return false; }
        }

        private bool checkpathTextBox()
        {
            if (pathTextBox.Text == "")
            {
                System.Windows.MessageBox.Show("Заполни путь к файлам");
                return true;
            }
            else { return false; }
        }


    private void prBar_ValueChanged()
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void numberOfColumnTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
