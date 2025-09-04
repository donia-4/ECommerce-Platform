using Ecommerce.DataAccess.Services.Stripe;
using Ecommerce.Entities.DTO.Stripe;
using Ecommerce.Entities.Shared.Bases;
using Ecommerce.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService _stripeService;
        private readonly ResponseHandler _responseHandler;
        private readonly IValidator<CreateCheckoutSessionRequest> _checkoutValidator;
        private readonly IValidator<CreatePaymentIntentRequest> _paymentValidator;
        private readonly IValidator<CashOnDeliveryRequest> _cashOnDeliveryValidator;



        public StripeController(
            IStripeService stripeService,
            ResponseHandler responseHandler,
            IValidator<CreateCheckoutSessionRequest> checkoutValidator,
            IValidator<CreatePaymentIntentRequest> paymentValidator,
            IValidator<CashOnDeliveryRequest> cashOnDeliveryValidator)
        {
            _stripeService = stripeService;
            _responseHandler = responseHandler;
            _checkoutValidator = checkoutValidator;
            _paymentValidator = paymentValidator;
            _cashOnDeliveryValidator = cashOnDeliveryValidator;
        }

        [HttpPost("checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
        {
            ValidationResult validation = await _checkoutValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                    _responseHandler.BadRequest<object>(errors));
            }

            var response = await _stripeService.CreateCheckoutSessionAsync(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
        {
            ValidationResult validation = await _paymentValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                    _responseHandler.BadRequest<object>(errors));
            }

            var response = await _stripeService.CreatePaymentIntentAsync(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            using var reader = new StreamReader(HttpContext.Request.Body);
            var json = await reader.ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"];

            var response = await _stripeService.HandleWebhookAsync(json, signature);
            return StatusCode((int)response.StatusCode, response);
        }
        //[HttpPost("cash-on-delivery")]
        //public async Task<IActionResult> ProcessCashOnDelivery([FromBody] CashOnDeliveryRequest request)
        //{
        //    var validationResult = await _cashOnDeliveryValidator.ValidateAsync(request);
        //    if (!validationResult.IsValid)
        //    {
        //        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
        //        return BadRequest(_responseHandler.BadRequest<object>(string.Join(", ", errorMessages)));
        //    }

        //    var response = await _stripeService.HandleCashOnDeliveryAsync(request);
        //    return StatusCode((int)response.StatusCode, response);
        //}
    }
}
