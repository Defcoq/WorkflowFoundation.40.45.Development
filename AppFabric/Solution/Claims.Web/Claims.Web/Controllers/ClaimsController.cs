using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using Claims.Web.Helpers;
using Claims.Web.Models;
using BusinessEntities;

namespace Claims.Web.Controllers
{
    public class ClaimsController : Controller
    {
        Claims.Web.Appraisal.WCF.Proxy.AppraisalServiceClient claimService = new Claims.Web.Appraisal.WCF.Proxy.AppraisalServiceClient();



        
        
        //
        // GET: /Claims/

        public ActionResult Index(int? page)
        {
            const int pageSize = 10;

            var claimResponse = claimService.FindAllClaims();
            
            List<Claim> listOfClaims = new List<Claim>();
            foreach (var webClaim in claimResponse)
            {
                listOfClaims.Add(webClaim);
            }
            var paginatedClaims = new PaginatedList<Claim>(listOfClaims.AsQueryable<Claim>(), page ?? 0, pageSize);

            return View(paginatedClaims);
        }

        //
        // GET: /Claims/Details/5

        public ActionResult Details(int id)
        {
            var webClaim = claimService.GetClaim(id);

            if (webClaim == null)
                return View("NotFound");
            else
                return View(webClaim);
        }

        //
        // GET: /Claims/Edit/5
        [Authorize(Users="dannyg,stephenk,reader")]
        public ActionResult Edit(int id)
        {
            var webClaim = claimService.GetClaim(id);

            return View(new ClaimFormViewModel(webClaim));
        }

        //
        // POST: /Claims/Edit/5

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //Retrieve existing claim
            Claim claim = claimService.GetClaim(id);

            //Retrieve existing claim
            Claim originalClaim = claim.Copy();

            try
            {
                //Update model
                UpdateModel(claim);
                UpdateModel(claim.Accidents);

                //Always update the date if the at least one field has been updated
                if (!claim.IsIdentical(originalClaim))
                {
                    claim.DateCreated = DateTime.Now;

                    //Attach the Rental Car endpoint to the claim
                    claim.RentalCar = MvcApplication.CarRentalEndpoint;

                    ////Book a rental car
                    //claim.RentalCar = RentCar(claim.ClaimId);

                    ////Process the claim and update the status
                    //claim.Status = Process(claim.ClaimId);

                    //if (String.IsNullOrEmpty(claim.Status)) return View("NotFound");

                    try
                    {
                        //Process claim by invoking the WF workflow service
                        if (!ProcessClaim(claim))
                        {
                            ModelState.AddModelError("Exception", "Failed to process claim in the ProcessClaimService WF");
                            return View(new ClaimFormViewModel(claim));
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Workflow Exception", ex);
                        return View(new ClaimFormViewModel(claim));
                    }
                    ////Persist changes back to database
                    //claimService.Save(webClaim);
                }

                //Perform HTTP redirect to details page for the saved Claim
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex);
                return View(new ClaimFormViewModel(claim));
            }
        }


        //public string Process(int id)
        //{
        //    //Create DiscoveryClient
        //    DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

        //    //Create the search criteria for the specified scope
        //    FindCriteria findCriteria = new FindCriteria(typeof(IBilling));

        //    //Add discovery scopes
        //    //findCriteria.Scopes.Add(new Uri("http://www.contoso.com/insurance"));
        //    //findCriteria.Scopes.Add(new Uri("ldap:///ou=insurance,o=contoso,c=us"));
        //    //findCriteria.ScopeMatchBy = FindCriteria.ScopeMatchByExact;

        //    //Find BillingService endpoint
        //    FindResponse findResponse = discoveryClient.Find(findCriteria);

        //    if (findResponse.Endpoints.Count == 0)
        //        return "";

        //    //Pick the first discovered endpoint
        //    EndpointAddress address = findResponse.Endpoints[0].Address;

        //    //Create the target service client
        //    ChannelFactory<IBilling> factory = new ChannelFactory<IBilling>(new BasicHttpBinding(), address);
        //    IBilling client = factory.CreateChannel();

        //    //Call the Billing Service
        //    string status = client.ProcessClaim(id, findResponse.Endpoints.Count, address.Uri.AbsoluteUri);

        //    factory.Close();

        //    //return  a new status of the processed claim
        //    return status;

        //}



        //public string RentCar(int id)
        //{
        //    //Check if the destination endpoint has been announced (discovered) yet
        //    if (String.IsNullOrEmpty(MvcApplication.CarRentalEndpoint))
        //        return "No rental car";

        //    //Create the target service client
        //    ChannelFactory<ICarRental> factory = new ChannelFactory<ICarRental>(new BasicHttpBinding(), MvcApplication.CarRentalEndpoint);
        //    ICarRental client = factory.CreateChannel();

        //    //Call the Car Rental Service
        //    string status = client.FindRentalCar(id);

        //    factory.Close();

        //    //return  a new status of the processed claim
        //    return status;

        //}



        //public string Billing(int id)
        //{
        //    // Create a Discovery Endpoint that points to the proxy service.
        //    Uri probeEndpointAddress = new Uri("net.tcp://localhost:9001/Probe");
        //    DiscoveryEndpoint discoveryEndpoint = new DiscoveryEndpoint(
        //        new NetTcpBinding(), new EndpointAddress(probeEndpointAddress));
            
        //    // Create DiscoveryClient using the previously created discoveryEndpoint
        //    DiscoveryClient managedDiscoveryClient = new DiscoveryClient(discoveryEndpoint);

        //    // Find IChapter6Discovery endpoints
        //    FindResponse findResponse = managedDiscoveryClient.Find(new FindCriteria(typeof(IBilling)));

        //    if (findResponse.Endpoints.Count == 0) return "";

        //    // Pick the first discovered endpoint
        //    EndpointAddress address = findResponse.Endpoints[0].Address;

        //    //Create the target service client
        //    ChannelFactory<IBilling> factory = new ChannelFactory<IBilling>(new BasicHttpBinding(), address);
        //    IBilling client = factory.CreateChannel();

        //    //Call the Billing Service
        //    string status = client.ProcessClaim(id, findResponse.Endpoints.Count, address.Uri.AbsoluteUri);

        //    factory.Close();

        //    //return  a new status of the processed claim
        //    return status;

        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public bool ProcessClaim(Claim claim)
        {
            bool isSuccess = false;

            using (Claims.Web.ProcessClaim.WF.ProcessClaimServiceClient client =
                new Claims.Web.ProcessClaim.WF.ProcessClaimServiceClient("BasicHttpBinding_IProcessClaimService"))
            {
                //Invoke ProcessClaim Operation
                isSuccess = (bool) client.ProcessClaim(claim);
            }

            return isSuccess;

        }
    }
}
