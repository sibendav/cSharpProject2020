using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{

    public enum Branches { Discount, Jerusalem, yahav, pualim, pagi, marcantil, mizrachiTpahut, leumi, benlumi, israel, ozarhachail, decsia, masad };
    public enum ResortType { Zimmer, AccommodationApartment, HotelRoom, Encampment };
    public enum Area { North, South, Central, Jerusalem };
    public enum OrderStatus { NotAddressed, SentEmail, ClosedForCustomerUnresponsiveness, ClosedForCustomerResponse };
    public enum CustomerRequirementStatus { IsOpen, ATransactionHasBeenClosedThroughTheSite, ClosedBecauseItHasExpired };
    public enum Extra { Necessary, possible, NotInterested };
    public enum Banks { Leumi = 10, Hapoalim = 12, Mizrachi = 20, Diskont = 11, Markentil = 17, OtzarHachayal = 14, UBank = 26, Pagi = 52, Igud = 13, Yahav = 4, Hadoar = 09, Israel = 99, Jrusalem = 54 }
}
