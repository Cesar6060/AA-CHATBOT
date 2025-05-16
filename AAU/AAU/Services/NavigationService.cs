using Microsoft.AspNetCore.Components;
namespace AAU.Services
{
    public class NavigationService
    {
        private readonly NavigationManager _navigationManager;

        public NavigationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
      
        public void NavigateTo(string endpoint) {
            Console.Write($"Navigating to {endpoint}");
            _navigationManager.NavigateTo($"/{endpoint}", true);
        }
        
        public void NavigateToParams(string endpoint, Dictionary<string, object>? parameters = null)
        {
            var url = $"/{endpoint}";
            if (parameters != null && parameters.Any())
            {
                var queryString = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                url = $"{url}?{queryString}";
            }
            Console.Write($"Navigating to {url}");
            _navigationManager.NavigateTo(url);
        }
    }
    
}
