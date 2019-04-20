using System;
using System.Collections.Generic;

namespace PD_Diary.Models
{
    public class Nutrient
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public List<Component> Components { get; set; }
    }
}