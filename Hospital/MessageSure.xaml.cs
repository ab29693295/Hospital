using Smart.Service;
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

namespace Hospital
{
    /// <summary>
    /// MessageTip.xaml 的交互逻辑
    /// </summary>
    public partial class MessageSure : Window
    {
        public int RecordID = 0;
        protected UnitOfWork unitOfWork = new UnitOfWork();
        public MessageSure( int ID)
        {
            InitializeComponent();
            RecordID = ID;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.CurrentRecordID = RecordID;
            if (RecordID != 0)
            {

                var ImageList = unitOfWork.DOperationImage.Get(p => p.operationID == RecordID);
               MainWindow._mainWindow.lb.ItemsSource = ImageList;


                var listFile = unitOfWork.DOperationVideo.Get(p => p.operationID == RecordID);

                MainWindow._mainWindow.DG1.ItemsSource = listFile;
            }
            MainWindow._mainWindow.BtnSave_Click(sender, e);
            
            this.Close();
        }
        private void Button_NO_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.Button_Click_New(sender, e);
            this.Close();
        }
    }
}
