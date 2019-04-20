using System;
using System.Linq;

namespace PD_Diary.Models
{
    static class EnumExtension
    {
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }

        public static string GetUnits(this ComponentType id)
        {
            if (id.In(ComponentType.Fat, ComponentType.Protein, ComponentType.Water, ComponentType.Carbohydrate))
                return "g";
            else if (id == ComponentType.Calories) return "kCal";
            else return "mg";
        }
    }
}
