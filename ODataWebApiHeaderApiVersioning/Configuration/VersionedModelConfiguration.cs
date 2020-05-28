using ODataWebApiHeaderApiVersioning.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace ODataWebApiHeaderApiVersioning.Configuration
{
    public class VersionedModelConfiguration : IModelConfiguration
    {
        private void ConfigureV1(ODataModelBuilder builder)
        {
            builder.EntitySet<Movie>("Movies");
            builder.EntityType<Movie>().Function("GetRating")
                 .Returns<int>();
            builder.Function("GetRatingUnbound")
                .Returns<int>()
                .Parameter<int>("movieId");
        }

        private void ConfigureV2(ODataModelBuilder builder)
        {
            builder.EntitySet<Movie>("Movies");
            builder.EntityType<Movie>().Function("GetRevenue")
                 .Returns<int>();
            builder.Function("GetRevenueUnbound")
                .Returns<int>()
                .Parameter<int>("movieId");
        }

        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {

            switch (apiVersion.MajorVersion)
            {
                case 1:
                    ConfigureV1(builder);
                    break;
                case 2:
                    ConfigureV2(builder);
                    break;
                default:
                    ConfigureV1(builder);
                    break;
            }
        }
    }
}
