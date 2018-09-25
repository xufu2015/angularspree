using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Common.Test
{
    public class UT1
    {

        [Fact]
        public void LookupAttribute_GetList()
        {
            Assert.Equal(2,(new LookupAttribute(typeof(Car1)).GetList() as List<Car1Model>).Count);
        }

        [Fact]
        public void LookupAttribute_GetList2()
        {
            Assert.Equal(0, (new LookupAttribute(typeof(Car2)).GetList() as List<Car2Model>).Count);
        }

        [Fact]
        public void IsAssignAble()
        {
            var type = typeof(Car1);
           Assert.True(typeof(BaseCar).IsAssignableFrom(type));

        }
        [Fact]
        public void Can_Get_Generic_Types()
        {
            var result = typeof(LookupAttribute).Assembly.GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(Repository<,>) &&
                            t.BaseType.GetGenericArguments()[0].Name == "Car1").FirstOrDefault();

            dynamic repository = Activator.CreateInstance(result) ;
           List<Car1Model> cc= repository.GetList();
          
        }

        [Fact]
        public void Can_Get_Property_Attribute()
        {
            var sample=new Sample1();
            var sampelTypes = typeof(Sample1).GetProperties().SelectMany(
            x => x.GetCustomAttributes(typeof(LookupAttribute), false));
            Assert.Equal(1, sampelTypes.Count());
            var ctype = sampelTypes.First();
            var lookuptype = ((LookupAttribute) ctype).LookupType;



            var result = typeof(LookupAttribute).Assembly.GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(Repository<,>) &&
                            t.BaseType.GetGenericArguments()[0].FullName == lookuptype.FullName).FirstOrDefault();

            dynamic repository = Activator.CreateInstance(result);
            List<Car1Model> cc = repository.GetList();
            Assert.Equal(cc.Count, 2);



            //dynamic repository = Activator.CreateInstance(lookuptype);
            //List<Car1Model> cc = repository.GetList();
            //List < Car1Model > cc= repository.GetList();
            //Assert.Equal(cc.Count, 2);
            //.Where(x => x.GetCustomAttributes(typeof(LookupAttribute), false).Length > 0);

            // var attr = (LookupAttribute[])pi.GetCustomAttributes(typeof(LookupAttribute), false);


            //var list=(LookupAttribute)sampelType.First()

        }
    }
}
