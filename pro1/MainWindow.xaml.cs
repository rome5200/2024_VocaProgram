using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace pro1
{
    public partial class MainWindow : Window
    {
        QuizWindow q = null;
        WordQuiz w = null;
        HiraganaWindow h = null;

        private Random random;
        int i = 1;

        int randomIndex;
        int MaxIndex = 664;

        IFirebaseConfig config = new FirebaseConfig
        {
            BasePath = "https://vocalist-ef97c-default-rtdb.firebaseio.com/",
            AuthSecret = "mXcAu7GqGQ0nXRaWkzypmgLRuwBOIR1DjSkzTuoo"
        };
        IFirebaseClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("연결 실패!");
                return;
            }

            random = new Random();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // 랜덤 라디오 버튼
            if (rbRandom.IsChecked == true)
            {
                //int randomIndex = new Random().Next(1, MaxIndex + 1);
                randomIndex = random.Next(1, MaxIndex + 1);

                FirebaseResponse resp = await client.GetAsync("Voca/" + randomIndex);
                int number = 0;

                Data data = resp.ResultAs<Data>();


                if (rbBoth.IsChecked == true)
                {
                    tbNum.Text = "No. " + (number + randomIndex).ToString();
                    tbWord.Text = data.word;
                    tbMean.Text = data.mean;
                }

                else if (rbWord.IsChecked == true)
                {
                    tbNum.Text = "No. " + (number + randomIndex).ToString();
                    tbWord.Text = data.word;
                    tbMean.Text = "";
                }

                else if (rbMean.IsChecked == true)
                {
                    tbNum.Text = "No. " + (number + randomIndex).ToString();
                    tbWord.Text = "";
                    tbMean.Text = data.mean;
                }

            }
            // 순서대로 라디오 버튼
            if (rbSequence.IsChecked == true)
            {
                FirebaseResponse resp = await client.GetAsync("Voca/" + i);
                Data data = resp.ResultAs<Data>();

                int number = 0;
                if (rbBoth.IsChecked == true)
                {
                    tbNum.Text = "No. " + (number + i).ToString();
                    tbWord.Text = data.word;
                    tbMean.Text = data.mean;
                }
                else if (rbWord.IsChecked == true)
                {
                    tbNum.Text = "No. " + (number + i).ToString();
                    tbWord.Text = data.word;
                    tbMean.Text = "";
                }
                else if (rbMean.IsChecked == true)
                {
                    tbNum.Text = "No. " + (number + i).ToString();
                    tbWord.Text = "";
                    tbMean.Text = data.mean;
                }

                i++; // i 값을 증가시켜 다음 단어를 가져오도록 합니다.
            }
            
        }

        private void btnHiragana_Click(object sender, RoutedEventArgs e)
        {
            if (h == null)
            {
                h = new HiraganaWindow();
                h.Show();
            }
        }

        private void btnMeanQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (q == null)
                q = new QuizWindow(this);
            q.Show();
        }

        private void btnWordQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (w == null)
                w = new WordQuiz(this);
            w.Show();
        }
    }
}
