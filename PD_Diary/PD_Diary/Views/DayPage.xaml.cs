using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PD_Diary.Services;
using PD_Diary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PD_Diary.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayPage : ContentPage
	{
		public DayPage ()
		{
			InitializeComponent ();
            DailyRecord dailyRecord = new MockDataStore().GetDailyRecord(DateTime.Now);
            ApplyDailyRecord(dailyRecord);
        }
        public void ApplyDailyRecord(DailyRecord dailyRecord)
        {
            ChosenDateLabel.Text = dailyRecord.Date.ToShortDateString();
            FillSummaryGrid(dailyRecord);
            FillMealGrid(dailyRecord);
        }

        private void FillMealGrid(DailyRecord dailyRecord)
        {
            int rowIdx = 0;
            foreach (Meal meal in dailyRecord.Meals)
            {

                MealGrid.Children.Add(new Label() { Text = meal.Id.ToString() }, 0, rowIdx++);
                foreach (Consumption consumption in meal.Consumptions)
                {
                    MealGrid.Children.Add(new Label() { Text = "    " + new MockDataStore().GetItemAsync(consumption.Id).Result.Text}, 0, rowIdx);
                    MealGrid.Children.Add(new Label() { Text = consumption.Weight.ToString(), HorizontalTextAlignment = TextAlignment.End }, 1, rowIdx);
                    MealGrid.Children.Add(new Label() { Text = "g" }, 2, rowIdx++);
                }
            }
        }

        private void FillSummaryGrid(DailyRecord dailyRecord)
        {
            Dictionary<ComponentType, double> components = new Dictionary<ComponentType, double>();
            foreach (Meal meal in dailyRecord.Meals)
            {
                foreach (Consumption consumption in meal.Consumptions)
                {
                    Nutrient nutrient = new MockDataStore().GetItemAsync(consumption.Id).Result;
                    foreach (Component component in nutrient.Components)
                    {
                        double addition = component.Per100gramm * consumption.Weight / 100.0;
                        if (components.ContainsKey(component.Id) )
                        {
                            components[component.Id] += addition;
                        }
                        else
                        {
                            components.Add(component.Id, addition);
                        }
                    }
                }
            }
            int rowIdx = 0;
            foreach (ComponentType componentType in components.Keys)
            {
                SummaryGrid.Children.Add(new Label() { Text = componentType.ToString() }, 0, rowIdx);
                SummaryGrid.Children.Add(new Label() { Text = components[componentType].ToString(), HorizontalTextAlignment = TextAlignment.End }, 1, rowIdx);
                SummaryGrid.Children.Add(new Label() { Text = componentType.GetUnits() }, 2, rowIdx);
                rowIdx++;
            }
        }
    }
}