using BlobStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.Data
{
    public interface ICustomerImage
    {
        IEnumerable<CustomerImages> GetImagesById(int id);
        CustomerImages GetImageID(int id);
        //CustomerImages GetById(int customerid);
        CustomerImages Update(CustomerImages updatedCustomerImage);
        int Commit();

    }

    public class InMemoryCustomerImage : ICustomerImage
    {
        List<CustomerImages> customerImages;
        public InMemoryCustomerImage()
        {
            customerImages = new List<CustomerImages>()
            {
                new CustomerImages{ ID = 1, CustomerID = 1, FileName = "Contract" },
                new CustomerImages{ ID = 2, CustomerID = 2, FileName = "Contract" },
                new CustomerImages{ ID = 3, CustomerID = 3, FileName = "Contract" },
                new CustomerImages{ ID = 4, CustomerID = 1, FileName = "DamagedTracker" },
                new CustomerImages{ ID = 5, CustomerID = 2, FileName = "BullDozer" },
                new CustomerImages{ ID = 6, CustomerID = 3, FileName = "Mower" }
            };
        }

        public CustomerImages GetImageID(int id)
        {
            return customerImages.SingleOrDefault(r => r.ID == id);
        }


        public CustomerImages Update(CustomerImages updatedCustomerImage)
        {
            var customerImage = customerImages.SingleOrDefault(r => r.ID == updatedCustomerImage.ID);
            if (customerImage != null)
            {
                customerImage.FileName = updatedCustomerImage.FileName;
                customerImage.CustomerID = updatedCustomerImage.CustomerID;
            }
            return customerImage;
        }

        public int Commit()
        {
            return 0;
        }


        public IEnumerable<CustomerImages> GetImagesById(int id)
        {
            return from r in customerImages
                   where r.CustomerID == id
                   orderby r.FileName
                   select r;
        }
    }


}