using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Serialization;

namespace HalloBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Suche(object sender, RoutedEventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={tb1.Text}";

            using (var web = new WebClient())
            {
                var json = web.DownloadString(url);
                var result = JsonConvert.DeserializeObject<BooksResult>(json);
                grid.ItemsSource = result.items.Select(x => x.volumeInfo);
            }
        }

        private void LinqQuery(object sender, RoutedEventArgs e)
        {
            var books = grid.ItemsSource as IEnumerable<Volumeinfo>;

            var query = from b in books
                        where b.pageCount < 500
                        orderby b.language, b.title descending
                        select b;

            grid.ItemsSource = query.ToList();
        }

        private void LinqLambda(object sender, RoutedEventArgs e)
        {
            var books = grid.ItemsSource as IEnumerable<Volumeinfo>;

            grid.ItemsSource = books.Where(b => b.pageCount < 500)
                                    .OrderBy(x => x.language)
                                    .ThenByDescending(x => x.title)
                                    .ToList();

        }

        private void ExportXML(object sender, RoutedEventArgs e)
        {
            var books = grid.ItemsSource as IEnumerable<Volumeinfo>;
            var filename = "book.xml";

            using (var sw = new StreamWriter(filename))
            {
                var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                serial.Serialize(sw, books.ToList());
                MessageBox.Show("Ok");
            }

        }

        private void ImportXML(object sender, RoutedEventArgs e)
        {
            var filename = "book.xml";

            using (var sr = new StreamReader(filename))
            {
                var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                grid.ItemsSource = (IEnumerable<Volumeinfo>)serial.Deserialize(sr);
            }
        }

        private void ExportExcel(object sender, RoutedEventArgs e)
        {
            var books = grid.ItemsSource as IEnumerable<Volumeinfo>;
            var filename = "book.xlsx";

            var fi = new FileInfo(filename);

            using (var pack = new ExcelPackage(fi))
            {
                var ws = pack.Workbook.Worksheets.FirstOrDefault(x => x.Name == "books");
                if (ws == null)
                    ws = pack.Workbook.Worksheets.Add("books");

                ws.Cells[1, 1].Value = "Meine Bücher";
                int row = 2;
                foreach (var b in books)
                {
                    ws.Cells[row, 1].Value = b.title;
                    ws.Cells[row, 2].Value = b.subtitle;
                    ws.Cells[row, 3].Value = b.pageCount;
                    ws.Cells[row, 4].Value = b.language;

                    if (b.pageCount > 700)
                    {
                        ws.Cells[row, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightVertical;
                        ws.Cells[row, 3].Style.Fill.PatternColor.Indexed = 8;
                    }

                    row++;
                }
                pack.Save();
            }
            Process.Start(filename);
        }

        private void SumPages(object sender, RoutedEventArgs e)
        {
            var books = grid.ItemsSource as IEnumerable<Volumeinfo>;
            var filename = "book.xlsx";

            var fi = new FileInfo(filename);

            using (var pack = new ExcelPackage(fi))
            {
                var ws = pack.Workbook.Worksheets.FirstOrDefault(x => x.Name == "books");

                var count = ws.Cells["A1:M100"].Where(x => x.Value != null &&
                                                           x.Style.Fill.PatternType == ExcelFillStyle.LightVertical)
                                                .Sum(x => int.Parse(x.Value.ToString()));
                MessageBox.Show($"Count: {count}");
            }
        }
    }
}

