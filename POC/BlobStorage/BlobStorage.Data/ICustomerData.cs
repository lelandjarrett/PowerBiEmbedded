﻿using BlobStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.Data
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetCustomersByName(string name);
        Customer GetById(int id);
        Customer Update(Customer updatedCustomer);
        int Commit();
    }

    public class InMemoryCustomerData : ICustomerData
    {

        readonly List<Customer> customers;

        public InMemoryCustomerData()
        {
            customers = new List<Customer>()
            {
                new Customer{ ID = 1, Name = "BACK CONSTRUCTION LLC", Location = "Indianapolis", BussineType = BussinesType.Construction},
                new Customer{ ID = 2, Name = "Pavement Maintenance Solutions Inc", Location = "Chicago", BussineType = BussinesType.Paving},
                new Customer{ ID = 3, Name = "Landscape Direct", Location = "Washington", BussineType = BussinesType.LandScaping}
            };

        }
        
        public Customer GetById(int id)
        {
            return customers.SingleOrDefault(r => r.ID == id);
        }

   
        public Customer Update(Customer updatedCustomer)
        {
            var customer = customers.SingleOrDefault(r => r.ID == updatedCustomer.ID);
            if(customer !=null)
            {
                customer.Name = updatedCustomer.Name;
                customer.Location = updatedCustomer.Location;
                customer.BussineType = updatedCustomer.BussineType;
            }
            return customer;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Customer> GetCustomersByName(string name = null)
        {
            return from r in customers
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        
    }
}
