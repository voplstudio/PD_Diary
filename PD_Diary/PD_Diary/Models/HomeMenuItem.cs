using System;
using System.Collections.Generic;
using System.Text;

namespace PD_Diary.Models
{
    public enum MenuItemType
    {
        DaySummary,
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
