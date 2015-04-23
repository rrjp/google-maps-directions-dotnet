using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace google_maps_directions_dotnet.Models
{
    /// <summary>
    /// Represents the more complex response we receive from Google.
    /// Just deserialize the pieces we are interested in.
    /// </summary>
    public class GoogleDirections
    {
        /** Array of routes. */
        public GoogleMapRoutes[] routes {get; set;}

        /**  Set to "NOT_FOUND" if it can't find a route. Status is "OK" if it worked. */
        public String status {get; set;}

        /** Each route can have legs. */
        public class GoogleMapRoutes
        {
            public GoogleMapLeg[] legs {get; set;}
        }

        /** Each leg can have steps. */
        public class GoogleMapLeg
        {
            public GoogleMapStep[] steps { get; set; }

            public GoogleMapLegDistance distance {get; set;}

            public GoogleMapLegDuration duration {get; set;}
        }

        /** Each step has directions. */
        public class GoogleMapStep
        {
            public String html_instructions { get; set; }
        }

        /** Distance */
        public class GoogleMapLegDistance
        {
            public String text { get; set; }
        }

        /** Duration */
        public class GoogleMapLegDuration
        {
            public String text { get; set; }
        }
    }
}