using Newtonsoft.Json;
using PD_Diary.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PD_Diary.Models
{
    [AttributeUsage(AttributeTargets.All)]
    public class ComponentTypeAttribute : Attribute
    {
        // Private fields.
        private readonly ComponentType componentType;

        public virtual ComponentType CType
        {
            get { return componentType; }
        }
        public ComponentTypeAttribute(ComponentType cType)
        {
            componentType = cType;
        }
    }

    [Table("Product")]
    public class Nutrient: INamed
    {
        [PrimaryKey, AutoIncrement, Column("ProductID")]
        public int Id { get; set; } = 0;
        [Column("Name")]
        public string Name { get; set; } 
        [Column("Protein"), ComponentType(ComponentType.Protein)]
        public double Protein
        { get { return GetComponent(GetComponentType()).Per100gramm; } set { SetComponent(GetComponentType(), value); } }

        [Column("Fat"), ComponentType(ComponentType.Fat)]
        public double Fat
        { get { return GetComponent(GetComponentType()).Per100gramm; } set { SetComponent(GetComponentType(), value); } }

        [Column("Carbohydrate"), ComponentType(ComponentType.Carbohydrate)]
        public double Carbohydrate
        { get { return GetComponent(GetComponentType()).Per100gramm; } set { SetComponent(GetComponentType(), value); } }

        [Column("Sodium"), ComponentType(ComponentType.Sodium)]
        public double Sodium
        { get { return GetComponent(GetComponentType()).Per100gramm; } set { SetComponent(GetComponentType(), value); } }
        [Column("Potassium"), ComponentType(ComponentType.Potassium)]
        public double Potassium
        { get { return GetComponent(GetComponentType()).Per100gramm; } set { SetComponent(GetComponentType(), value); } }
        [Column("Phosphorus"), ComponentType(ComponentType.Phosphates)]
        public double Phosphates
        { get { return GetComponent(GetComponentType()).Per100gramm; } set { SetComponent(GetComponentType(), value); } }
        [Column("Energy"), ComponentType(ComponentType.Calories)]
        public double Calories
        { get { return GetComponent(GetComponentType()).Per100gramm; } set { SetComponent(GetComponentType(), value); } }
        [Column("CategoryID")]
        public long CategoryID { get; set; }

        public List<Component> Components = new List<Component>();

        public void SetComponent(ComponentType ctype, double value)
        {
            GetComponent(ctype).Per100gramm = value;
        }

        private static ComponentType GetComponentType([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            var pInfo = typeof(Nutrient).GetProperty(memberName)
                             .GetCustomAttribute<ComponentTypeAttribute>();
            return pInfo.CType;
        }

        internal static Nutrient Get(int id)
        {
            return new NutrientRepository(App.DATABASE_NAME).GetItem(id);
        }

        public static Nutrient GetNew()
        {
            return new Nutrient()
            {
                Id = 0,
                Protein = 0,
                Fat = 0,
                Carbohydrate = 0,
                Sodium = 0,
                Potassium = 0,
                Phosphates = 0,
                Calories = 0
            };
        }

        public Component GetComponent(ComponentType ctype)
        {
            var component = Components.Find(x => x.Id == ctype);
            if (component == null)
            {
                component = new Component() { Id = ctype, Per100gramm = 0 };
                Components.Add(component);
            }
            return component;
        }

        internal Nutrient Clone()
        {
            Nutrient newItem = new Nutrient
            {
                Id = Id,
                Name = Name,
                Components = new List<Component>(),
                CategoryID = CategoryID
            };
            foreach (var item in Components)
            {
                newItem.Components.Add(item.Clone());
            }
            return newItem;
        }
    }
}