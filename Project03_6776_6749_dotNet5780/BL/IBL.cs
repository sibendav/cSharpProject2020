using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;


namespace BL
{
    public interface IBL
    {
        #region//***Impliment of the same function in the iDal interface***//
        /// <summary>
        /// This function is adding a guest request to the list
        /// add checking if it's already exist
        /// </summary>
        /// <param name="gr"></param>
        /// <returns>GuestRequest</returns>
        GuestRequest AddCustomerReq(GuestRequest gr);
        /// <summary>
        /// This function is updating guest request
        /// </summary>
        /// <param name="gr"></param>
        /// <returns>GuestRequest</returns>
        GuestRequest UpdateCustomerReq(GuestRequest gr);
        /// <summary>
        /// The function is updating all the guest request passwords
        /// </summary>
        /// <param name="gr"></param>
        /// <returns></returns>
        GuestRequest UpdateAllPasswordCustomerReq(GuestRequest gr);

        /// <summary>
        /// This function is adding a new hosting unit to the list
        /// </summary>
        /// <param name="hu"></param>
        /// <returns>HostingUnit</returns>
        HostingUnit AddHostingUnit(HostingUnit hu);
        /// <summary>
        /// This function is deleting HostingUnit
        /// </summary>
        /// <param name="hu"></param>
        /// <returns>bool</returns>
        bool DeleteHostingUnit(HostingUnit hu);
        /// <summary>
        /// This function is updating HostingUnit
        /// </summary>
        /// <param name="hu"></param>
        /// <returns>HostingUnit</returns>
        HostingUnit UpdateHostingUnit(HostingUnit hu);

        /// <summary>
        /// This function is adding a new order to the list
        /// </summary>
        /// <param name="or"></param>
        /// <returns>Order</returns>
        Order AddOrder(Order or);
        /// <summary>
        /// This function is updating order
        /// </summary>
        /// <param name="or"></param>
        /// <returns>Order</returns>
        Order UpdateOrder(Order or);
        #endregion

        #region//***list returns functions***//
        /// <summary>
        /// This function is returning the list of all the HostingUnits
        /// </summary>
        /// <returns>List<HostingUnit></returns>
        List<HostingUnit> AllHostingUnits();
        /// <summary>
        /// This function is returning the list of all the GuestRequest
        /// </summary>
        /// <returns>List<GuestRequest></returns>
        List<GuestRequest> AllGuestRequests();
        /// <summary>
        /// This function is returning the list of all the Order
        /// </summary>
        /// <returns>List<Order></returns>
        List<Order> AllOrders();
        /// <summary>
        /// This function is returning the list of all the BankBranch
        /// </summary>
        /// <returns>List<BankBranch></returns>
        List<BankBranch> AllBankBranch();
        #endregion

        #region//***Special functions for the ibl interface***//        
        /// <summary>
        /// This function is returning a list of all the available hosting unit in dates that we get
        /// </summary>
        /// <param name="begining"></param>
        /// <param name="numDays"></param>
        /// <returns>List<HostingUnit></returns>
        List<HostingUnit> AvailableHostingUnit(DateTime begining, int numDays);
        /// <summary>
        /// The function is returning the num of passing days 
        /// </summary>
        /// <param name="list"></param>
        /// <returns>int</returns>
        int PassedDays(params DateTime[] list);
        /// <summary>
        /// The function is returning list of order that expired
        /// </summary>
        /// <param name="numDays"></param>
        /// <returns>List<Order></returns>
        List<Order> AllExpiredOrders(int numDays);
        /// <summary>
        /// The function is returning a list of all the guest request that relevante to the requirments
        /// </summary>
        /// <param name="myRequirments"></param>
        /// <returns>List<GuestRequest></returns>
        List<GuestRequest> GuestRequestsRequirment(Func<GuestRequest, bool> myRequirments);//delegate
        /// <summary>
        /// The function is calculating the num of send orders
        /// </summary>
        /// <param name="gr"></param>
        /// <returns>int</returns>
        int NumOfSendingOrders(GuestRequest gr);
        /// <summary>
        /// The function is calculating the num of send and closed order 
        /// </summary>
        /// <param name="hu"></param>
        /// <returns>int</returns>
        int NumOfSendingOrdersAndClosedDeals(HostingUnit hu);
        #endregion

        #region//***Grouping fuctiond***//
        /// <summary>
        /// Group of all the guest request by area
        /// </summary>
        /// <returns>IEnumerable<IGrouping<Area, GuestRequest>></returns>
        IEnumerable<IGrouping<Area, GuestRequest>> GroupGuestReqByArea();
        /// <summary>
        /// Group of all the guest request by num vacationers
        /// </summary>
        /// <returns>IEnumerable<IGrouping<int, GuestRequest>></returns>
        IEnumerable<IGrouping<int, GuestRequest>> GroupGuestReqBynumVacationers();
        /// <summary>
        /// Group of all the hosting units by num hosting units
        /// </summary>
        /// <returns>IEnumerable<IGrouping<int, Host>></returns>
        IEnumerable<IGrouping<int, Host>> GroupHostByNumHostingUnits();
        /// <summary>
        /// Group of all the hosting units by area
        /// </summary>
        /// <returns>IEnumerable<IGrouping<Area, HostingUnit>></returns>
        IEnumerable<IGrouping<Area, HostingUnit>> GroupHostingUnitsByArea();
        #endregion

        #region//***Creative functions***//
        /// <summary>
        /// The function is returning all the relevante hosting units for a guest request
        /// </summary>
        /// <param name="gr"></param>
        /// <returns>List<HostingUnit></returns>
        List<GuestRequest> FindTheRelevanteGuestRequermentsHostingUnit(HostingUnit gr);
        /// <summary>
        /// The function is returning a gr list
        /// </summary>
        /// <param name="gr"></param>
        /// <returns></returns>
        List<GuestRequest> FindAllThePriviaseGR(GuestRequest gr);
        /// <summary>
        /// The function is returning a hu list
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        List<HostingUnit> FindAllThePriviaseHU(Host h);
        /// <summary>
        /// The function is returning all the priviase orders for the gr
        /// </summary>
        /// <param name="gr"></param>
        /// <returns>List<Order></returns>
        List<Order> FindAllThePriviaseOrdersForGR(GuestRequest gr);
        /// <summary>
        /// The function is returning all the priviase orders of hu
        /// </summary>
        /// <param name="gr"></param>
        /// <returns>List<Order></returns>
        List<Order> FindAllThePriviaseOrdersForHU(HostingUnit hu);
        /// <summary>
        /// The function is adding the same gr with defirente dates
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="dateEnter"></param>
        /// <param name="dateRelease"></param>
        /// <returns>GuestRequest</returns>
        GuestRequest AddTheSameGuestRequest(GuestRequest gr);
        /// <summary>
        /// The function is returning  a list of all the available hosting units in the required dates
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>List<HostingUnit></returns>
        List<HostingUnit> AvailableHostinngUnitInTheRequiredPeriod(DateTime d1, DateTime d2);
        /// <summary>
        /// The function is calculating the persent of annoal capacity
        /// </summary>
        /// <param name="hu"></param>
        /// <returns>double</returns>
        double ThePercentOfCapacity(HostingUnit hu);
        /// <summary>
        /// the function is returning a gr object
        /// </summary>
        /// <param name="un"></param>
        /// <returns></returns>
        GuestRequest getGRByUserName(string un);
        /// <summary>
        /// The function is returning a host object
        /// </summary>
        /// <param name="un"></param>
        /// <returns></returns>
        Host getHostByUserName(string un);
        #endregion
    }
}
