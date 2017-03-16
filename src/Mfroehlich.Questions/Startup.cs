using Mfroehlich.Common.HttpOptions;
using Mfroehlich.Questions.Converters;
using Mfroehlich.Questions.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Mfroehlich.Questions
{
    public class Startup
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseHttpOptions<QuestionsOptions>()
                .UseStartup<Startup>()
                .UseKestrel()
                .Build();

            host.Run();
        }

        private QuestionsOptions options;

        public Startup(IOptions<QuestionsOptions> options)
        {
            this.options = options.Value;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(json => {
                json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                json.SerializerSettings.Converters.Add(new QuestionConverter());
                json.SerializerSettings.Converters.Add(new StringEnumConverter(true));
            });

            var context = new QuestionContext("questions.sqlite");
            context.Database.EnsureCreated();
            services.AddSingleton(context);

            services.AddScoped<IQuestionRepository, QuestionRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder => {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins(options.Origins);
            });

            app.UseMvc();
        }
    }
}