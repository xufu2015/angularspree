using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Attributes;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LookupAttribute : Attribute
    {
        public Type LookupType { get; private set; }

        public LookupAttribute(Type lookupType)
        {
            this.LookupType = lookupType;
        }

        public dynamic GetList()
        {
            if (typeof(BaseCar).IsAssignableFrom(this.LookupType))
            {
                var result = typeof(LookupAttribute).Assembly.GetTypes()
                    .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                t.BaseType.GetGenericTypeDefinition() == typeof(Repository<,>) &&
                                t.BaseType.GetGenericArguments()[0].Name == LookupType.Name).FirstOrDefault();
                if (result != null)
                {
                dynamic repository = Activator.CreateInstance(result);
                return repository.GetList();
                }

                return null;
            }
            else
            {
                return null;
            }
        }
    }

    public class BaseCar
    {

    }
    public class BaseModel
    {

    }
    public class Car1: BaseCar
    {

    }
    public class Car2: BaseCar
    {

    }
    public class Car1Model: BaseModel
    {

    }
    public class Car2Model: BaseModel
    {

    }

    public class Repository<T, M>
        where T : BaseCar
        where M : BaseModel
    {
        public virtual List<M> GetList()
        {
            return new List<M>();
        }
    }

    public class Car1Inter : Repository<Car1, Car1Model>
    {
        public List<Car1Model> GetList()
        {
            return new List<Car1Model>()
            {
                new Car1Model(),
                new Car1Model()
            };
        }
    }

    public class Car2Inter : Repository<Car2, Car2Model>
    {

    }
}


public class Sample1
{
    public string Field1 { get; set; }

    [Lookup(typeof(Car1))]
    public int  CardId { get; set; }
}