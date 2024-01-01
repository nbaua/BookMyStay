namespace BookMyStay.PaymentAPI.Models
{
    public class PaymentGatewayRequestDTO
    {
        //Stripe Payment Gateway Request Items
        public string? SessionId { get; set; }
        public string? SessionUrl { get; set; }
        public string SuccessUrl { get; set; } //On Payment Success 
        public string FailureUrl { get; set; } //On Error - Cancel

        public PaymentItemDTO PaymentItem { get; set; }
    }
}
