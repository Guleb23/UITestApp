using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows;
using System.Windows.Media;
using ElbrusAPI.Models;
using System.Runtime.InteropServices;
using EasyCaptcha.Wpf;

namespace UITestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int countLogin = 0;
        Task<IEnumerable<WorkerModel>> models;
        string answer = "";
        public MainWindow()
        {
            InitializeComponent();
            models = CallAPI();
            WorkerModel work = new WorkerModel();
            work = models.Result.FirstOrDefault();
            MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
            answer = MyCaptcha.CaptchaText;

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            txbLogin.BorderBrush = Brushes.White;
        }


        public static async Task<IEnumerable<WorkerModel>> CallAPI()
        {
            HttpClient httpClient = new HttpClient();
            string baseUrl = "https://localhost:7176/ElbrusAPIUser";
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.BaseAddress = new Uri(baseUrl);
            var response = httpClient.GetAsync(baseUrl).Result;
            if(response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<IEnumerable<WorkerModel>>().Result;
                return res;
            }
            else
            {
                return response.Content.ReadAsAsync<IEnumerable<WorkerModel>>().Result;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = models.Result.FirstOrDefault(w => w.login == txbLogin.Text && w.password == txbPass.Password);
            if(countLogin >= 2)
            {
                MyCaptcha.Visibility = Visibility.Visible;
                stackCap.Visibility = Visibility.Visible;
                if(currentUser != null && txbCap.Text == answer)
                {
                    MessageBox.Show("Вы успешно вошли");
                }
                else
                {
                    MessageBox.Show("aGAIN");
                }

            }else if(currentUser != null)
            {
                MessageBox.Show("Вы успешно вошли");
            }
            else
            {
                countLogin++;
                MessageBox.Show("Не найдено в системе");
            }
        }
    }
}