using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using BeginWF40.Comunication.BookLkp.Client.ServiceReference2;

namespace BeginWF40.Comunication.BookLkp.Client
{

    class Program
    {
        static void Main(string[] args)
        {
            // create dictionary with input arguments for the workflow
            IDictionary<string, object> input = new Dictionary<string, object>
            {
            { "argAuthor" , "Margaret Mitchell" },
            { "argTitle" , "Gone with the Wind" },
            { "argISBN" , "1234567890123" }
            };
            // execute the workflow
            IDictionary<string, object> output = WorkflowInvoker.Invoke(new Workflow1(), input);
            BookInfo[] l = output["argBookList"] as BookInfo[];
            if (l != null)
            {
                foreach (BookInfo i in l)
                {
                    Console.WriteLine("{0}: {1}, {2}",
                    i.Title, i.status, i.InventoryID);
                }
            }

            else
                Console.WriteLine("No items were found");
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();


        }
    }
}
