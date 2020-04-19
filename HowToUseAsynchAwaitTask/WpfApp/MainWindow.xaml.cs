using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
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

        private List<string> threadLog = new List<string>();
        private HttpClient _client = new HttpClient();

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            threadLog.Add($"1 => {Thread.CurrentThread.ManagedThreadId}");
            var text = await Do().ConfigureAwait(false);
            threadLog.Add($"4 => {Thread.CurrentThread.ManagedThreadId}");
            testBox.Text = text;
        }

        public async Task<string> Do()
        {
            threadLog.Add($"2 => {Thread.CurrentThread.ManagedThreadId}");
            var content = await _client.GetStringAsync("http://google.com")
                .ConfigureAwait(false);
            threadLog.Add($"3 => {Thread.CurrentThread.ManagedThreadId}");
            return "Hello World";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var a = threadLog;
        }
    }
}
