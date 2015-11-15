using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.Activities.Statements;
using System.IO;
using LeadGenerator.Activities;
using System.ServiceModel;
using System.ServiceModel.Activities;


namespace LeadGenerator
{
    /*****************************************************/
    // This file contains the definition of the EnterLead
    // workflow
    /*****************************************************/

    public sealed class EnterLead : Activity
    {
        // Define the input and output arguments
        public InArgument<string> ContactName { get; set; }
        public InArgument<string> ContactPhone { get; set; }
        public InArgument<string> Interests { get; set; }
        public InArgument<string> Notes { get; set; }
        //public InArgument<string> ConnectionString { get; set; }
        public InArgument<int> Rating { get; set; }
        public InArgument<TextWriter> Writer { get; set; }


        public EnterLead()
        {
            // Define the variables used by this workflow
            Variable<Lead> lead = new Variable<Lead> { Name = "lead" };
            Variable<string> assignedTo = new Variable<string> { Name = "assignedTo" };
            Send send = new Send
                {
                    OperationName = "Assign",
                    ServiceContractName = "CreateAssignment",
                    Content = new SendParametersContent
                    {
                        Parameters =
                    {
                                    {
                                    "leadID",
                                    new InArgument<int> (env => lead.Get(env).LeadID)
                                    },
                                    {
                                    "assignedTo",
                                    new InArgument<string>(env => assignedTo.Get(env))
                                    }
                    }
                    },
                    EndpointAddress = new InArgument<Uri>
                    (env => new Uri("http://localhost/CreateAssignment")),
                    Endpoint = new Endpoint
                    {
                        Binding = new BasicHttpBinding()
                    }
                };

            // Define the SendRequest workflow
            this.Implementation = () => new Sequence
            {
                DisplayName = "EnterLead",
                Variables = { lead, assignedTo },
                Activities =
                {
                    new CreateLead
                    {
                    ContactName = new InArgument<string>
                    (env => ContactName.Get(env)),
                    ContactPhone = new InArgument<string>
                    (env => ContactPhone.Get(env)),
                    Interests = new InArgument<string>
                    (env => Interests.Get(env)),
                    Notes = new InArgument<string>(env => Notes.Get(env)),
                    //ConnectionString = new InArgument<string>(env => ConnectionString.Get(env)),
                    Lead = new OutArgument<Lead>(env => lead.Get(env)),
                    },
                new WriteLine
                {
                Text = new InArgument<string>
                (env => "Lead received [" + Rating.Get(env).ToString()
                + "]; waiting for assignment"),
                TextWriter = new InArgument<TextWriter>
                (env => Writer.Get(env))
                },
                new InvokeMethod
                {
                    TargetType = typeof(ApplicationInterface),
                    MethodName = "NewLead",
                    Parameters =
                    {
                    new InArgument<Lead>(env => lead.Get(env))
                    }
                },
                new AddComment
                {
                Comment = new InArgument<string>(env => "Lead has been created")
                },
                
                

               new WaitForInput<string>
                {
                BookmarkName = "GetAssignment",
                Input = new OutArgument<string>(env => assignedTo.Get(env))
                },
                //funziona capitolo 15
                //new TransactionScope
                //{
                //    Body = new Sequence
                //    {
                //        Activities =
                //        {

                //            new AssignLead
                //            {
                //            AssignedTo = new InArgument<string>(env => assignedTo.Get(env)),
                //            Lead = new InOutArgument<Lead>(env => lead.Get(env)),
                //            },
                //            new Delay
                //            {
                //            Duration = TimeSpan.FromSeconds(20)
                //            },
                //            new CreateAssignment
                //            {
                //            AssignedTo = new InArgument<string>(env => assignedTo.Get(env)),
                //            LeadID = new InArgument<int>(env => lead.Get(env).LeadID),
                //            }
                //        }
                //    },
                // },
                new AssignLead
                {
                    AssignedTo = new InArgument<string>
                    (env => assignedTo.Get(env)),
                    Lead = new InOutArgument<Lead>(env => lead.Get(env)),
                    },
                    new CorrelationScope
                    {
                    Body = new Sequence
                    {
                    Activities =
                    {
                    send,
                    new ReceiveReply
                    {
                    Request = send
                    }
                    }
                    }
                },

                 new InvokeMethod
                                {
                                TargetType = typeof(ApplicationInterface),
                                MethodName = "UpdateLead",
                                Parameters =
                                {
                                new InArgument<Lead>(env => lead.Get(env))
                                }
                                },

                new AddComment
                {
                Comment = new InArgument<string>(env => "Lead is being assigned")
                },
                new WriteLine
                {
                Text = new InArgument<string>
                (env => "Lead assigned [" + Rating.Get(env).ToString()
                + "] to " + lead.Get(env).AssignedTo),
                TextWriter = new InArgument<TextWriter>
                (env => Writer.Get(env))
                }
                }
            };

        }
    }
}
