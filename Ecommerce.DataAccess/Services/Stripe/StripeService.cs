using Ecommerce.DataAccess.Services.Stripe;
using Ecommerce.Entities.DTO.Stripe;
using Ecommerce.Entities.Shared.Bases;
using Ecommerce.Utilities.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using Stripe;
using Microsoft.IdentityModel.Tokens;
using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.Entities.Models;
using Ecommerce.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

public class StripeService : IStripeService
{
    private readonly ResponseHandler _responseHandler;
    private readonly ILogger<StripeService> _logger;
    private readonly StripeSettings _stripeSettings;

    public StripeService(
        IOptions<StripeSettings> stripeSettings,
        ResponseHandler responseHandler,
        ILogger<StripeService> logger)
    {
        _stripeSettings = stripeSettings.Value;
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        _responseHandler = responseHandler;
        _logger = logger;
    }

    public async Task<Response<CreateCheckoutSessionResponse>> CreateCheckoutSessionAsync(CreateCheckoutSessionRequest request)
    {
        _logger.LogInformation(
            "Starting CreateCheckoutSessionAsync for Products: {Products}, Currency: {Currency}",
            string.Join(", ", request.ProductNames),
            request.Currency);

        try
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = request.SuccessUrl,
                CancelUrl = request.CancelUrl
            };

            for (int i = 0; i < request.ProductNames.Count; i++)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = request.Currency,
                        UnitAmount = request.UnitAmounts[i],
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = request.ProductNames[i]
                        }
                    },
                    Quantity = request.Quantities[i]
                });

                _logger.LogDebug(
                    "Added line item: {Name}, Quantity: {Quantity}, UnitAmount: {UnitAmount}",
                    request.ProductNames[i],
                    request.Quantities[i],
                    request.UnitAmounts[i]);
            }

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            _logger.LogInformation(
                "Checkout session created successfully. SessionId: {SessionId}, Url: {Url}",
                session.Id,
                session.Url);

            return _responseHandler.Success(
                new CreateCheckoutSessionResponse
                {
                    SessionId = session.Id,
                    Url = session.Url
                },
                "Checkout session created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating checkout session");
            return _responseHandler.ServerError<CreateCheckoutSessionResponse>("Failed to create checkout session.");
        }
    }

    public async Task<Response<CreatePaymentIntentResponse>> CreatePaymentIntentAsync(CreatePaymentIntentRequest request)
    {
        _logger.LogInformation(
            "Starting CreatePaymentIntentAsync. Amount: {Amount}, Currency: {Currency}, Description: {Description}",
            request.Amount,
            request.Currency,
            request.Description);

        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
                Description = request.Description,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);

            _logger.LogInformation(
                "PaymentIntent created successfully. ClientSecret: {ClientSecret}",
                intent.ClientSecret);

            return _responseHandler.Success(
                new CreatePaymentIntentResponse
                {
                    ClientSecret = intent.ClientSecret
                },
                "Payment intent created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating payment intent");
            return _responseHandler.ServerError<CreatePaymentIntentResponse>("Failed to create payment intent.");
        }
    }

    public async Task<Response<object>> HandleWebhookAsync(string json, string stripeSignature)
    {
        _logger.LogInformation(
            "Received webhook event. Signature: {Signature}",
            stripeSignature);

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                stripeSignature,
                _stripeSettings.WebhookSecret,
                throwOnApiVersionMismatch: false);

            _logger.LogInformation(
                "Stripe Event received: {EventType}",
                stripeEvent.Type);

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                _logger.LogInformation(
                    "Checkout session completed successfully. SessionId: {SessionId}",
                    session?.Id);
            }

            return _responseHandler.Success<object>(
                null,
                "Webhook handled successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling webhook");
            return _responseHandler.ServerError<object>("Webhook handling failed.");
        }
    }
}
