using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lesson1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _questionsBindingSource = ComposeQuestions();
            _formDataModelBindingSource = ComposeFormDataModel();
            SetupQuestionComponents();
        }

        private BindingSource ComposeQuestions()
        {
            var questionsBindingSource = new BindingSource();
            var questions = new BindingList<Question>();
            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                questions.Add(
                    new Question(
                        rand.Next(10), rand.Next(10),
                        rand.Next(2) == 1 ? QuestionOperator.Add : QuestionOperator.Sub));
            }
            questionsBindingSource.DataSource = questions;
            return questionsBindingSource;
        }

        private BindingSource ComposeFormDataModel()
        {
            var formDataBindingSource = new BindingSource();
            formDataBindingSource.DataSource = new FormDataModel();
            return formDataBindingSource;
        }

        private void SetupQuestionComponents()
        {
            SetupUserAnswerTextBox();
            SetupNextButton();
            Formula.DataBindings.Add("Text", _questionsBindingSource, "Formula");
            ResultLabel.DataBindings.Add("Text", _questionsBindingSource, "IsCorrect");
        }

        private void SetupUserAnswerTextBox()
        {
            UserAnswerTextBox.DataBindings.Add("Text", _questionsBindingSource, "UserAnswer", true, DataSourceUpdateMode.OnPropertyChanged);
            Binding binding1 = new("Enabled", _questionsBindingSource, "IsCorrect");
            binding1.Format += new ConvertEventHandler((sender, e) => { if (e.Value is null) return; e.Value = !((bool)e.Value); });
            UserAnswerTextBox.DataBindings.Add(binding1);
        }

        private void SetupNextButton()
        {
            Binding binding = new("Text", _formDataModelBindingSource, "Time");
            binding.Format += new ConvertEventHandler((sender, e) => { if (e.Value is null) return; e.Value = $"Next ({e.Value})"; });
            NextButton.DataBindings.Add(binding);
        }

        private BindingSource _questionsBindingSource;
        private BindingSource _formDataModelBindingSource;
        private FormDataModel _formDataModel { get { return ((FormDataModel)_formDataModelBindingSource.Current); } }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_questionsBindingSource.Position < _questionsBindingSource.Count - 1)
            {
                _questionsBindingSource.MoveNext();
                _formDataModel.Time = FormDataModel.InitialTime;
            }
            else
            {
                int correctCount = 0;
                foreach (Question question in _questionsBindingSource)
                {
                    if (question.IsCorrect) correctCount++;
                }
                AnswerTimer.Stop();
                MessageBox.Show($"Total: {_questionsBindingSource.Count}, Correct: {correctCount}");
            }
        }

        private void AnswerTimer_Tick(object sender, EventArgs e)
        {
            _formDataModel.Time--;
            if (_formDataModel.Time == 0) NextButton.PerformClick();
        }
    }

    internal class FormDataModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static int InitialTime = 10;

        public FormDataModel()
        {
            _time = InitialTime;
        }

        private int _time;
        public int Time { get { return _time; } set { _time = value; NotifyPropertyChanged(); } }
    }
}