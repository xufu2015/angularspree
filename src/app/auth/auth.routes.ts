import { UpdatePasswordComponent } from './components/update-password/update-password.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { LoginComponent } from './components/login/login.component';
import { ForgetPasswordComponent } from './components/forget-password/forget-password.component';

export const AuthRoutes = [
  { path: '', redirectTo: 'signup', pathMatch: 'full' },
  { path: 'signup', component: SignUpComponent },
  { path: 'login', component: LoginComponent },
  { path: 'recover', component: ForgetPasswordComponent},
  { path: 'updatePassword', component: UpdatePasswordComponent}
];


  public class DateHelpTest
  {
    [Fact]
    public void GetDaysInMonthShouldReturn_12_Items()
    {
      var totalMonths = DateHelper.GetMonthNumberOfDays().Count();
      Assert.Equal(12, totalMonths);
    }

    [Theory]
    [InlineData(1, 31)]
    [InlineData(2, 28)]
    [InlineData(3, 31)]
    [InlineData(4, 30)]
    [InlineData(5, 31)]
    [InlineData(6, 30)]
    [InlineData(7, 31)]
    [InlineData(8, 31)]
    [InlineData(9, 30)]
    [InlineData(10, 31)]
    [InlineData(11, 30)]
    [InlineData(12, 31)]
    public void GetDaysInMonthShould_Return_CorrectDaysForMonth(int month, int day)
    {
      var expectedDay = DateHelper.GetMonthNumberOfDays().SingleOrDefault(x => x.Item1 == month)?.Item2;

      Assert.Equal(expectedDay, day);
    }

    [Theory]
    [InlineData(1, 33)]
    [InlineData(2, 29)]
    [InlineData(3, 33)]
    [InlineData(4, 33)]
    [InlineData(5, 33)]
    [InlineData(6, 33)]
    [InlineData(7, 33)]
    [InlineData(8, 33)]
    [InlineData(9, 33)]
    [InlineData(10, 33)]
    [InlineData(11, 33)]
    [InlineData(12, 33)]
    public void GetDaysInMonthShould_Return_InCorrectDaysForMonth(int month, int day)
    {
      var calDay = DateHelper.GetMonthNumberOfDays().SingleOrDefault(x => x.Item1 == month)?.Item2;
      Assert.NotEqual(calDay, day);
    }

    [Theory]
    [InlineData(0, 33)]
    [InlineData(-1, 29)]
    [InlineData(13, 33)]
    public void GetDaysInMonth_CanNotBeFound_When_MonthOutOfRange(int month, int day)
    {
      var expectedDay = DateHelper.GetMonthNumberOfDays().SingleOrDefault(x => x.Item1 == month)?.Item2;
      Assert.Null(expectedDay);
    }

    [Theory]
    [InlineData(2000, 366)]
    [InlineData(2018, 365)]
    public void GetNumberOfDaysForaYear_Should_ReturnCorrectDays(int year, int estimatedDays)
    {
      var days = DateHelper.GetNumberOfDaysForaYear(year);
      Assert.Equal(days, estimatedDays);
    }

    [Theory]
    [InlineData(1999, 2019, 7305)]
    [InlineData(1999, 2014, 5479)]
    public void CalculateDaysBetweenyear(int year1, int year2, int numberofDays)
    {
    var days=  DateHelper.GetDaysDifferenceBetweenYears(year1, year2);
      Assert.Equal(days, numberofDays);
    }

    [Theory]
    [InlineData(2018, 7, 4, 185)]
    [InlineData(2018, 3, 1, 60)]
    [InlineData(2018, 2, 1, 32)]
    public void CalculateDaysInYear(int year, int month, int day,int days)
    {
      MyDate myDate=new MyDate(day, month, year);
      var caldays = DateHelper.GetNumberofDaysInaYear(myDate);
      Assert.Equal(caldays, days);
    }

    [Theory, ClassData(typeof(SampleTestData))]
    public void CalculateDayDifferenceBetweenDays(MyDate date1, MyDate date2,int numberofDays)
    {
      var calDays = new DateHelper().GetDateDifference(date1, date2);
      Assert.Equal(numberofDays, calDays);
    }

    [Fact]
    public void DateHelp_CheckDateIsValid()
    {
      var myDate=new MyDate(1,1,-1);
      Assert.False(new DateHelper().IsValidDate(myDate));
      var myDate2 = new MyDate(29, 2, 2018);
      Assert.False(new DateHelper().IsValidDate(myDate2));
    }
    private class SampleTestData:IEnumerable<object[]>
    {
      private readonly List<object[]> _data=new List<object[]>
      {
        new object[]{new MyDate(1,1,1999), new MyDate(30, 8, 2014), -5720 },
        new object[]{new MyDate(1,2,2018), new MyDate(1, 1, 2018), 31 },
        new object[]{new MyDate(2,1,2018), new MyDate(1, 1, 2018), 1 }
      };
      public IEnumerator<object[]> GetEnumerator()
      { return _data.GetEnumerator(); }

      IEnumerator IEnumerable.GetEnumerator()
      { return GetEnumerator(); }
    }



  }
  
  
      public class CategoryServiceTest
    {
        private ICategoryService _categoryService;
        private Mock<IRepository<Category>> _categoryRespMock;
         
        public CategoryServiceTest()
        {
           
            var category1 = new Category()
            {
                Name = "Category1",
                Id = 1
            };
            var category2 = new Category()
            {
                Id = 2,
                Name = "Category2"
            };
            _categoryRespMock = new Mock<IRepository<Category>>();
            _categoryRespMock.Setup(x => x.Table).Returns(new List<Category> {category1, category2}.AsQueryable());

            _categoryService = new CategoryService(null, null, _categoryRespMock.Object);

        }

        [Fact]
        public void DeleteCategoryTest()
        {
            //var category = new Category()
            //{
            //    Name = "Category3",
            //    Id = 3
            //};
            Category category = null;
         Action act=()=>   _categoryService.DeleteCategory(category);
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void GetCategoryById_Test()
        {
            var cat = _categoryService.GetCategoryById(1);
            Assert.NotNull(cat);
        }
    }
