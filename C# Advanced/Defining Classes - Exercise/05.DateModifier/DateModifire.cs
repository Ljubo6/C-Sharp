namespace _05.DateModifier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class DateModifier
    {
        public int CalculateDiff(string dateOne,string dateTwo)
        {
            int[] dateOneArr = dateOne.Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            DateTime dateTime1 = new DateTime(dateOneArr[0],dateOneArr[1],dateOneArr[2]);

            int[] dateTwoArr = dateTwo.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            DateTime dateTime2 = new DateTime(dateTwoArr[0], dateTwoArr[1], dateTwoArr[2]);

            return Math.Abs((dateTime1 - dateTime2).Days);
        }
    }
}
