namespace WakeUpServer.Api.Home
{
    using Microsoft.AspNetCore.Mvc;
    using WakeUpServer.Api.WakeUp;

    [Microsoft.AspNetCore.Components.Route(ApiConstants.Route)]
    public class HomeController : ApiController
    {
        private readonly UrlBuilder urlBuilder;

        public HomeController(UrlBuilder urlBuilder)
        {
            this.urlBuilder = urlBuilder;
        }

        [HttpGet]
        public ApiHomeInfo Retrieve()
        {
            return new ApiHomeInfo(new Url(this.urlBuilder.Build(ApiConstants.Route, nameof(WakeUpServiceController))));
        }
    }
}