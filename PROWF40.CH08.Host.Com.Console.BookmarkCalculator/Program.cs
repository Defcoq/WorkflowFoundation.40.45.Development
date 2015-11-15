using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Threading;
using System.Collections.Generic;
//using PROWF40.CH08.Host.Com.ConsoleTest.BookmarkCalculator.CH13.Exeption.Handling;

namespace PROWF40.CH08.Host.Com.ConsoleTest.BookmarkCalculator
{

    class Program
    {
        private static Boolean isRunning = true;
        private static WorkflowApplication wfApp;

        static void Main(string[] args)
        {
            #region CH13 Transaction, Compensation, exeption handling
            #region using try catch activities
            //Console.WriteLine("UpdateInventoryTryCatch with exception");
            //RunWorkflow(new UpdateInventoryTryCatch(), 43687, true);
            //Thread.Sleep(4000);
            //Console.ReadLine();

            #endregion

            #region TransactionScope activities
            //Console.WriteLine("UpdateInventoryTran without exception");
            //RunWorkflow(new UpdateInventoryTransactionScope(), 43687, false);
            //Thread.Sleep(4000);
            //Console.WriteLine("\nUpdateInventoryTran with exception ");
            //RunWorkflow(new UpdateInventoryTransactionScope(), 43687, true);
            //Thread.Sleep(4000);

            #endregion

            #region Compensation activities
            //Console.WriteLine("UpdateInventoryComp without exception ");
            //RunWorkflow(new UpdateInventoryComp(), 43687, false);
            //Thread.Sleep(4000);
            //Console.WriteLine("\nUpdateInventoryComp with exception ");
            //RunWorkflow(new UpdateInventoryComp(), 43687, true);
            //Thread.Sleep(4000);
            //Console.ReadLine();
            #endregion

            #region Manual Compensation
            Console.WriteLine("UpdateInventoryManualComp without exception ");
            RunWorkflow(new UpdateInventoryManualComp(), 43687, false);
            Thread.Sleep(4000);
            Console.WriteLine("\nUpdateInventoryManualComp with exception ");
            RunWorkflow(new UpdateInventoryManualComp(), 43687, true);
            Thread.Sleep(4000);
            Console.ReadLine();
            #endregion
            //Console.WriteLine("UpdateInventory without exception");
            //RunWorkflow(new UpdateInventory(), 43687, false);
            //Thread.Sleep(4000);
            //Console.WriteLine("\nUpdateInventory with exception");
            //RunWorkflow(new UpdateInventory(), 43687, true);
            //Thread.Sleep(4000);

            #endregion
            #region CH08 Host Comunication
            //            while (isRunning)
            //            {
            //                try
            //                {

            //                    AutoResetEvent syncEvent = new AutoResetEvent(false);
            //                    WorkflowApplication wfApp = new WorkflowApplication(new BookmarkCalculator());
            //                    wfApp.Completed = delegate(WorkflowApplicationCompletedEventArgs e)
            //                    {
            //                        if (e.CompletionState == ActivityInstanceState.Closed)
            //                        {
            //                            Console.WriteLine("Result = {0}", e.Outputs["Result"]);
            //                        }
            //                        syncEvent.Set();
            //                    };
            //                    wfApp.Idle = delegate(WorkflowApplicationIdleEventArgs e)
            //                    {
            //                        Console.WriteLine("Workflow is idle");
            //                    };
            //                    wfApp.OnUnhandledException = delegate(
            //                    WorkflowApplicationUnhandledExceptionEventArgs e)
            //                    {
            //                        Console.WriteLine(e.UnhandledException.Message.ToString());
            //                        return UnhandledExceptionAction.Terminate;
            //                    };
            //                    wfApp.Run();

            //                    Console.WriteLine("Enter an expression or 'quit' to exit");
            //                    String expression = Console.ReadLine();
            //                    if ((String.IsNullOrEmpty(expression)) || (!String.IsNullOrEmpty(expression) && expression.Trim().ToLower() == "quit"))
            //                    {
            //                        wfApp.Cancel();
            //                        Console.WriteLine("Exiting program");
            //                        isRunning = false;
            //                    }
            //                    else if (IsBookmarkValid(wfApp, "GetString"))
            //                    {
            //                        wfApp.ResumeBookmark("GetString", expression);
            //                    }
            //                    syncEvent.WaitOne();


            ////                    AutoResetEvent syncEvent = new AutoResetEvent(false);

            ////#if INVOKEACTION
            ////                    BookmarkCalculatorInvokeAction wf = new BookmarkCalculatorInvokeAction();
            ////#else
            ////                    BookmarkCalculatorAction wf = new BookmarkCalculatorAction();
            ////#endif
            ////                    wf.Action = new ActivityAction<String, String>
            ////                    {
            ////                        Argument1 = new DelegateInArgument<String>(),
            ////                        Argument2 = new DelegateInArgument<String>(),
            ////                        Handler = new InvokeMethod
            ////                        {
            ////                            TargetType = typeof(Program),
            ////                            MethodName = "ReceivedNotification",
            ////                            Parameters =
            ////                            {
            ////                                new InArgument<String>(
            ////                                    ac => wf.Action.Argument1.Get(ac)),
            ////                                new InArgument<String>(
            ////                                    ac => wf.Action.Argument2.Get(ac))
            ////                            }
            ////                        }
            ////                    };

            ////                    wfApp = new WorkflowApplication(wf);
            ////                    wfApp.Completed = delegate(
            ////                        WorkflowApplicationCompletedEventArgs e)
            ////                    {
            ////                        if (e.CompletionState == ActivityInstanceState.Closed)
            ////                        {
            ////                            Console.WriteLine("Result = {0}", e.Outputs["Result"]);
            ////                        }
            ////                        syncEvent.Set();
            ////                    };

            ////                    wfApp.Idle = delegate(WorkflowApplicationIdleEventArgs e)
            ////                    {
            ////                        Console.WriteLine("Workflow is idle");
            ////                    };

            ////                    wfApp.OnUnhandledException = delegate(
            ////                        WorkflowApplicationUnhandledExceptionEventArgs e)
            ////                    {
            ////                        Console.WriteLine(e.UnhandledException.Message.ToString());
            ////                        return UnhandledExceptionAction.Terminate;
            ////                    };

            ////                    wfApp.Run();
            ////                    syncEvent.WaitOne();
            //                }
            //                catch (Exception exception)
            //                {
            //                    Console.WriteLine("Error: {0}", exception.Message);
            //                }
            //            }
            #endregion
        }


        private static void RunWorkflow(Activity wf, Int32 orderId, Boolean isDemoException)
        {
            try
            {
                DisplayInventory(orderId, "Starting");
                AutoResetEvent syncEvent = new AutoResetEvent(false);
                WorkflowApplication wfApp =new WorkflowApplication(wf, new Dictionary<String, Object>{
                                                                                                        {"ArgSalesOrderId", orderId},
                                                                                                        {"ArgIsDemoException", isDemoException}
                                                                                                        });
                wfApp.Completed = delegate(
                WorkflowApplicationCompletedEventArgs e)
                {
                    syncEvent.Set();
                };
                wfApp.OnUnhandledException = delegate(
                WorkflowApplicationUnhandledExceptionEventArgs e)
                {
                    Console.WriteLine("OnUnhandledException: {0}",
                    e.UnhandledException.Message);
                    return UnhandledExceptionAction.Cancel; //needed to compensate
                };
                wfApp.Run();
                syncEvent.WaitOne();
                DisplayInventory(orderId, "Ending");

            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: {0}", exception.Message);
            }

        }

        private static void DisplayInventory(Int32 orderId, String desc)
        {
            WorkflowInvoker.Invoke(new DisplayInventory(), new Dictionary<String, Object>
                                                                    {
                                                                    {"ArgSalesOrderId", orderId},
                                                                    {"ArgDescription", desc}
                                                                    });

        }

        private static Boolean IsBookmarkValid(WorkflowApplication wfApp, String bookmarkName)
        {
            Boolean result = false;
            var bookmarks = wfApp.GetBookmarks();
            if (bookmarks != null)
            {
                result =
                    (from b in bookmarks
                     where b.BookmarkName == bookmarkName
                     select b).Any();
            }
            return result;
        }

        public static void ReceivedNotification(String prompt, String bookmarkName)
        {
            Action<String, String> asyncWork = (msg, bm) =>
            {
                Console.WriteLine(msg);
                String expression = Console.ReadLine();

                if ((String.IsNullOrEmpty(expression)) ||
                    (!String.IsNullOrEmpty(expression) &&
                     expression.Trim().ToLower() == "quit"))
                {
                    wfApp.Cancel();
                    Console.WriteLine("Exiting program");
                    isRunning = false;
                }
                else if (IsBookmarkValid(wfApp, bm))
                {
                    wfApp.ResumeBookmark(bm, expression);
                }
            };

            //ar = IAsyncResult
            asyncWork.BeginInvoke(prompt, bookmarkName,
                ar => { ((Action<String, String>)ar.AsyncState).EndInvoke(ar); },
                asyncWork);
        }
    }
}
