using System;

using PD_Diary.Models;

namespace PD_Diary.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Nutrient Item { get; set; }
        public ItemDetailViewModel(Nutrient item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
