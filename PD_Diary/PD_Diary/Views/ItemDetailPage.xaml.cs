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
        public Nutrient _nutrient;

        private bool _readOnly = true;
        private Grid grid = null;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (FirstStackLayout != null)
                {
                    if (grid != null)
                    {
                        for (int i = 0; i < grid.RowDefinitions.Count; i++)
                        {
                            Editor editor = (Editor)grid.Children[i * 3 + 1];
                            if (editor != null)
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
        public ItemDetailPage(Nutrient nutrient)
        {
            InitializeComponent();

            _nutrient = nutrient;

            if (_nutrient == null)
            {
                _nutrient = NewNutrient();
            }

            AddComponentsAsGrid();
            MessagingCenter.Subscribe<ItemDetailPage, Nutrient>(this, "AddNutrient", async (obj, item) =>
            {
                for (int i = 0; i < _nutrient.Components.Count; i++)
                {
                    Editor editor = (Editor)grid.Children[i * 3 + 1];
                    double result = 0;
                    if (Double.TryParse(editor.Text, out result)) _nutrient.Components[i].Per100gramm = result;
                }

                await new Services.MockDataStore().AddItemAsync(_nutrient);
            });
        }

        private static Nutrient NewNutrient()
        {
            return new Nutrient()
            {
                Id = Guid.NewGuid().ToString(),
                Components = new List<Component> {
                        new Component{Id = ComponentType.Protein, Per100gramm = 0},
                        new Component{Id = ComponentType.Fat, Per100gramm = 0},
                        new Component{Id = ComponentType.Carbohydrate, Per100gramm = 0},
                        new Component{Id = ComponentType.Sodium, Per100gramm = 0},
                        new Component{Id = ComponentType.Potassium, Per100gramm = 0},
                        new Component{Id = ComponentType.Phosphates, Per100gramm = 0},
                        new Component{Id = ComponentType.Calories, Per100gramm = 0}
                    }
            };
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            _nutrient = new Services.MockDataStore().GetItemAsync("FBFBC500-209F-4A8B-88CF-A6EB8DF01192").Result;

            AddComponentsAsGrid();
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

        private void AddComponentsAsGrid()
        {
            if (_nutrient == null || _nutrient.Components == null || _nutrient.Components.Count == 0)
            {
                FirstStackLayout.Children.Add(new Label { Text = "Нет данных" });
                return;
            }

            grid = new Grid();

            for (int i = 0; i < _nutrient.Components.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
            }


            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });

            FirstStackLayout.Children.Add(new Label { Text = "Состав в 100g" });

            for (int i = 0; i < _nutrient.Components.Count; i++)
            {
                grid.Children.Add(new Label { Text = _nutrient.Components[i].Id.ToString() }, 0, i + 1);
                grid.Children.Add(new Editor { Text = _nutrient.Components[i].Per100gramm.ToString(), IsEnabled = !_readOnly }, 1, i + 1);
                grid.Children.Add(new Label { Text = _nutrient.Components[i].Id.GetUnits() }, 2, i + 1);
            }

            FirstStackLayout.Children.Add(grid);
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddNutrient", _nutrient);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}