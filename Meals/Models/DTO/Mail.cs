using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class EMail
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string AppendixPath { get; set; }


    }
}
