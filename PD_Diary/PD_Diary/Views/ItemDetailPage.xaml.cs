using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PD_Diary.Models;
using PD_Diary.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using PD_Diary.Services;

namespace PD_Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        public Nutrient _nutrient { get; set; }

        private bool _readOnly = true;
        private Grid grid = null;
        private Editor titleEditor = null;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly == value) return;
                _readOnly = value;

                if (FirstStackLayout != null)
                {
                    if (grid != null)
                    {
                        for (int i = 0; i < grid.RowDefinitions.Count; i++)
                        {
                            Editor editor = (Editor)grid.Children[i * 3 + 1];
                            if (editor != null)
                            {
                                editor.IsEnabled = !_readOnly;
                            }
                        }
                    }
                    if (titleEditor != null)
                    {
                        titleEditor.IsEnabled = !_readOnly;
                    }
                }
                ToolbarItems[1].IsEnabled = !_readOnly;
                ToolbarItems[2].IsEnabled = _readOnly;

            }
        }
        public View GetView(Grid grid, int col, int row)
        {
            foreach (View v in grid.Children) if ((col == Grid.GetColumn(v)) && (row == Grid.GetRow(v))) return v;
            return null;
        }

        public string CategoryName => GetCategoryName();

        private string GetCategoryName()
        {
            if (_nutrient != null)
            {
                string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(App.DATABASE_NAME);
                SQLiteConnection database = new SQLiteConnection(databasePath);
                try
                {
                    database.CreateTable<Nutrient>();
                    CategoryData categoryData = database.Get<CategoryData>(Convert.ToInt32(_nutrient.CategoryID));
                    if (categoryData != null) return categoryData.Name;
                }
                catch(Exception){/* do nothing to return default*/}
                finally
                {
                    database.Close();
                }
            }
            return "без категории";
        }

        public ItemDetailPage(int id)
        {
            InitializeComponent();
            InitPage(Nutrient.Get(id));
        }

        public ItemDetailPage(Nutrient nutrient)
        {
            InitializeComponent();
            InitPage(nutrient);
        }

        private void InitPage(Nutrient nutrient)
        {
            if (nutrient == null)
            {
                _nutrient = Nutrient.GetNew();
            }
            else _nutrient = nutrient.Clone();

            AddComponentsAsGrid();
            MessagingCenter.Subscribe<ItemDetailPage, Nutrient>(this, "RemoveNutrient", async (obj, item) =>
            {
                await App.Database.DeleteItemAsync(_nutrient.Id);
            });
            BindingContext = this;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            InitPage(null);
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
        //public string Title { get; set; } = "test title";
        private void AddComponentsAsGrid()
        {
            if (_nutrient == null || _nutrient.Components == null || _nutrient.Components.Count == 0)
            {
                FirstStackLayout.Children.Add(new Label { Text = "Нет данных" });
                return;
            }
            FirstStackLayout.Children.Add(new Label { Text = "Название продукта" });
            FirstStackLayout.Children.Add(titleEditor = new Editor { Text = _nutrient.Name, IsEnabled = !_readOnly });
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
            for (int i = 0; i < _nutrient.Components.Count; i++)
            {
                Editor editor = (Editor)grid.Children[i * 3 + 1];
                if (double.TryParse(editor.Text, out double result)) _nutrient.Components[i].Per100gramm = result;
            }
            if (titleEditor != null)
                _nutrient.Name = titleEditor.Text;

            if (_nutrient.Id == 0)
            {
                await App.Database.AddItemAsync(_nutrient);
                MessagingCenter.Send(this, "AddNutrientOnList", _nutrient);
            }
            else
            {
                await App.Database.UpdateItemAsync(_nutrient);
                MessagingCenter.Send(this, "UpdateNutrient", _nutrient);
            }
            MessagingCenter.Send(this, "AddNutrient", _nutrient);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        void Edit_Clicked(object sender, EventArgs e)
        {
            ReadOnly = !ReadOnly;
        }

        async private void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "RemoveNutrient", _nutrient);
            await Navigation.PopModalAsync();
        }
    }
}