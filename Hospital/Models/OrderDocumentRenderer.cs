using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Hospital.Models
{
    class OrderDocumentRenderer : IDocumentRenderer
    {
        public void Render(FlowDocument doc, object data)
        {
            ListBox group = doc.FindName("DaoPricture") as ListBox;
            group.ItemsSource = ((PainetMaster)data).OrderDetails;
        }
    }
}
