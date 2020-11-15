namespace Term_Paper_Rudenko
{
    public class PasswordRecovery
    {
        private string _username;
        private string _question;
        private string _answer;

        public static string[] Questions = { "What Is your favorite book?",
                                            "What is the name of the road you grew up on?",
                                            "What is your mother's maiden name?",
                                            "What was the name of your first/current/favorite pet?",
                                            "What was the first company that you worked for?",
                                            "Where did you meet your spouse?",
                                            "Where did you go to high school/college?" };

        public PasswordRecovery()
        {
            _question = "";
            _answer = "";
        }

        public PasswordRecovery(string username, string question, string answer)
        {
            _username = username;
            _question = question;
            _answer = answer;
        }

        public bool Check(string question, string answer)
        {
            if (question == _question && answer == _answer)
                return true;
            else return false;
        }

        public string Question { get => _question; set => _question = value; }

        public string Answer { get => _answer; set => _answer = value; }

        public string Username { get => _username; set => _username = value; }
    }
}
