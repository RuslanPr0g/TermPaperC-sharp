using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class SpentTimeOnLecture
    {
        private string _username;
        private int _lectureID;
        private int seconds;

        public SpentTimeOnLecture()
        {
            _username = "";
            seconds = 0;
            _lectureID = 0;
        }

        public SpentTimeOnLecture(string u, int i, int s)
        {
            _username = u;
            _lectureID = i;
            seconds = s;
        }

        public string String()
        {
            return _username + "\t" + _lectureID + "\t" + seconds;
        }

        public static double AverageTimeSpentOnLectures(string username, List<SpentTimeOnLecture> lectures)
        {
            double sum = 0;
            double count = 0;

            foreach (SpentTimeOnLecture lecture in lectures)
            {
                if (lecture.Username == username)
                {
                    sum += lecture.Seconds;

                    count++;
                }
            }

            if (count > 0)
                return sum / count;
            else
                return 0;
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
            }
        }
        public int LectureID
        {
            get
            {
                return _lectureID;
            }

            set
            {
                _lectureID = value;
            }
        }
        public int Seconds
        {
            get
            {
                return seconds;
            }

            set
            {
                seconds = value;
            }
        }
    }
}
