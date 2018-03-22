using System.Collections.Generic;
using Domain;
using DataAccess;

namespace Logic
{
    public class IOController
    {
        List<RouteNumber> routeNumberList;
        List<Contractor> contractorList;
        List<Offer> listOfHours;

        public IOController()
        {
            routeNumberList = new List<RouteNumber>();
        }
       
        public void InitializeExportToPublishList(string filePath)
        {
            CSVExportToPublishList ExportToPublishList = new CSVExportToPublishList(filePath);
            ExportToPublishList.CreateFile(); 
        }
        public void InitializeExportToCallingList(string filePath)
        {
            CSVExportToCallList ExportCallList = new CSVExportToCallList(filePath);
            ExportCallList.CreateFile();
        }
        public void InitializeImport(string masterDataFilePath, string routeNumberFilePath)
        {
            CSVImport csvImport = new CSVImport();
            csvImport.ImportContractors(masterDataFilePath);
            csvImport.ImportRouteNumbers();
            csvImport.ImportOffers(routeNumberFilePath);
            contractorList = csvImport.SendContractorListToContainer();
            routeNumberList = csvImport.SendRouteNumberListToContainer();
            listOfHours = csvImport.SendHoursListToContainer();
            ListContainer listContainer = ListContainer.GetInstance();
            listContainer.GetLists(routeNumberList, contractorList, listOfHours);
        }
    }
}
