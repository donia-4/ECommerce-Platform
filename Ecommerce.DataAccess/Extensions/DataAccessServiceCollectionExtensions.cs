using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.DataAccess.Services.Auth;
using Ecommerce.DataAccess.Services.Cart;
using Ecommerce.DataAccess.Services.Category;
using Ecommerce.DataAccess.Services.Discount;
using Ecommerce.DataAccess.Services.Email;
using Ecommerce.DataAccess.Services.ImageUploading;
using Ecommerce.DataAccess.Services.OAuth;
using Ecommerce.DataAccess.Services.OTP;
using Ecommerce.DataAccess.Services.ProductBuyer;
using Ecommerce.DataAccess.Services.ProductService;
using Ecommerce.DataAccess.Services.Token;
using Ecommerce.DataAccess.Services.Wishlist;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utilities.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;
using Stripe;
using Ecommerce.DataAccess.Services.Stripe;
using Ecommerce.DataAccess.Services.Order;

namespace Ecommerce.DataAccess.Extensions
{
    public static class DataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ProdCS")));

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IImageUploadService, CloudinaryImageUploadService>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthGoogleService, AuthGoogleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDiscountService, Services.Discount.DiscountService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IProductService, Services.ProductService.ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<IProductBuyerService, ProductBuyerService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }

        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

            services.AddFluentEmail(emailSettings.FromEmail)
                .AddSmtpSender(new SmtpClient(emailSettings.SmtpServer)
                {
                    Port = emailSettings.SmtpPort,
                    Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                    EnableSsl = emailSettings.EnableSsl
                });

            return services;
        }
        public static IServiceCollection AddStripeConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var stripeSettings = configuration.GetSection("Stripe").Get<StripeSettings>();

            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = stripeSettings.SecretKey;
            return services;
        }

    }
}
