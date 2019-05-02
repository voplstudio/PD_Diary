using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PD_Diary.Models;
using PD_Diary.ViewModels;
using System.Collections.Generic;

namespace PD_Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        private bool _readOnly = true;
        private Grid grid = null;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (FirstStackLayout != null)
                {
                    if ( grid  != null)
                    {
                        for ( int i = 0; i < grid.RowDefinitions.Count; i++)
                        {
                            Editor editor = (Editor)grid.Children[i*3+1];
                            if ( editor != null)
                            {
                                editor.IsEnabled = !value;
                            }
                        }
                    }
                }
            }
        }
        public View GetView(Grid grid, int col, int row)
        {
            foreach (View v in grid.Children) if ((col == Grid.GetColumn(v)) && (row == Grid.GetRow(v))) return v;
            return null;
        }
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            AddComponentsAsGrid(viewModel.Item);
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Nutrient
            {
                Id = (new Guid("FBFBC500-209F-4A8B-88CF-A6EB8DF01192")).ToString(),
                Text = "Свинина тушенная",
                Components = new List<Component> {
                        new Component{Id = ComponentType.Protein, Per100gramm = 14.9},
                        new Component{Id = ComponentType.Fat, Per100gramm = 32.2},
                        new Component{Id = ComponentType.Carbohydrate, Per100gramm = 0},
                        new Component{Id = ComponentType.Sodium, Per100gramm = 456},
                        new Component{Id = ComponentType.Potassium, Per100gramm = 253},
                        new Component{Id = ComponentType.Phosphates, Per100gramm = 160},
                        new Component{Id = ComponentType.Calories, Per100gramm = 349}
                    }
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;

            AddComponentsAsGrid(item);
        }

        private void AddComponents(Nutrient item)
        {
            for (int i = 0; item.Components != null && i < item.Components.Count; i++)
            {
                FirstStackLayout.Children.Add(new Label()
                {
                    Text = item.Components[i].Id.ToString()
                });
                FirstStackLayout.Children.Add(new Label()
                {
                    Text = item.Components[i].Per100gramm.ToString()
                });
            }
        }

        private void AddComponentsAsGrid(Nutrient nutrient)
        {
            if (nutrient == null || nutrient.Components == null || nutrient.Components.Count == 0)
            {
                FirstStackLayout.Children.Add(new Label { Text = "Нет данных" });
                return;
            }

            grid = new Grid();

            for (int i = 0; i < nutrient.Components.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
            }


            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });

            FirstStackLayout.Children.Add(new Label { Text = "Состав в 100g" });

            for (int i = 0; i < nutrient.Components.Count; i++)
            {
                grid.Children.Add(new Label { Text = nutrient.Components[i].Id.ToString() }, 0, i + 1);
                grid.Children.Add(new Editor { Text = nutrient.Components[i].Per100gramm.ToString(), IsEnabled=!_readOnly }, 1, i + 1);
                grid.Children.Add(new Label { Text = nutrient.Components[i].Id.GetUnits() }, 2, i + 1);
            }
            
            FirstStackLayout.Children.Add(grid);
        }
    }
}