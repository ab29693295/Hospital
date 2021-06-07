using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Controls;
using System.Windows.Xps;
using System.Windows;
using System.Windows.Media.Imaging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Windows.Media;


namespace Hospital
{
    public class PdfXpsHelper
    {
        public static string FileFolder;
        private static int id = 0;
        public static PdfDocument PdfFile = new PdfDocument();

        public static void SaveViewContentToPdf(string path, Viewbox viewbox)
        {
            SaveMemoryStreamToPdf(path, SaveViewContentToMemoryStream(path, viewbox));
        }

        public static void SaveMemoryStreamToPdf(string path, MemoryStream lMemoryStream)
        {
            var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
            if (String.IsNullOrWhiteSpace(path))
            {
                string myPath = @"C:/pdfreport" + ".pdf";
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, myPath, 0);
                id++;
            }
            else
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, path, 0);
        }


        public static MemoryStream SaveViewContentToMemoryStream(string path, Viewbox viewbox)
        {
            viewbox.StretchDirection = StretchDirection.Both;

            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            VisualsToXpsDocument vToXpsD = (VisualsToXpsDocument)writer.CreateVisualsCollator();
            var newSize = new Size(0, 0);
            if (!viewbox.IsLoaded || viewbox.RenderSize.Height <= 0 || viewbox.RenderSize.Width <= 0)
            {
                //newSize.Width = 816;
                //newSize.Height = 1056;
                newSize.Width = 950;
                newSize.Height = 3150;
                viewbox.Arrange(new Rect(newSize));
                viewbox.Measure(newSize);
                viewbox.UpdateLayout();
            }
            vToXpsD.Write(viewbox);
            vToXpsD.EndBatchWrite();
            doc.Close();
            package.Close();

            return lMemoryStream;
        }
    }
}