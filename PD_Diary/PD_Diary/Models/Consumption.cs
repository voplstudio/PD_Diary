namespace PD_Diary.Models
{
    public class Consumption
    {
        public string Id;
        public double Weight;

        public Consumption(string id, double weight)
        {
            Id = id;
            Weight = weight;
        }
    }
}