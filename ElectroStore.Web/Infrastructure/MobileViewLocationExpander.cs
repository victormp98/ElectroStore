using Microsoft.AspNetCore.Mvc.Razor;

namespace ElectroStore.Web.Infrastructure
{
    public class MobileViewLocationExpander : IViewLocationExpander
    {
        private const string ValueKey = "istechnologymobile";

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var userAgent = context.ActionContext.HttpContext.Request.Headers["User-Agent"].ToString();
            var isMobile = userAgent.Contains("Android") || userAgent.Contains("iPhone") || userAgent.Contains("iPad");
            context.Values[ValueKey] = isMobile.ToString();
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var isMobile = false;

            if (context.Values.TryGetValue(ValueKey, out var isMobileString))
            {
                bool.TryParse(isMobileString, out isMobile);
            }

            if (isMobile)
            {
                foreach (var location in viewLocations)
                {
                    if (location.Contains("{0}"))
                    {
                         yield return location.Replace("{0}", "{0}.Mobile");
                    }
                    yield return location;
                }
            }
            else
            {
                foreach (var location in viewLocations)
                {
                    yield return location;
                }
            }
        }
    }
}
