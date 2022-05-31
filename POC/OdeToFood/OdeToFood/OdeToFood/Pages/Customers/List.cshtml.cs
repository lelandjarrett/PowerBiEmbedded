using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;

namespace OdeToFood.Pages.Customers
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly ICustomerData customerData;

        public string Message { get; set; }

        public ListModel(IConfiguration config, 
                         ICustomerData customerData)
        {
            this.config = config;
            this.customerData = customerData;
        }

        public void OnGet()
        {
            Message = config["Message"];
        }
    }
}
