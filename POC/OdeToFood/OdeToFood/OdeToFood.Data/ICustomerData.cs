using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public interface ICustomerData
    {
        IEnumerable<Customers> GetAll();
    }

    public class InMemeroyCustomerData : ICustomerData
    {
        List<Customers> customers;
        public InMemeroyCustomerData()
        {
            customers = new List<Customers>()
            {
                new Customers { Id = 1, Name = "Rick Campbell Builder", Location = "Greenwood", Business = BusinessType.Construction },
                new Customers { Id = 1, Name = "Garrity Stone, Inc", Location = "Indianapolis", Business = BusinessType.Paving },
                new Customers { Id = 2, Name = "Adam Albers Landscaping", Location = "Mooresville", Business = BusinessType.LandScape }
            };
        }
        public IEnumerable<Customers> GetAll()
        {
            return from r in customers
                   orderby r.Name
                   select r;
        }
    }
}
