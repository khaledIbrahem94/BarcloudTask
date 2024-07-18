using NETCore.MailKit.Extensions;

namespace BarcloudTask.API.Extensions;

public static class EmailService
{
    public static IHostApplicationBuilder AddEmailService(this IHostApplicationBuilder builder)
    {
        //Add MailKit
        builder.Services.AddMailKit(optionBuilder =>
        {
            optionBuilder.UseMailKit(new NETCore.MailKit.Infrastructure.Internal.MailKitOptions()
            {
                //get options from sercets.json
                Server = builder.Configuration["Email:SMTP"],
                Port = Convert.ToInt32(builder.Configuration["Email:Port"]),
                SenderName = builder.Configuration["Email:Name"],
                SenderEmail = builder.Configuration["Email:Email"],

                // can be optional with no authentication
                Account = builder.Configuration["Email:Account"],
                Password = builder.Configuration["Email:Password"],
                // enable ssl or tls
                Security = true
            });
        });
        return builder;
    }
}
