using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lesson1_2
{
    internal class Question : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Question(int number1, int number2, QuestionOperator @operator)
        {
            Number1 = number1; Number2 = number2; Operator = @operator;
        }

        public int Number1 { get; }
        public int Number2 { get; }
        public QuestionOperator Operator { get; }
        public string formula { get { return $"{Number1} {Operator.ToSymbolString()} {Number2}"; } }
        public int Answer
        {
            get
            {
                return Operator switch
                {
                    QuestionOperator.Add => Number1 + Number2,
                    QuestionOperator.Sub => Number1 - Number2,
                    _ => throw new NotImplementedException(),
                };
            }
        }
        private int _userAnswer = 0;
        public string UserAnswer
        {
            get
            {
                return _userAnswer.ToString();
            }
            set
            {
                try { _userAnswer = Int32.Parse(value); }
                catch { return; }
                NotifyPropertyChanged();
                Answered = true;
            }
        }
        public bool IsCorrect
        {
            get { return _userAnswer == Answer; }
        }
        private bool _answered = false;
        public bool Answered
        {
            get { return _answered; }
            private set { _answered = value; NotifyPropertyChanged(); }
        }
    }

    internal enum QuestionOperator
    {
        Add, Sub
    }

    internal static class QuestionOperatorExtension
    {
        public static string ToSymbolString(this QuestionOperator questionOperator)
        {
            return questionOperator switch
            {
                QuestionOperator.Add => "+",
                QuestionOperator.Sub => "-",
                _ => "",
            };
        }
    }
}