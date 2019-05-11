using System.Collections.Generic;

namespace PD_Diary.Models
{
    public class Meal
    {
        public List<Consumption> Consumptions = new List<Consumption>();
        public MealType Id;
    }
}