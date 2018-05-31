using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

// KONSTANTINOS VAFEIADAKIS
// vafeiadakis@valuesoft.eu

//************ IMPORTANT NOTE ************************************
// IF YOU HAVE RESHARPER INSTALLED AT YOUR MACHINE YOU NEED TO DOWNLOAD
// THE XUNIT PLUGIN (ReSharper → Extension Manager -> xUnit ) IN ORDER TO RUN 
// UNIT TESTS
// IF YOU DISABLE RESHARPER MAYBE YOU CAN RUN THE UNIT TESTS
// YO!
//******************************************************************
namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            // *** Validation ***
            int hours = this.ValidateHour(aTime);
            int minutes = this.ValidateMinutes(aTime);
            int seconds = this.ValidateSeconds(aTime);
            // -----End of Validation ---


            List<Expression> trees = new List<Expression>()
                {   //Seconds /flash
                    Expression.Condition(Expression.Constant(seconds % 2 == 0),Expression.Constant("Y"), Expression.Constant("O")),
                    //hours
                    Expression.Condition(Expression.Constant(hours >= 5),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(hours >= 10),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(hours >= 15),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(hours >= 20),Expression.Constant("R"), Expression.Constant("O")),
                    //hour
                    Expression.Condition(Expression.Constant(hours % 5 >= 1),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(hours % 5 >= 2),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(hours % 5 >= 3),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(hours % 5 >= 4),Expression.Constant("R"), Expression.Constant("O")),
                    //minutes
                    Expression.Condition(Expression.Constant(minutes >= 5),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 10),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 15),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 20),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 25),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 30),Expression.Constant("R"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 35),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 40),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 45),Expression.Constant("R"), Expression.Constant("O")),
                     Expression.Condition(Expression.Constant(minutes >= 50),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant(minutes >= 55),Expression.Constant("Y"), Expression.Constant("O")),
                    //minute
                    Expression.Condition(Expression.Constant((minutes % 5) >= 1),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant((minutes % 5) >= 2),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant((minutes % 5) >= 3),Expression.Constant("Y"), Expression.Constant("O")),
                    Expression.Condition(Expression.Constant((minutes % 5) >= 4),Expression.Constant("Y"), Expression.Constant("O"))
                };
           

            var clock = new string[trees.Count].Zip(trees, (a, b) => Expression.Lambda<Func<string>>(b).Compile()()).ToList();           

            return new StringBuilder().Append(clock[0])
                         .AppendLine()
                         .Append(string.Join("", clock.GetRange(1, 4)))
                         .AppendLine()
                         .Append(string.Join("", clock.GetRange(5, 4)))
                         .AppendLine()
                         .Append(string.Join("", clock.GetRange(9, 11)))
                         .AppendLine()
                         .Append(string.Join("", clock.GetRange(20, 4))).ToString();
            

        }
    }
}
