using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;
namespace Domain
{
    public class Contractor
    {
        private int type2;
        private int type3;
        private int type5;
        private int type6;
        private int type7;

        public List<Offer> winningOffers;
        public List<Offer> ineligibleOffers;

        public string ReferenceNumberBasicInformationPDF { get; set; }
        public string UserID { get; set; }
        public string CompanyName { get; set; }
        public string ManagerName { get; set; }  
        public string Hours { get; set; }
        public int NumberOfType2PledgedVehicles { get; set; }
        public int NumberOfType3PledgedVehicles { get; set; }
        public int NumberOfType5PledgedVehicles { get; set; }
        public int NumberOfType6PledgedVehicles { get; set; }
        public int NumberOfType7PledgedVehicles { get; set; }
        public string TryParseValueType2PledgedVehicles { get; set; }
        public string TryParseValueType3PledgedVehicles { get; set; }
        public string TryParseValueType5PledgedVehicles { get; set; }
        public string TryParseValueType6PledgedVehicles { get; set; }
        public string TryParseValueType7PledgedVehicles { get; set; }
        public int NumberOfWonType2Offers { get; private set; }
        public int NumberOfWonType3Offers { get; private set; }
        public int NumberOfWonType5Offers { get; private set; }
        public int NumberOfWonType6Offers { get; private set; }
        public int NumberOfWonType7Offers { get; private set; }

        public Contractor()
        {
            winningOffers = new List<Offer>();
            ineligibleOffers = new List<Offer>();
        }
        public Contractor(
            string referenceNumberBasicInformationPDF, string userID, string companyName,
            string managerName, int numberOfType2PledgedVehicles, int numberOfType3PledgedVehicles,int numberOfType5PledgedVehicles,
            int numberOfType6PledgedVehicles, int numberOfType7PledgedVehicles, string hours) : this()
        {
            this.ReferenceNumberBasicInformationPDF = referenceNumberBasicInformationPDF;
            this.UserID = userID;
            this.CompanyName = companyName;
            this.ManagerName = managerName;
            this.Hours = hours;
            this.NumberOfType2PledgedVehicles = numberOfType2PledgedVehicles;
            this.NumberOfType3PledgedVehicles = numberOfType3PledgedVehicles;
            this.NumberOfType5PledgedVehicles = numberOfType5PledgedVehicles;
            this.NumberOfType6PledgedVehicles = numberOfType6PledgedVehicles;
            this.NumberOfType7PledgedVehicles = numberOfType7PledgedVehicles;
        }

        public void AddWonOffer(Offer offer)
        {
            bool alreadyOnTheList = winningOffers.Any(item => item.OfferReferenceNumber == offer.OfferReferenceNumber);
            if (!alreadyOnTheList)
            {
                winningOffers.Add(offer);
            }
            else 
            {
                foreach(Offer winOffer in winningOffers)
                {
                    if (winOffer.OfferReferenceNumber == offer.OfferReferenceNumber)
                    {
                        winOffer.IsEligible = true; 
                    }
                }
                    
            }
         
        }
        public List<Offer> ReturnIneligibleOffers()
        {
            List<Offer> InEligibleOffersToReturn = new List<Offer>();
            foreach (Offer Offer in winningOffers)
            {
                if (!Offer.IsEligible)
                {
                    InEligibleOffersToReturn.Add(Offer);
                }
            }
            return InEligibleOffersToReturn;
        }
        public void RemoveIneligibleOffersFromWinningOffers()
        {
            List<Offer> toBeRemoved = new List<Offer>();
            foreach (Offer Offer in winningOffers)
            {
                if (!Offer.IsEligible)
                {
                    ineligibleOffers.Add(Offer);
                    toBeRemoved.Add(Offer);
                }
            }
            if (toBeRemoved.Count > 0)
            {
                foreach (Offer Offer in toBeRemoved)
                {
                    winningOffers.Remove(Offer);
                }
            }
        }         
        public List<Offer> CompareNumberOfWonOffersAgainstVehicles()
        {
            List<Offer> OffersWithConflict = new List<Offer>();
            type2 = 0;
            type3 = 0;
            type5 = 0;
            type6 = 0;
            type7 = 0;
            if (winningOffers.Count > 0)
            {
                foreach (Offer Offer in winningOffers)
                {
                    if (Offer.IsEligible)
                    {
                        if (Offer.RequiredVehicleType == 2)
                        {
                            type2++;
                        }
                        if (Offer.RequiredVehicleType == 3)
                        {
                            type3++;
                        }
                        if (Offer.RequiredVehicleType == 5)
                        {
                            type5++;
                        }
                        if (Offer.RequiredVehicleType == 6)
                        {
                            type6++;
                        }
                        if (Offer.RequiredVehicleType == 7)
                        {
                            type7++;
                        }
                    }
                }
            }
            if (winningOffers.Count > 0)
            {
                if (NumberOfType2PledgedVehicles == 0 && NumberOfType3PledgedVehicles==0 && NumberOfType5PledgedVehicles == 0 && NumberOfType6PledgedVehicles == 0 && NumberOfType7PledgedVehicles == 0)
                {
                    //If all pledged vehicles is 0, it means they have unlimited amount of vehicles available
                }
                else
                {
                    foreach (Offer Offer in IfTooManyWonOffers(NumberOfType2PledgedVehicles, type2, 2))
                    {
                        OffersWithConflict.Add(Offer);
                    }
                    foreach (Offer Offer in IfTooManyWonOffers(NumberOfType3PledgedVehicles, type3, 3))
                    {
                        OffersWithConflict.Add(Offer);
                    }
                    foreach (Offer Offer in IfTooManyWonOffers(NumberOfType5PledgedVehicles, type5, 5))
                    {
                        OffersWithConflict.Add(Offer);
                    }
                    foreach (Offer Offer in IfTooManyWonOffers(NumberOfType6PledgedVehicles, type6, 6))
                    {
                        OffersWithConflict.Add(Offer);
                    }
                    foreach (Offer Offer in IfTooManyWonOffers(NumberOfType7PledgedVehicles, type7, 7))
                    {
                        OffersWithConflict.Add(Offer);
                    }
                }
            }

            return OffersWithConflict;
        }
        private List<Offer> IfTooManyWonOffers(int numberOfPledgedVehicles, int numberOfWonOffersWithThisType, int type)
        {
            List<Offer> OffersToCheck = new List<Offer>();
            List<Offer> listOfOffersToReturn = new List<Offer>();
            foreach (Offer winningOffer in winningOffers)
            {
                if (winningOffer.IsEligible && winningOffer.RequiredVehicleType == type)
                {
                    OffersToCheck.Add(winningOffer);
                }
            }


            if (numberOfPledgedVehicles < numberOfWonOffersWithThisType)
            {
                if (numberOfPledgedVehicles == 0) //This is done because, sometimes contractors place bids on routenumbers, they don't have the correct vehicle type for. 
                {
                    foreach (Offer Offer in winningOffers)
                    {
                        if (Offer.RequiredVehicleType == type)
                        {
                            Offer.IsEligible = false;
                        }
                    }
                }
                else
                {
                    listOfOffersToReturn = FindOptimalWins(OffersToCheck, numberOfPledgedVehicles);
                }
            }

            return listOfOffersToReturn;
        }
        private List<Offer> FindOptimalWins(List<Offer> OffersToCheck, int numberOfPledgedVehicles)
        {
            List<Offer> OffersWithConflict = new List<Offer>();
            List<Offer> OffersToChooseFrom = OffersToCheck.OrderByDescending(x => x.DifferenceToNextOffer).ThenBy(x => x.ContractorPriority).ToList();

            foreach (Offer Offer in OffersToChooseFrom)
            {
                if (Offer.DifferenceToNextOffer >= OffersToChooseFrom[numberOfPledgedVehicles - 1].DifferenceToNextOffer)
                {
                    Offer.IsEligible = true;
                }
                else
                {
                    Offer.IsEligible = false;
                }
            }
          
            int eligibleOffers = 0;
            foreach (Offer Offer in OffersToChooseFrom)
            {
                if (Offer.IsEligible)
                {
                    eligibleOffers++;
                }
            }
            if (eligibleOffers > numberOfPledgedVehicles)
            {              
                if (OffersToChooseFrom[numberOfPledgedVehicles - 1].ContractorPriority != OffersToChooseFrom[numberOfPledgedVehicles].ContractorPriority)
                {
                    int length = OffersToCheck.Count;
              
                    for (int i = numberOfPledgedVehicles; i < length; i++)
                    {
                        if (OffersToChooseFrom[i].DifferenceToNextOffer == OffersToChooseFrom[numberOfPledgedVehicles - 1].DifferenceToNextOffer)
                        {
                            OffersToChooseFrom[i].IsEligible = false;
                        }
                    }
                }
                else
                {
                    foreach (Offer Offer in OffersToChooseFrom)
                    {
                        if (Offer.DifferenceToNextOffer == OffersToChooseFrom[numberOfPledgedVehicles-1].DifferenceToNextOffer && Offer.IsEligible)
                        {
                            OffersWithConflict.Add(Offer);
                        }
                    }
                    if (OffersWithConflict.Count == 1)
                    {
                        OffersWithConflict.Clear();
                    }
                }
                              
            }
            return OffersWithConflict;
        }
        public void CountNumberOfWonOffersOfEachType(List<Offer> outPutList)
        {
            NumberOfWonType2Offers = 0;
            NumberOfWonType3Offers = 0;
            NumberOfWonType5Offers = 0;
            NumberOfWonType6Offers = 0;
            NumberOfWonType7Offers = 0;


            foreach (Offer Offer in outPutList)
            {
                if (Offer.UserID == UserID)
                {
                    if (Offer.RequiredVehicleType == 2)
                    {
                        NumberOfWonType2Offers++;
                    }
                    if (Offer.RequiredVehicleType == 3)
                    {
                        NumberOfWonType3Offers++;
                    }
                    if (Offer.RequiredVehicleType == 5)
                    {
                        NumberOfWonType5Offers++;
                    }
                    if (Offer.RequiredVehicleType == 6)
                    {
                        NumberOfWonType6Offers++;
                    }
                    if (Offer.RequiredVehicleType == 7)
                    {
                        NumberOfWonType7Offers++;
                    }

                }
            }

        }
    }
}