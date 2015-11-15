using System;
using System.Activities;
using System.Activities.Statements;
using System.IO;
using System.ServiceModel.Activities;
using System.ServiceModel;

using LeadGenerator;

namespace LeadResponse
{
    /*****************************************************/
    //
    /*****************************************************/
    public sealed class WorkAssignment : Activity
    {
        public InArgument<TextWriter> Writer { get; set; }

        public WorkAssignment()
        {
        
            // Define the variables used by this workflow
            Variable<int> leadID = new Variable<int> { Name = "leadID" };
            Variable<string> assignedTo = new Variable<string> { Name = "assignedTo" };
            Variable<Assignment> assignment = new Variable<Assignment> { Name = "assignment" };

            Receive receive = new Receive
            {
                OperationName = "Assign",
                ServiceContractName = "CreateAssignment",
                CanCreateInstance = true,
                Content = new ReceiveParametersContent
                {
                    Parameters = 
                    {
                        { "leadID", new OutArgument<int>(leadID) },
                        { "assignedTo", new OutArgument<string>(assignedTo) }
                    }
                }
            };

            // Define the Assignment workflow
            this.Implementation = () => new Sequence
            {
                DisplayName = "WorkAssignment",
                Variables = { assignment, leadID, assignedTo },
                Activities = 
                {
                    receive,
                    new Delay
                    {
                        Duration = TimeSpan.FromSeconds(5)
                    },
                    new CreateAssignment
                    {
                        LeadID = new InArgument<int>(env => leadID.Get(env)),
                        AssignedTo = new InArgument<string>
                            (env => assignedTo.Get(env)),
                        Assignment = new OutArgument<Assignment>
                            (env => assignment.Get(env)),
                    },
                    new SendReply
                    {
                        Request = receive
                    },
                    new Persist
                    {
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>
                            (env => "Lead has been assigned to " + 
                                assignment.Get(env).AssignedTo),
                        TextWriter = new InArgument<TextWriter> (env => Writer.Get(env))
                    },
                    new InvokeMethod
                    {
                        TargetType = typeof(ApplicationInterface),
                        MethodName = "AddAssignment",
                        Parameters =
                        {
                            new InArgument<Assignment>(env => assignment.Get(env))
                        }
                    },
                    new WaitForInput<Assignment>
                    {
                        BookmarkName = "GetCompletion",
                        Input = new OutArgument<Assignment>
                            (env => assignment.Get(env))
                    },
                    new CompleteAssignment
                    {
                        Assignment = new InOutArgument<Assignment>
                            (env => assignment.Get(env))
                    },
                    new InvokeMethod
                    {
                        TargetType = typeof(ApplicationInterface),
                        MethodName = "UpdateAssignment",
                        Parameters =
                        {
                            new InArgument<Assignment>(env => assignment.Get(env))
                        }
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>
                            (env => "Assignment has been completed"),
                        TextWriter = new InArgument<TextWriter> (env => Writer.Get(env))
                    },
                }
            };
        }
    }
}
