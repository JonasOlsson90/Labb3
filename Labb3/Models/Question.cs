using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3.Models
{
    class Question
    {
        public string Statement { get; }
        public string[] Answers { get; }
        public int CorrectAnswer { get; } //ToDO: Fråga om den verkligen ska ha readonly key word.
        public string ImagePath { get; }
    }
}
