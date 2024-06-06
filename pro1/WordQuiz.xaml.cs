using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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

namespace pro1
{
    public partial class WordQuiz : Window
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            BasePath = "https://vocalist-ef97c-default-rtdb.firebaseio.com/",
            AuthSecret = "mXcAu7GqGQ0nXRaWkzypmgLRuwBOIR1DjSkzTuoo"
        };
        IFirebaseClient client;
        FirebaseResponse response;
        Data obj;
        FirebaseResponse resp;
        Data o;

        bool viewFlag = false;
        bool onFlag = false; 
        string correct;

        public WordQuiz(MainWindow mainWindow)
        {
            InitializeComponent();

            client = new FireSharp.FirebaseClient(config);

            LoadData();

        }
          private async void LoadData()
        {
            btnAnswer1.Content = "";
            btnAnswer2.Content = "";
            btnAnswer3.Content = "";
            btnAnswer4.Content = "";

            Random r = new Random();
            int index = r.Next(1, 665);  // DB에 저장된 데이터를 불러올 index 선언
            FirebaseResponse response = await client.GetAsync("Voca/" + index);
            Data obj = response.ResultAs<Data>();
            txtQuest.Text = obj.mean;
            correct = obj.word;

            List<Button> meanbtn = new List<Button> { btnAnswer1, btnAnswer2, btnAnswer3, btnAnswer4 };

            int ansIndex = r.Next(4);  // 정답의 버튼 위치 지정
            meanbtn[ansIndex].Content = correct;  // 랜덤으로 설정한 버튼에 정답을 넣음

            List<string> Indexes = new List<string> { index.ToString() };  // 사용한 index를 Indexes 리스트에 저장

            Data o;

            // 남은 버튼에 의미 넣기
            for (int i = 0; i < 4; i++)
            {
                if (i != ansIndex)
                {
                    index = r.Next(1, 665);
                    FirebaseResponse resp = await client.GetAsync("Voca/" + index);
                    o = resp.ResultAs<Data>();
                    Indexes.Add(index.ToString());
                    meanbtn[i].Content = o.word;
                }
            }
            viewFlag = true;
            if (viewFlag == true)
                grid1.Visibility = Visibility.Visible;
            onFlag = false;
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Content.ToString() == correct)
                onFlag = true;

            if (onFlag == true)
            {
                grid1.Visibility = Visibility.Hidden;
                LoadData();
            }
        }
    }
}
