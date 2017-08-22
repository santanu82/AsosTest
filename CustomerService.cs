using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalService;
using CompanyModel;

namespace AppService
{
    public class CustomerService : ICustomerService
    {
        private ICustomerWcfService _customerWcfService;
        private ICompanyWcfService _companyWcfService;
        //Dependancy injection via constructor injection
        public CustomerService(ICustomerWcfService customerWcfService, ICompanyWcfService companyWcfService)
        {
            _customerWcfService = customerWcfService;
            _companyWcfService = companyWcfService;
        }
        public CustomerService()
        {

        }
        CustomerWcfService myWcfService = new CustomerWcfService();
        CompanyWcfService myCompanyWcfService = new CompanyWcfService();
        /// <summary>
        /// this method add customer to the database if the customer is valid and credit limit is equal or more than 500
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="comapnyId"></param>
        /// <returns></returns>
        public bool AddCustomer(string firstName, string lastName, string email, DateTime dateOfBirth, int comapnyId)
        {
            bool isCustomerAddedtoDB = true;
            int creditLimit = GetCustomerCreditLimit(comapnyId);
            if (ValidateCustomer(firstName,lastName,email,dateOfBirth) && creditLimit >= 500)
            {
                isCustomerAddedtoDB = true;
            }
            else
            {
                isCustomerAddedtoDB = false;
            }
            return isCustomerAddedtoDB;
        }

        /// <summary>
        /// This method returns the credit limit for the customer depending up on the company status
        /// </summary>
        /// <param name="companyStatus"></param>
        /// <returns></returns>
        public int GetCustomerCreditLimit(string companyStatus)
        {
            
            int creditLimit = 0;
            if (companyStatus == "VeryImportantClient")
            {
                creditLimit =  Int32.MaxValue;
            }
            if (companyStatus == "ImportantClient")
            {
                creditLimit = 2*(myWcfService.GetCreditLimit());
            }
            if (!String.IsNullOrEmpty(companyStatus) && companyStatus != "VeryImportantClient"
                && companyStatus != "ImportantClient")
            {
                creditLimit = myWcfService.GetCreditLimit();
            }
            return creditLimit;
        }
        /// <summary>
        /// This method returns the credit limit for the customer depending up on the CompanyId
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int GetCustomerCreditLimit(int companyId)
        {
            var companyData = myCompanyWcfService.GetCompany(companyId);
            var companyStatus = companyData.CompanyStatus;
            int creditLimit = 0;
            if (companyStatus == "VeryImportantClient")
            {
                creditLimit = Int32.MaxValue;
            }
            if (companyStatus == "ImportantClient")
            {
                creditLimit = 2 * (myWcfService.GetCreditLimit());
            }
            if (!String.IsNullOrEmpty(companyStatus) && companyStatus != "VeryImportantClient"
                && companyStatus != "ImportantClient")
            {
                creditLimit = myWcfService.GetCreditLimit();
            }
            return creditLimit;
        }
        /// <summary>
        /// This method validate the customer
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public bool ValidateCustomer(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            bool isValidCustomer = true;
            if (!String.IsNullOrEmpty(firstName)
                && !String.IsNullOrEmpty(firstName)
                && !String.IsNullOrEmpty(email)
                && dateOfBirth < DateTime.Now.Date)
            {
                isValidCustomer = true;
            }
            else
            {
                isValidCustomer = false;
            }
            return isValidCustomer;
        }
    }
}
