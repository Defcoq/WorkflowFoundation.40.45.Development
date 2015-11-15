using System;
using System.Activities;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.IO;

namespace LibraryReservation
{
    /*****************************************************/
    // This file contains the definition of two workflows:
    //
    // SendRequest initiates a new request
    // ProcessRequest handles incoming requests
    //
    /*****************************************************/
    public sealed class SendRequest : Activity
    {
        // Define the input and output arguments
        public InArgument<string> Title { get; set; }
        public InArgument<string> Author { get; set; }
        public InArgument<string> ISBN { get; set; }
        public InArgument<TextWriter> Writer { get; set; }
        public OutArgument<ReservationResponse> Response { get; set; }

        public SendRequest()
        {
            // Define the variables used by this workflow
            Variable<ReservationRequest> request =
                new Variable<ReservationRequest> { Name = "request" };
            Variable<string> requestAddress =
                new Variable<string> { Name = "RequestAddress" };
            Variable<bool> reserved = new Variable<bool> { Name = "Reserved" };

            // Define the SendRequest workflow
            this.Implementation = () => new Sequence
            {
                DisplayName = "SendRequest",
                Variables = { request, requestAddress, reserved },
                Activities =
                {
                    new CreateRequest
                    {
                        Title = new InArgument<string>(env => Title.Get(env)),
                        Author = new InArgument<string>(env => Author.Get(env)),
                        ISBN = new InArgument<string>(env => ISBN.Get(env)),
                        Request = new OutArgument<ReservationRequest>
                            (env => request.Get(env)),
                        RequestAddress = new OutArgument<string>
                            (env => requestAddress.Get(env))
                    },
                    new Send
                    {
                        OperationName = "RequestBook",
                        ServiceContractName = "ILibraryReservation",
                        Content = SendContent.Create
                            (new InArgument<ReservationRequest>(request)),
                        EndpointAddress = new InArgument<Uri>
                            (env => new Uri("http://localhost:" +
                            requestAddress.Get(env) + "/ClientService")),
                        Endpoint = new Endpoint
                        {
                            Binding = new BasicHttpBinding()
                        },
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>
                            (env => "Request sent; waiting for response"),
                        TextWriter = new InArgument<TextWriter> (env => Writer.Get(env))
                    },
                    new WaitForInput<ReservationResponse>
                    {
                        BookmarkName = "GetResponse",
                        Input = new OutArgument<ReservationResponse>
                            (env => Response.Get(env))
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>
                            (env => "Response received from " +
                            Response.Get(env).Provider.BranchName + " [" +
                            Response.Get(env).Reserved.ToString() + "]"),
                        TextWriter = new InArgument<TextWriter> (env => Writer.Get(env))
                    },
                }
            };
        }
    }

    // Add the ProcessRequest workflow here
    public sealed class ProcessRequest : Activity
    {
        public InArgument<ReservationRequest> request { get; set; }
        public InArgument<TextWriter> Writer { get; set; }

        public ProcessRequest()
        {
            // Define the variables used by this workflow
            Variable<ReservationResponse> response =
                new Variable<ReservationResponse> { Name = "response" };
            Variable<bool> reserved = new Variable<bool> { Name = "Reserved" };
            Variable<string> address = new Variable<string> { Name = "Address" };

            // Define the ProcessRequest workflow
            this.Implementation = () => new Sequence
            {
                DisplayName = "ProcessRequest",
                Variables = { response, reserved, address },
                Activities =
                {
                    new WriteLine
                    {
                        Text = new InArgument<string>(env => "Got request from: " +
                            request.Get(env).Requester.BranchName),
                        TextWriter = new InArgument<TextWriter> (env => Writer.Get(env))
                    },
                    new InvokeMethod
                    {
                        TargetType = typeof(ApplicationInterface),
                        MethodName = "NewRequest",
                        Parameters =
                        {
                            new InArgument<ReservationRequest>(env => request.Get(env))
                        }
                    },
                    new WaitForInput<bool>
                    {
                        BookmarkName = "GetResponse",
                        Input = new OutArgument<bool>(env => reserved.Get(env))
                    },
                    new CreateResponse
                    {
                        Request = new InArgument<ReservationRequest>
                            (env => request.Get(env)),
                        Reserved = new InArgument<bool>(env => reserved.Get(env)),
                        Response = new OutArgument<ReservationResponse>
                            (env => response.Get(env))
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>(env => "Sending response to: " +
                            request.Get(env).Requester.BranchName),
                        TextWriter = new InArgument<TextWriter> (env => Writer.Get(env))
                    },
                    new Send
                    {
                        OperationName = "RespondToRequest",
                        ServiceContractName = "ILibraryReservation",
                        EndpointAddress = new InArgument<Uri>(
                            env => new Uri("http://localhost:" +
                            request.Get(env).Requester.Address + "/ClientService")),
                        Endpoint = new Endpoint
                        {
                            Binding = new BasicHttpBinding()
                        },
                        Content = SendContent.Create
                            (new InArgument<ReservationResponse>(response))
                    }
                }
            };
        }
    }
}
