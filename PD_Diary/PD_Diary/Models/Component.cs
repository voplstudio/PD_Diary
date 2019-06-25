using System;

namespace PD_Diary.Models
{
    public class Component
    {
        public ComponentType Id;
        public double Per100gramm;

        internal Component Clone()
        {
            var newItem = new Component
            {
                Id = Id,
                Per100gramm = Per100gramm
            };
            return newItem;
        }
    }
}