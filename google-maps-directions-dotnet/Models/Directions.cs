using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace google_maps_directions_dotnet.Models
{
    /// <summary>
    /// Represents the simplfied response we will return from the our API.
    /// </summary>
    public class Directions
    {
        public string distance { set; get; }

        public string duration { set; get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string errorMessage { set; get; }

        public string[] steps { set; get; }
    }
}