using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace google_maps_directions_dotnet.Models
{
    public class DirectionsProvider
    {
        /// <summary>
        /// Google server base URL from the config file.
        /// </summary>
        static string c_sGoogleBaseUrl = null;
        
        /// <summary>
        /// Status returned from the Google API when it worked.
        /// </summary>
        static string cs_goodStatus = "OK";

        /// <summary>
        /// Message returned when it didn't work. Not taking the time for i18n now.
        /// </summary>
        static string cs_errorMsg = "Error retrieving directions.";

        /// <summary>
        /// Get the directions.
        /// </summary>
        /// <param name="from">Starting point.</param>
        /// <param name="to">Ending point.</param>
        /// <returns>Directions object. May be empty on error.</returns>
        public Directions getDirections(string from, string to)
        {
            Directions dirs = null;

            if (c_sGoogleBaseUrl == null)
                c_sGoogleBaseUrl = ConfigurationManager.AppSettings["GoogleDirsUrl"];

            var restClient = new RestClient(c_sGoogleBaseUrl);

            restClient.Authenticator = new HttpBasicAuthenticator("", "");
            restClient.UserAgent = "reyes-test-app";

            var request = new RestRequest(Method.GET);
            request.Resource = "json";

            request.AddParameter("origin", from);
            request.AddParameter("destination", to);

            Uri uri = restClient.BuildUri(request);

            IRestResponse response = restClient.Execute(request);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                string result = response.Content;
                GoogleDirections gDirs = JsonConvert.DeserializeObject<GoogleDirections>(result);
                dirs = convertDirections(gDirs);
            }

            return dirs;
        }

        /// <summary>
        /// Extract just the parts we are interested in from the information returned from Google.
        /// </summary>
        /// <param name="googleDirections">The information we got from Google.</param>
        /// <returns>The information this API will return.</returns>
        Directions convertDirections(GoogleDirections googleDirections)
        {
            Directions mapDirections = new Directions();

            // Parse out Google map directions to flatten the "routes" and "legs" stuff as we will only support one leg.
            if (googleDirections == null || !cs_goodStatus.Equals(googleDirections.status, StringComparison.OrdinalIgnoreCase))
            {
                mapDirections.errorMessage = cs_errorMsg;
                return mapDirections;
            }

            if (googleDirections.routes != null && googleDirections.routes.Length > 0)
                if (googleDirections.routes[0] != null && googleDirections.routes[0].legs.Length > 0)
                    if (googleDirections.routes[0].legs[0].steps != null && googleDirections.routes[0].legs[0].steps.Length > 0)
                    {
                        mapDirections.steps = new string[googleDirections.routes[0].legs[0].steps.Length];
                        for (int i = 0; i < googleDirections.routes[0].legs[0].steps.Length; i++)
                        {
                            mapDirections.steps[i] = googleDirections.routes[0].legs[0].steps[i].html_instructions;
                        }

                        mapDirections.distance = googleDirections.routes[0].legs[0].distance.text;
                        mapDirections.duration = googleDirections.routes[0].legs[0].duration.text;
                    }

            return mapDirections;
        }

    }
}