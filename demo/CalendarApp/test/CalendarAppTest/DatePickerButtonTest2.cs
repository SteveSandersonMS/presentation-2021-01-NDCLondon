using System;
using Bunit;
using CalendarApp.Pages.Home;
using Xunit;
using static Bunit.ComponentParameterFactory;

namespace CalendarApp
{
    public class DatePickerButtonTest : TestContext
    {
        [Fact]
        public void RendersMinimumDate()
        {
            var minDate = DateTime.Now.Date.AddDays(3);
            var result = RenderComponent<DatePickerButton>(
                ("Min", minDate)
            );

            var input = result.Find("input[type=date]");
            Assert.Equal(
                minDate.ToString("yyyy-MM-dd"), 
                input.GetAttribute("min"));
        }
    }
}
