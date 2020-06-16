using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    /// <summary>
    /// This interface is consists functions thatworks with the database
    /// </summary>
    public interface Idal
    {
        /// <summary>
        /// This function is adding a guest request to the list
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
        /// <summary>
        /// This function is returning the list of all the Host
        /// </summary>
        /// <returns>List<Host></returns>
        List<Host> AllHosts();

        //***Additional functions***//
        /// <summary>
        /// The function geting a key and returning GuestRequest by the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>GuestRequest</returns>
        GuestRequest GetGuestRequest(int key);
        /// <summary>
        /// The function geting a key and returning HostingUnit by the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>HostingUnit</returns>
        HostingUnit GetHostingUnit(int key);
        /// <summary>
        /// The function geting a key and returning Order by the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Order</returns>
        Order GetOrder(int key);
        /// <summary>
        /// The function is returning a list of all the orders of one guest request by the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>List<Order></returns>
        List<Order> CustomerReqOrder(int key);
        /// <summary>
        /// This function is returning a list of all the orders of one hosting unit by a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>List<Order></returns>
        List<Order> HostingUnitOrder(int key);
        int GetNumDaysExpire();
        DateTime GetLastDate();
        void SetLastDate();
    }
}
