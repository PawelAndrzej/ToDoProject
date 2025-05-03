using ToDoWebApplication.Helper;
using ToDoWebApplication.Models;
using ToDoWebApplication.Settings;

namespace ToDoXUnitTest
{
    public class ToDoConrollerTest
    {
        //Test current week dates
        [Fact]
        public void CurrentWeekTest()
        {
            var currentWeek = Helper.CurrentWeek;
            Assert.Equal(DayOfWeek.Monday, currentWeek.Item1.DayOfWeek);
            Assert.Equal(DayOfWeek.Sunday, currentWeek.Item2.DayOfWeek);
        }
        //Test default data model
        [Fact]
        public void ToDoModelDefaultValueTest()
        {
            ToDoModel model = new ToDoModel();
            Assert.Equal(Setting.ToDoDefaultTitle, model.Title);
            Assert.Equal(Setting.ToDoDefaultExpiryDateTime, model.ExpiryDateTime);
            Assert.Equal(Setting.ToDoDefaultComplete, model.Complete);
            Assert.Equal(Setting.ToDoDefaultDescription, model.Description);
        }
        //Test filter data model
        [Fact]
        public void ToDoModelFilterDefaultValueTest()
        {
            ToDoModelFilter model = new ToDoModelFilter();
            Assert.Equal(ExpireDateType.All, model.ExpireDateType);
        }
    }
}