using google_maps_directions_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace google_maps_directions_dotnet.Controllers
{
    public class DirectionsController : ApiController
    {
        DirectionsProvider m_directionsProvider = new DirectionsProvider();

        // GET: api/Directions
        /// <summary>
        /// Get directions from Google Directions API
        /// </summary>
        /// <returns>Directions</returns>
        public HttpResponseMessage Get()
        {
            // Verify we were passed to and from parameters.
            NameValueCollection queryString = this.Request.RequestUri.ParseQueryString();

            bool bMissingParams = false;

            string from = "";
            if (!string.IsNullOrWhiteSpace(queryString["from"]))
                from = queryString["from"];
            else
                bMissingParams = true;

            string to = "";
            if (!string.IsNullOrWhiteSpace(queryString["to"]))
                to = queryString["to"];
            else
                bMissingParams = true;

            HttpResponseMessage responseMsg = null;

            if (bMissingParams)
            {
                responseMsg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return responseMsg;
            }

            // We seem to have params. Get the directions.
            Directions dirs = m_directionsProvider.getDirections(from, to);

            try
            {
                responseMsg = this.Request.CreateResponse(HttpStatusCode.OK, dirs);
            }
            catch(Exception)
            {
                responseMsg = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return responseMsg;
        }
    }
}
