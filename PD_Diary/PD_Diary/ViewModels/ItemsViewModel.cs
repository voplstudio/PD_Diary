using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using PD_Diary.Models;
using PD_Diary.Views;
using System.Linq;

namespace PD_Diary.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Nutrient> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Nutrient>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemDetailPage, Nutrient>(this, "AddNutrientOnList", (obj, item) =>
           {
               var newItem = item as Nutrient;
               Items.Add(newItem);
                //await DataStore.AddItemAsync(newItem);
            });
            MessagingCenter.Subscribe<ItemDetailPage, Nutrient>(this, "RemoveNutrient", (obj, item) =>
           {
               var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);
               if (foundItem != null)
                   Items.Remove(foundItem); 
               //await DataStore.DeleteItemAsync(newItem);
           });
            MessagingCenter.Subscribe<ItemDetailPage, Nutrient>(this, "UpdateNutrient", (obj, item) =>
            {
                var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);
                if (foundItem != null)
                    Items.Remove(foundItem);
                Items.Add(item);
                //await DataStore.UpdateItemAsync(newItem);
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}