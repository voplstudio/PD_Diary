using PD_Diary.Services;
using SQLite;

namespace PD_Diary.Views
{
    [Table("Category")]
    internal class CategoryData : INamed
    {
        [PrimaryKey, AutoIncrement, Column("CategoryID")]
        public int Id { get; set; }
        [Column("Name")]    
        public string Name { get; set; }
    }
}