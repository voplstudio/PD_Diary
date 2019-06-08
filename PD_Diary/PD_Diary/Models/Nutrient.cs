using System;
using System.Collections.Generic;

namespace PD_Diary.Models
{
    public class Nutrient
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<Component> Components { get; set; }

        internal Nutrient Clone()
        {
            Nutrient newItem = new Nutrient();
            newItem.Id = Id;
            newItem.Text = Text;
            newItem.Components = new List<Component>();
            foreach (var item in Components) 
            {
                newItem.Components.Add(item.Clone());       
            }
            return newItem;
        }
    }
}