using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService
{
    public interface ICustomerService
    {
        bool AddCustomer(string firstName, string lastName, string email, DateTime dateOfBirth, int comapnyId);

        int GetCustomerCreditLimit(string companyStatus);

        int GetCustomerCreditLimit(int companyId);

        bool ValidateCustomer(string firstName, string lastName, string email, DateTime dateOfBirth);

    }
}
