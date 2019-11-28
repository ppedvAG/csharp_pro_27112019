using Microsoft.Data.SqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HalloAsync
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

        private void OhneThreading(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1_____0__________0; i++)
            {
                pb1.Value = i;
                Thread.Sleep(3__0___0);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = false;
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(30);
                }
                pb1.Dispatcher.Invoke(() => b.IsEnabled = true);
            });
        }

        private void StartTaskmitTS(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            var b = (Button)sender;
            b.IsEnabled = false;
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Factory.StartNew(() => pb1.Value = i, cts.Token, TaskCreationOptions.None, ts);
                    Thread.Sleep(30);

                    if (cts.IsCancellationRequested)
                    {
                        //cleanup
                        break;
                    }
                }
                Task.Factory.StartNew(() => b.IsEnabled = true, CancellationToken.None, TaskCreationOptions.None, ts); ;
            });
        }

        CancellationTokenSource cts = null;

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            var b = (Button)sender;
            b.IsEnabled = false;
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;

                try
                {
                    await Task.Delay(30, cts.Token);
                }
                catch (TaskCanceledException)
                {
                    MessageBox.Show("Erfolgreich abgebrochen");
                }

                if (cts.IsCancellationRequested)
                    break;
            }
            b.IsEnabled = !false;
        }

        private async void CountDB(object sender, RoutedEventArgs e)
        {
            pb1.IsIndeterminate = true;
            var conString = "Server=.\\SQLEXPRESS;Database=Northwind;Trusted_Connection=true;";
            using var con = new SqlConnection(conString);
            await con.OpenAsync();
            //MessageBox.Show("DB ok");
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Employees;WAITFOR DELAY '00:00:05'";
            var result = await cmd.ExecuteScalarAsync();
            MessageBox.Show($"{result} Employees in DB");

            pb1.IsIndeterminate = !true;
        }


        public long AlteLangsameFunktion()
        {
            Thread.Sleep(3000);
            return 234567890;
        }

        public Task<long> AlteLangsameFunktionAsync()
        {
            return Task.Run(() => AlteLangsameFunktion());
        }

        private async void StarteAltUndLangsam(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Alt und langsame berechnet: {AlteLangsameFunktion()}");
            MessageBox.Show($"Alt und langsame berechnet: {await AlteLangsameFunktionAsync()}");
        }
    }
}
