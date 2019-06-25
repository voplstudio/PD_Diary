namespace PD_Diary.Models
{
    public class Consumption
    {
        public int Id;
        public double Weight;

        public Consumption(int id, double weight)
        {
            Id = id;
            Weight = weight;
        }
    }
}