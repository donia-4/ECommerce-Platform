using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.DTO.Stripe;
using Ecommerce.Entities.Shared.Bases;

namespace Ecommerce.DataAccess.Services.Stripe
{
    public interface IStripeService
    {
        Task<Response<CreateCheckoutSessionResponse>> CreateCheckoutSessionAsync(CreateCheckoutSessionRequest request);
        Task<Response<CreatePaymentIntentResponse>> CreatePaymentIntentAsync(CreatePaymentIntentRequest request);
        Task<Response<object>> HandleWebhookAsync(string json, string stripeSignature);
    }
}
