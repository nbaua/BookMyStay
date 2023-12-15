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
        #endregion

        #region Authentication
        public static string AuthAPIDefaultRoute= "/api/auth";
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

        #region Listing
        public static string ListingApiCreateGetAllListings = "/api/listing";
        public static string ListingApiGetOrDeleteListingById = "/api/listing";

        public const string ListingCreated = "Listing Created Successfully!";
        public const string ListingUpdated = "Listing Updated Successfully!";
        public const string ListingDeleted = "Listing Deleted Successfully!";
        #endregion

        #region Booking
        public static string BookingApiApplyOfferOnBooking= "/api/booking/ManageOffer";
        public static string BookingApiGetBookingsByUserId= "/api/booking/GetBookings";
        public static string BookingApiDeleteBookingsByDetailsId= "/api/booking/Delete";
        public static string BookingApiManageBooking= "/api/booking/Manage";

        public const string BookingCreated = "Booking Created Successfully!";

        #endregion
    }
}
