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
            Components = new Dictionary<ComponentType, double>
            {
                { ComponentType.Protein, 35 },
                { ComponentType.Fat, -1 },
                { ComponentType.Carbohydrate, -1 },
                { ComponentType.Sodium, 1500 },
                { ComponentType.Potassium, 2000 },
                { ComponentType.Phosphates, 1000 },
                { ComponentType.Calcium, -1 },
                { ComponentType.Calories, 3000 },
                { ComponentType.Water, 1000 }
            };
        }
    }
}
