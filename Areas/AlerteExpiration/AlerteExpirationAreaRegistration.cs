using System.Web.Mvc;

namespace StrangerRecord.Areas.AlerteExpiration
{
    public class AlerteExpirationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AlerteExpiration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AlerteExpiration_default",
                "AlerteExpiration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}