using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Q566260
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            for (int i = 1; i < 30; i++)
            {
                customers.Add(new Customer() { ID = i, Name = "Name" + i, SomeProperty = "Value" + i });
            }
            grid.ItemsSource = customers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DocumentPreviewWindow preview = new DocumentPreviewWindow();
            PrintableControlLink link = new PrintableControlLink(grid.View as DevExpress.Xpf.Printing.IPrintableControl);
            link.ExportServiceUri = "../ExportService1.svc";
            LinkPreviewModel model = new LinkPreviewModel(link);
            preview.Model = model;
            link.CreateDocument(true);
            preview.ShowDialog();
        }
    }
    public class Customer
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string SomeProperty
        {
            get;
            set;
        }
    }

    public class MyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridColumn column = value as GridColumn;
            if (column.FieldName == "Name")
                return new SolidColorBrush(Colors.Red);
            return new SolidColorBrush(Colors.Green); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
