 using System;
using System.Activities;
using System.Collections.Generic;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary
{
    public sealed class ParseCalculatorArgs : CodeActivity
    {
        [RequiredArgument]
        public InArgument<String> Expression { get; set; }
        public OutArgument<Double> FirstNumber { get; set; }
        public OutArgument<Double> SecondNumber { get; set; }
        public OutArgument<String> Operation { get; set; }

      
       
        protected override void Execute(CodeActivityContext context)
        {
            
            FirstNumber.Set(context, 0);
            SecondNumber.Set(context, 0);
            Operation.Set(context, "error");

            String line = Expression.Get(context);
            if (!String.IsNullOrEmpty(line))
            {
                String[] arguments = line.Split(' ');
                if (arguments.Length == 3)
                {
                    Double number = 0;
                    if (Double.TryParse(arguments[0], out number))
                    {
                        FirstNumber.Set(context, number);
                    }
                    Operation.Set(context, arguments[1]);
                    if (Double.TryParse(arguments[2], out number))
                    {
                        SecondNumber.Set(context, number);
                    }
                }
            }
        }
    }
}
