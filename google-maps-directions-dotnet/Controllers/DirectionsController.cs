using google_maps_directions_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<Directions> Get(string from, string to)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // We seem to have params. Get the directions.
            Directions dirs = await m_directionsProvider.getDirections(from, to);

            return dirs;
        }
    }
}
