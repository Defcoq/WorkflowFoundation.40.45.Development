using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Activities;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using System.ServiceModel;

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
        public OutArgument<ReservationResponse> Response { get; set; }

        public SendRequest()
        {
            // Define the variables used by this workflow
            Variable<ReservationRequest> request = 
                new Variable<ReservationRequest> { Name = "request" };
            Variable<string> requestAddress = 
                new Variable<string> { Name = "RequestAddress" };

            // Define the Send activity
            Send submitRequest = new Send
            {
                ServiceContractName = "ILibraryReservation",
                EndpointAddress = new InArgument<Uri>(env => new Uri("http://localhost:" + requestAddress.Get(env) + "/LibraryReservation")),
                Endpoint = new Endpoint
                {
                    Binding = new BasicHttpBinding()
                },
                OperationName = "RequestBook",
                Content = SendContent.Create (new InArgument<ReservationRequest>(request))
            };

            // Define the SendRequest workflow
            this.Implementation = () => new Sequence
            {
                DisplayName = "SendRequest",
                Variables = { request, requestAddress },
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
                    new CorrelationScope
                    {
                        Body = new Sequence
                        {
                            Activities = 
                            {
                                submitRequest,
                                new WriteLine
                                {
                                    Text = new InArgument<string>
                                        (env => "Request sent; waiting for response"),
                                },
                                new ReceiveReply
                                {
                                    Request = submitRequest,
                                    Content = ReceiveContent.Create
                                        (new OutArgument<ReservationResponse>
                                            (env => Response.Get(env)))
                                }
                            }
                        }
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>
                            (env => "Response received from " + 
                             Response.Get(env).Provider.BranchName),
                    },
                }
            };     
        }
    }

    public sealed class ProcessRequest : Activity
    {
        public ProcessRequest()
        {
            // Define the variables used by this workflow
            Variable<ReservationRequest> request =
                new Variable<ReservationRequest> { Name = "request" };
            Variable<ReservationResponse> response =
                new Variable<ReservationResponse> { Name = "response" };
            Variable<bool> reserved = new Variable<bool> { Name = "reserved" };
            Variable<CorrelationHandle> requestHandle =
                new Variable<CorrelationHandle> { Name = "RequestHandle" };

            // Create a Receive activity
            Receive receiveRequest = new Receive
            {
                ServiceContractName = "ILibraryReservation",
                OperationName = "RequestBook",
                CanCreateInstance = true,
                Content = ReceiveContent.Create
                    (new OutArgument<ReservationRequest>(request)),
                CorrelatesWith = requestHandle
            };

            // Define the ProcessRequest workflow
            this.Implementation = () => new Sequence
            {
                DisplayName = "ProcessRequest",
                Variables = { request, response, reserved, requestHandle },
                Activities = 
                {
                    receiveRequest,
                    new WriteLine
                    {
                        Text = new InArgument<string>(
                            env => "Got request from: " + 
                            request.Get(env).Requester.BranchName),
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>(env => "Requesting: " + 
                            request.Get(env).Title),
                    },
                    new Assign 
                    {
                        To = new OutArgument<Boolean>(reserved), 
                        Value = new InArgument<Boolean>(env => true)
                    },
                    new Delay
                    {
                        Duration = TimeSpan.FromSeconds(2)
                    },
                    new CreateResponse
                    {
                        Request = new InArgument<ReservationRequest>(env => request.Get(env)),
                        Response = new OutArgument<ReservationResponse>
                            (env => response.Get(env)),
                        Reserved = new InArgument<bool>(env => reserved.Get(env)),
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>(env => "Sending response to: " + 
                            request.Get(env).Requester.BranchName),
                    },
                    new SendReply
                    {
                        Request = receiveRequest,
                        Content = SendContent.Create
                            (new InArgument<ReservationResponse>(response))
                    }
                }
            };
        }
    }
}
