using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace SIEMENS_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Answers
        {
            public int QuestionNo { get; set; }
            public string RB_No { get; set; }
            public bool IsCorrect { get; set; }
        }

        public Random r = new Random(DateTime.Now.Ticks.GetHashCode());
        private Quiz q = new Quiz();
        DispatcherTimer _timer;
        TimeSpan _time;
        private Dictionary<int, int> DictIndex = new Dictionary<int, int>();
        private List<Answers> ListofAnswers = new List<Answers>();
        private List<int> AnsweredQuestions = new List<int>();
        private List<int> UnAnsweredQuestions = new List<int>();
        private List<int> UpdateListBox = new List<int>();
        private List<int> randomList = new List<int>();
        private string ans;
        private int score = 0;
        private int number = 1;
        private int RandomNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
            txtFname.Focus();
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFname.Text) && !string.IsNullOrEmpty(txtLname.Text))
                StartQuiz();
            else
                MessageBox.Show("Enter your name to start the quiz");
        }

        private void StartQuiz()
        {
            SetRandomQuestions();
            AddPagination();
            StartTimer();

            lblUser.Text = txtFname.Text + " " + txtLname.Text;
            txtFname.Visibility = Visibility.Hidden;
            txtLname.Visibility = Visibility.Hidden;
            lblFname.Visibility = Visibility.Hidden;
            lblLname.Visibility = Visibility.Hidden;
            start.Visibility = Visibility.Hidden;
            lblTimer.Visibility = Visibility.Visible;
            quest.Visibility = Visibility.Visible;
            rb1.Visibility = Visibility.Visible;
            rb2.Visibility = Visibility.Visible;
            rb3.Visibility = Visibility.Visible;
            rb4.Visibility = Visibility.Visible;
            scoreLbl.Visibility = Visibility.Visible;
            btnPrev.Visibility = Visibility.Visible;
            btnNext.Visibility = Visibility.Visible;
            btnEnd.Visibility = Visibility.Visible;
            rb1.Visibility = Visibility.Visible;
            rb2.Visibility = Visibility.Visible;
            rb3.Visibility = Visibility.Visible;
            rb4.Visibility = Visibility.Visible;
           
            int i = DictIndex[number];
            quest.Text = number + ". " + q.getQuestion(i);

            DisplayQuizQuestion(i);

        }

        private void DisplayQuizQuestion(int i)
        {
            rb1.Content = q.getAnswer(i, 1);
            rb2.Content = q.getAnswer(i, 2);
            rb3.Content = q.getAnswer(i, 3);
            rb4.Content = q.getAnswer(i, 4);

            rb1.Content = q.getAnswer(i, 1);
            rb2.Content = q.getAnswer(i, 2);
            rb3.Content = q.getAnswer(i, 3);
            rb4.Content = q.getAnswer(i, 4);

            if (Convert.ToString(rb1.Content).StartsWith("*"))
            {
                this.ans = Convert.ToString(rb1.Content).Substring(1, Convert.ToString(rb1.Content).Length - 1);
                rb1.Content = Convert.ToString(rb1.Content).Substring(1, Convert.ToString(rb1.Content).Length - 1);
            }
            else
            {
                if (Convert.ToString(rb2.Content).StartsWith("*"))
                {
                    this.ans = Convert.ToString(rb2.Content).Substring(1, Convert.ToString(rb2.Content).Length - 1);
                    rb2.Content = Convert.ToString(rb2.Content).Substring(1, Convert.ToString(rb2.Content).Length - 1);
                }
                else
                {
                    if (Convert.ToString(rb3.Content).StartsWith("*"))
                    {
                        this.ans = Convert.ToString(rb3.Content).Substring(1, Convert.ToString(rb3.Content).Length - 1);
                        rb3.Content = Convert.ToString(rb3.Content).Substring(1, Convert.ToString(rb3.Content).Length - 1);
                    }
                    else
                    {
                        this.ans = Convert.ToString(rb4.Content).Substring(1, Convert.ToString(rb4.Content).Length - 1);
                        rb4.Content = Convert.ToString(rb4.Content).Substring(1, Convert.ToString(rb4.Content).Length - 1);
                    }
                }
            }
        }

        private void StartTimer()
        {
            _time = TimeSpan.FromSeconds(180);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                lblTimer.Content = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                    CheckIfAnsweredAll();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            
        }

        private void Option1_Click(object sender, RoutedEventArgs e)
        {
            ListofAnswers.RemoveAll(r => r.QuestionNo == this.number);
            if (Convert.ToString(rb1.Content) == this.ans)
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb1.Name.ToString(), IsCorrect = true });
            else
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb1.Name.ToString(), IsCorrect = false });
            UpdateListBoxStatus(this.number);
        }

        private void Option2_Click(object sender, RoutedEventArgs e)
        {
            ListofAnswers.RemoveAll(r => r.QuestionNo == this.number);
            if (Convert.ToString(rb2.Content) == this.ans)
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb2.Name.ToString(), IsCorrect = true });
            else
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb2.Name.ToString(), IsCorrect = false });
            UpdateListBoxStatus(this.number);

        }

        private void Option3_Click(object sender, RoutedEventArgs e)
        {
            ListofAnswers.RemoveAll(r => r.QuestionNo == this.number);
            if (Convert.ToString(rb3.Content) == this.ans)
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb3.Name.ToString(), IsCorrect = true });
            else
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb3.Name.ToString(), IsCorrect = false });
            UpdateListBoxStatus(this.number);
        }

        private void Option4_Click(object sender, RoutedEventArgs e)
        {
            ListofAnswers.RemoveAll(r => r.QuestionNo == this.number);
            if (Convert.ToString(rb4.Content) == this.ans)
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb4.Name.ToString(), IsCorrect = true });
            else
                ListofAnswers.Add(new Answers { QuestionNo = this.number, RB_No = rb4.Name.ToString(), IsCorrect = false });
            UpdateListBoxStatus(this.number);
        }

        private void ReTakeQuiz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Close();
            ClearAnswers();
            ListofAnswers.Clear();
            AnsweredQuestions.Clear();
            UnAnsweredQuestions.Clear();
            UpdateListBox.Clear();
            StartTimer();
            this.score = 0;
            this.number = 1;
            final.Visibility = Visibility.Hidden;
            restart.Visibility = Visibility.Hidden;
            quest.Visibility = Visibility.Visible;
            rb1.Visibility = Visibility.Visible;
            rb2.Visibility = Visibility.Visible;
            rb3.Visibility = Visibility.Visible;
            rb4.Visibility = Visibility.Visible;
            scoreLbl.Visibility = Visibility.Visible;
            btnNext.Visibility = Visibility.Visible;
            btnPrev.Visibility = Visibility.Visible;
            btnEnd.Visibility = Visibility.Visible;
            sp.Visibility = Visibility.Visible;
            lblUser.Visibility = Visibility.Hidden;
            lblTimer.Visibility = Visibility.Visible;
            lblUser.Visibility = Visibility.Visible;
            
            int i = DictIndex[number];
            quest.Text = number + ". " + q.getQuestion(i);
            //
            rb1.Content = q.getAnswer(i, 1);
            rb2.Content = q.getAnswer(i, 2);
            rb3.Content = q.getAnswer(i, 3);
            rb4.Content = q.getAnswer(i, 4);
            if (Convert.ToString(rb1.Content).StartsWith("*"))
            {
                this.ans = Convert.ToString(rb1.Content).Substring(1, Convert.ToString(rb1.Content).Length - 1);
                rb1.Content = Convert.ToString(rb1.Content).Substring(1, Convert.ToString(rb1.Content).Length - 1);
            }
            else
            {
                if (Convert.ToString(rb2.Content).StartsWith("*"))
                {
                    this.ans = Convert.ToString(rb2.Content).Substring(1, Convert.ToString(rb2.Content).Length - 1);
                    rb2.Content = Convert.ToString(rb2.Content).Substring(1, Convert.ToString(rb2.Content).Length - 1);
                }
                else
                {
                    if (Convert.ToString(rb3.Content).StartsWith("*"))
                    {
                        this.ans = Convert.ToString(rb3.Content).Substring(1, Convert.ToString(rb3.Content).Length - 1);
                        rb3.Content = Convert.ToString(rb3.Content).Substring(1, Convert.ToString(rb3.Content).Length - 1);
                    }
                    else
                    {
                        this.ans = Convert.ToString(rb4.Content).Substring(1, Convert.ToString(rb4.Content).Length - 1);
                        rb4.Content = Convert.ToString(rb4.Content).Substring(1, Convert.ToString(rb4.Content).Length - 1);
                    }
                }
            }
        }

        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (this.number > 1)
            {
                this.number--;
                AnswerProcessing();
                SetAnswer(this.number);
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (this.number < 11)
            {
                this.number++;
                AnswerProcessing();
                SetAnswer(this.number);
            }
        }

        private void AnswerProcessing()
        {
            int i = DictIndex[number];
            quest.Text = number + ". " + q.getQuestion(i);

            DisplayQuizQuestion(i);
        }

        private void ClearAnswers()
        {
            rb1.IsChecked = false;
            rb2.IsChecked = false;
            rb3.IsChecked = false;
            rb4.IsChecked = false;
        }

        private void EndQuiz_Click(object sender, RoutedEventArgs e)
        {
            CheckIfAnsweredAll();
        }

        private void QuizOver()
        {
            _timer.Stop();
            btnEnd.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
            btnPrev.Visibility = Visibility.Hidden;
            quest.Visibility = Visibility.Hidden;
            rb1.Visibility = Visibility.Hidden;
            rb2.Visibility = Visibility.Hidden;
            rb3.Visibility = Visibility.Hidden;
            rb4.Visibility = Visibility.Hidden;
            final.Visibility = Visibility.Visible;
            final.Content = "   \t Congratulations  " + lblUser.Text + "\n" + "       " + " Your score for the quiz attend on " + DateTime.Now.ToShortDateString() + " is " + this.score;
            restart.Visibility = Visibility.Visible;
            sp.Visibility = Visibility.Hidden;
            lblUser.Visibility = Visibility.Hidden;
            lblTimer.Visibility = Visibility.Hidden;
            lblStatus.Visibility = Visibility.Hidden;
            ListBoxStatus.Visibility = Visibility.Hidden;
        }

        private void AddPagination()
        {

            for (int i = 1; i <= 11; ++i)
            {
                Button button = new Button()
                {
                    Content = i,
                    Tag = i,
                    Name = "Question" + i.ToString()
                };
                button.Height = 20;
                button.Width = 30;
                button.Click += new RoutedEventHandler(Pagination_Click);
                this.sp.Children.Add(button);
            }
        }

        private void SetRandomQuestions()
        {
            int i = 1;
            while (DictIndex.Count < 11 || i < 11)
            {
                RandomNumber = r.Next(0, 11);
                if (!randomList.Contains(RandomNumber))
                {
                    randomList.Add(RandomNumber);
                    DictIndex.Add(i, RandomNumber);
                    i++;
                }
            }
        }

        private void UpdateListBoxStatus(int i)
        {
            if(!UpdateListBox.Contains(i))
            {
                ListBoxStatus.ItemsSource = null;
                UpdateListBox.Add(i);
                UpdateListBox.Sort();
                ListBoxStatus.ItemsSource = UpdateListBox;
            }

            if (UpdateListBox.Count > 0)
            {
                lblStatus.Visibility = Visibility.Visible;
                ListBoxStatus.Visibility = Visibility.Visible;
            }
        }

        void Pagination_Click(object sender, RoutedEventArgs e)
        {
            
            int j = (int)(sender as Button).Tag;
            int i = 0;
            SetAnswer(j);

            i = DictIndex[j];
            number = j;
            quest.Text = j + ". " + q.getQuestion(i);
            //
            DisplayQuizQuestion(i);

        }

        private void SetAnswer(int j)
        {
            ClearAnswers();
            List<Answers> Answers = new List<Answers>();
            Answers = ListofAnswers.Select(s => s).Where(w => w.QuestionNo == j).ToList();

            if (Answers.Count > 0)
            {
                string RBNo = Answers[0].RB_No;
                if (RBNo.Contains("1"))
                    rb1.IsChecked = true;
                else
                {
                    if (RBNo.Contains("2"))
                        rb2.IsChecked = true;
                    else
                    {
                        if (RBNo.Contains("3"))
                            rb3.IsChecked = true;
                        else if (RBNo.Contains("4"))
                            rb4.IsChecked = true;
                    }
                }
            }
        }

        private void CheckIfAnsweredAll()
        {
            AnsweredQuestions = ListofAnswers.Select(s => s.QuestionNo).ToList();
            UnAnsweredQuestions = DictIndex.Keys.ToList().Except(AnsweredQuestions).ToList();
            score = ListofAnswers.Where(w => w.IsCorrect == true).ToList().Count();

            if (AnsweredQuestions.Count == 11 || _time==TimeSpan.Zero)
            {
                QuizOver();
            }
            else
            {
                var message = string.Join(Environment.NewLine, UnAnsweredQuestions);
                MessageBox.Show( message, "Attend the below questions to complete the test");
            }

        }

    }
}
