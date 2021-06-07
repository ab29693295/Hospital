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
using System.Xml.Linq;

namespace Hospital
{
    /// <summary>
    /// MessageTip.xaml 的交互逻辑
    /// </summary>
    public partial class RecordDeleteSure : Window
    {
        public int DType;

        public int _DeleteID;

        protected UnitOfWork unitOfWork = new UnitOfWork();


        public RecordDeleteSure( int Type,int ID)
        {
            InitializeComponent();
            _DeleteID = ID;
            DType = Type;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            if (DType == 1)
            {
                unitOfWork.DOperationImage.Delete(_DeleteID);
                unitOfWork.Save();
                this.Close();
                RecordImage._recordImage.DeleteSuccess();

            }
            else if (DType == 2)
            {
                unitOfWork.DOperationVideo.Delete(_DeleteID);
                unitOfWork.Save();
                this.Close();
                RecordManageVideo._RecordManageVideo.DeleteSuccess();
            }
            else if (DType == 3)
            {
                unitOfWork.DOperationRecord.Delete(_DeleteID);
                unitOfWork.Save();
                this.Close();
                RecordManage._recordManage.DeleteSuccess();
            }
            else if (DType == 4)
            {
                unitOfWork.DPatient.Delete(_DeleteID);
                unitOfWork.Save();
                this.Close();
                Operation._Operation.DeleteSure();
            }
            this.Close();
        }
        private void Button_NO_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }
    }
}
