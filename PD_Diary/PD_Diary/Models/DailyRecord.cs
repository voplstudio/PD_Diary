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
        public void AddConsumption(MealType mealType, int id, double weight)
        {
            Meals[mealType].Consumptions.Add(new Consumption(id, weight));
        }

        public static DailyRecord GetDailyRecord(DateTime date)
        {
            // TODO: Get data from DB

            DailyRecord dailyRecord = new DailyRecord { Date = date };
            dailyRecord.AddConsumption(MealType.Breakfast, 100, 200);
            dailyRecord.AddConsumption(MealType.Lunch, 200, 100);
            dailyRecord.AddConsumption(MealType.Lunch, 300, 300);
            return dailyRecord;
        }
    }

}
