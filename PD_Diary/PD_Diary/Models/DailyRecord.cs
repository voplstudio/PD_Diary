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
            Meals = new Dictionary<MealType, Meal>();
            Meals.Add(MealType.Breakfast, new Meal() { Id = MealType.Breakfast });
            Meals.Add(MealType.Diner, new Meal() { Id = MealType.Diner });
            Meals.Add(MealType.Lunch, new Meal() { Id = MealType.Lunch });
            Meals.Add(MealType.Supper, new Meal() { Id = MealType.Supper });
        } 
        public void AddConsumption(MealType mealType, string id, double weight)
        {
            Meals[mealType].Consumptions.Add(new Consumption(id, weight));
        }
    }

}
