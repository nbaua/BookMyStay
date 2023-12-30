namespace BookMyStay.WebApp.Helpers
{
    public class Constants
    {

        #region General
        public static string TokenKeyPrefix = "AUTH_TOKEN";
        public static string AuthApiEndPoint { get; set; }
        public static string OfferApiEndPoint { get; set; }
        public static string ListingApiEndPoint { get; set; }
        public static string BookingApiEndPoint { get; set; }
        public static string PaymentApiEndPoint { get; set; }
        public static string DBLoggerApiEndPoint { get; set; }

        public static string BrokerMessageQueue = "BMSMessage";
        public static string BrokerCheckoutQueue = "BMSCheckout";
        public static string BrokerPaymentQueue = "BMSPayment";

        #endregion

        #region Authentication
        public static string AuthAPIDefaultRoute = "/api/auth";
        public static string AuthAPILoginRoute = "/api/auth/login";
        public static string AuthAPIRegisterRoute = "/api/auth/register";
        public static string AuthAPIAssignRoleRoute = "/api/auth/assignRole";

        public const string UserRegistered = "User Registered Successfully!";
        public const string UserLoggedIn = "User Logged-In Successfully!";
        public const string UserNotLoggedIn = "User Not Logged-In, Check Credentials!";
        #endregion

        #region Offer
        public static string OfferApiCreateGetAllOffers = "/api/offer";
        public static string OfferApiGetOrDeleteOfferById = "/api/offer";
        public static string OfferApiGetOfferByCode = "/api/offer/code";

        public const string OfferCreated = "Offer Created Successfully!";
        public const string OfferUpdated = "Offer Updated Successfully!";
        public const string OfferDeleted = "Offer Deleted Successfully!";
        #endregion

        #region Payment
        public static string PaymentApiCreatePaymentRequest = "/api/payment/create";
        #endregion

        #region Listing
        public static string ListingApiCreateGetAllListings = "/api/listing";
        public static string ListingApiGetOrDeleteListingById = "/api/listing";

        public const string ListingCreated = "Listing Created Successfully!";
        public const string ListingUpdated = "Listing Updated Successfully!";
        public const string ListingDeleted = "Listing Deleted Successfully!";
        #endregion

        #region Booking
        public static string BookingApiApplyOfferOnBooking = "/api/booking/ManageOffer";
        public static string BookingApiGetBookingsByUserId = "/api/booking/GetBookings";
        public static string BookingApiDeleteBookingsByDetailsId = "/api/booking/Delete";
        public static string BookingApiManageBooking = "/api/booking/Manage";

        public const string BookingCreated = "Booking Created Successfully!";
        public const string BookingRemoved = "Booking Removed Successfully!";
        public const string OfferCodeApplied = "Offer Code Applied Successfully!";
        public const string OfferCodeNotApplied = "Offer Code Not Applied!";
        public const string OfferCodeRemoved = "Offer Code Removed Successfully!";

        #endregion

        #region DBLogger
        public static string DBLoggerApiLogQueue = "/api/logger/LogQueue";

        public const string DBLogCreated = "DB Log Created Successfully!";
        public const string DBLogNotCreated = "DB Log Can Not Be Created!";
        #endregion

    }
}
