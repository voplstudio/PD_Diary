using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PD_Diary.Views;
using PD_Diary.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PD_Diary
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "data.sqlite";
        private static NutrientRepository database;

        public static NutrientRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new NutrientRepository(DATABASE_NAME);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
