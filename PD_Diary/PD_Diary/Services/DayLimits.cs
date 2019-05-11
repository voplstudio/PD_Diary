using System;
using System.Collections.Generic;
using PD_Diary.Models;
using System.Text;

namespace PD_Diary.Services
{
    public class DayLimits
    {
        public Dictionary<ComponentType, double> Components;
        public DayLimits()
        {
            Components = new Dictionary<ComponentType, double>();
            Components.Add(ComponentType.Protein, 35);
            Components.Add(ComponentType.Fat, -1);
            Components.Add(ComponentType.Carbohydrate, -1);
            Components.Add(ComponentType.Sodium, 1500);
            Components.Add(ComponentType.Potassium, 2000);
            Components.Add(ComponentType.Phosphates, 1000);
            Components.Add(ComponentType.Calcium, -1);
            Components.Add(ComponentType.Calories, 3000);
            Components.Add(ComponentType.Water, 1000);
        }
    }
}
