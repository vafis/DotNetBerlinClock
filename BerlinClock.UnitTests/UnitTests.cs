using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace BerlinClock.UnitTests
{
    public class Fixture : IDisposable
    {
        public ITimeConverter TimeConverter => new TimeConverter();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    public class UnitTests : IClassFixture<Fixture>
    {
        ITimeConverter sut;
        public UnitTests(Fixture fixture)
        {
            sut = fixture.TimeConverter;
        }

        [Theory]
        [InlineData("24:00:00", "Y\r\nRRRR\r\nRRRR\r\nOOOOOOOOOOO\r\nOOOO")]                              
        [InlineData("00:00:00", "Y\r\nOOOO\r\nOOOO\r\nOOOOOOOOOOO\r\nOOOO")]
        [InlineData("13:17:01", "O\r\nRROO\r\nRRRO\r\nYYROOOOOOOO\r\nYYOO")]
        [InlineData("23:59:59", "O\r\nRRRR\r\nRRRO\r\nYYRYYRYYRYY\r\nYYYY")]
        public void Convertime_Test(string time, string expected)
        {           
            Assert.Equal(expected, sut.convertTime(time));
        }
        [Theory]
        [InlineData("23:11:ss", typeof(ArgumentException))]
        [InlineData("23:59:66", typeof(ArgumentOutOfRangeException))]
        public void time_string_input_parameter_seconds_validation(string time, Type type)
        {
            Assert.Throws(type, () => sut.ValidateSeconds(time));
        }
        [Theory]
        [InlineData("23:1s:01", typeof(ArgumentException))]
        [InlineData("23:66:59", typeof(ArgumentOutOfRangeException))]
        public void time_string_input_parameter_minutes_validation(string time, Type type)
        {
            Assert.Throws(type, () => sut.ValidateMinutes(time));
        }
        [Theory]
        [InlineData("a1:17:01", typeof(ArgumentException))]
        [InlineData("25:59:59", typeof(ArgumentOutOfRangeException))]
        [InlineData("", typeof(ArgumentNullException))]
        public void time_string_input_parameter_hours_validation(string time, Type type)
        {
            Assert.Throws(type, ()=> sut.ValidateHour(time));
        }

    }
}
