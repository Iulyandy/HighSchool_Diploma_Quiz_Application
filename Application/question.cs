using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat
{
    class question
    {
        private int id;
        private string prompt, a, b, c, d, correct, imagne, answer;

        public int ID { set { id = value; } get { return id; } }
        public string Prompt { set { prompt = value; } get { return prompt; } }
        public string A { set { a = value; } get { return a; } }
        public string B { set { b = value; } get { return b; } }
        public string C { set { c = value; } get { return c; } }
        public string D { set { d = value; } get { return d; } }
        public string Correct { set { correct = value; } get { return correct; } }
        public string Image { set { imagne = value; } get { return imagne; } }
        public string Answer { set { answer = value; } get { return answer; } }

        public question() { }
        public question(int id, string prompt, string a, string b, string c, string d, string correct, string image)
        {
            ID = id;
            Prompt = prompt;
            A = a; B = b; C = c; D = d;
            Correct = correct;
            Image = image;
        }
    }
}
