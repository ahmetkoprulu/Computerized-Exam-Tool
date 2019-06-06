using System;
using System.Collections.Generic;
using System.Text;

namespace Cet.Entities.Complex
{
    public class ComplexQuestion
    {
        public ComplexQuestion()
        {

        }

        public string Text { get; set; }
        public int Num { get; set; }
        public string Extra { get; set; }
        public string AnswerText { get; set; }
    }
}
