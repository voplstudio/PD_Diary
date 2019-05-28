using System;
using System.Collections.Generic;
using System.Text;

namespace PD_Diary.Models
{
    public class DailyRecord
    {
        public Dictionary<MealType, Meal> Meals;
        public DateTime Date;
        public DailyRecord()
        {
            Meals = new Dictionary<MealType, Meal>
            {
                { MealType.Breakfast, new Meal() { Id = MealType.Breakfast } },
                { MealType.Diner, new Meal() { Id = MealType.Diner } },
                { MealType.Lunch, new Meal() { Id = MealType.Lunch } },
                { MealType.Supper, new Meal() { Id = MealType.Supper } }
            };
        } 
        public void AddConsumption(MealType mealType, string id, double weight)
        {
            Meals[mealType].Consumptions.Add(new Consumption(id, weight));
        }
    }

}
