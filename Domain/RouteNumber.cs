using System.Collections.Generic;

namespace Domain
{
    public class RouteNumber
    {
        public List<Offer> offers;

        public int RouteID { get; set; }
        public int RequiredVehicleType { get; set; }
        public int Hours { get; set; }
        
        public RouteNumber()
        {
            offers = new List<Offer>(); 
        }
        public RouteNumber(int routeID, int requiredVehicleType, int hours) : this()
        {          
            this.RouteID = routeID;
            this.RequiredVehicleType = requiredVehicleType;
            this.Hours = hours;
        }
    }
}
