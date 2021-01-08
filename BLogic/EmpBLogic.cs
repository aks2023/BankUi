using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using BEntity;


namespace BLogic
{
    public class EmpBLogic
    {
        
        EmpDal ob = new EmpDal();
        public int ValidateEmployee(string username, string password)
        {
            return ob.ValidateEmployee(username, password);

        }

        public int ManagerLogin(string username, string password)
        {
            return ob.ManagerLogin(username, password);

        }

        public int Clogin(string username, string password)
        {
            return ob.Clogin(username, password);
        }

        public int ApplyLoanLogin(string username, string password)
        {
            return ob.ApplyLoanLogin(username, password);

        }
        public int ValidatePanCus(string p)
        {
            return ob.VlidatePanCus(p);

        }

        public int CustIdValidation(String custid)
        {
            return ob.CustIdValidation(custid);

        }


        public int ValidatePanEmp(string p)
        {
            return ob.VlidatePanEmp(p);

        }

        public int CustomerRegistration(CustomerEntity s, string balance)
        {
            return ob.CustomerRegisration(s,balance);
        }
        public int EmployeeRegistration(EmployeeEntity e)
        {
            return ob.EmployeeRegistration(e);
        }

        public int Closing(string clo)
        {
            return ob.closing(clo);

        }
        public List<CustomerEntity> CustomerByid(string hello)
        {
            return ob.CustomerByid(hello);
        }

        public List<LoanTransactionEntity> viewreport(string ac)
        {
            return ob.viewreport(ac);
        }



        public int DepositAmount(SavingsTransactionEntity s, string AccountID)
        {
            return ob.DepositAmount(s, AccountID);

        }
        public int WithdrawalAmount(SavingsTransactionEntity s, string AccountID)
        {
            return ob.WithdrawalAmount(s, AccountID);

        }


        public int Transaction(string Accountid)
        {
            return ob.Transaction(Accountid);
        }

        public int BeforViewLoan(string ac)
        {
            return ob.BeforViewLoan(ac);
        }
        public int CustomerInfo(string hello)
        {
            return ob.CustomerInfo(hello);
        }


        public List<SavingsTransactionEntity> TransactionsByid(string Accountid)
        {
            return ob.TransactionsByid(Accountid);
        }

        public int DeactivateCustomer(string cid)
        {
            return ob.DeActivateCustomer(cid);
        }

        public int loanacc(LoanAccountEntity l, string tenure, string Salary)
        {
            return ob.loanacc(l, tenure, Salary);
        }
        public int payemi(LoanTransactionEntity lt, String lacid)
        {
            return ob.payemi(lt,lacid);
        }
        public List<LoanAccountEntity> viewloan()
        {
            return ob.viewloan();
        }
    }
}
