using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BEntity
{
    class EmpBEntity
    {
    }
    public class CustomerEntity
    {

        public string CustId { get; set; }
        [Required(ErrorMessage = "Password cannot be left blank")]
        public string Pwd { get; set; }
        public string CustName { get; set; }

        public DateTime Dob { get; set; }
        [RegularExpression("[A-Z][A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9]", ErrorMessage = "Pan Number Should contain four letters and four digits only and only capital letters")]
        public string Pan { get; set; }
        public Nullable<bool> status { get; set; }
    }

    public class EmployeeEntity
    {

        [Required(ErrorMessage = "Username Cannot Be Null")]
        public int EmpId { get; set; }
        public string Pwd { get; set; }
        public string EmpName { get; set; }
        public int DepName { get; set; }
        [RegularExpression("[A-Z][A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9]", ErrorMessage = "Pan Number Should contain four letters and four digits only and only capital letters")]
        public string Pan { get; set; }
    }

    public class SavingsAccountEntities
    {


        public string AccountId { get; set; }
        public string CustId { get; set; }
        public int Balance { get; set; }

    }

    public class SavingsTransactionEntity
    {
        public int TransactionId { get; set; }
        [Required(ErrorMessage = "AccountId cannot be left blank")]
        public string AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
    }




    public class LoanAccountEntity
    {
        public string LoanAcid { get; set; }
        public string Custid { get; set; }
        public Nullable<int> LoanAmount { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> Tenure { get; set; }
        public Nullable<double> LnRoi { get; set; }
        public Nullable<double> Emi { get; set; }
    }



    public class LoanTransactionEntity
    {
        public int transactionNo { get; set; }
        public string loanAcid { get; set; }
        public Nullable<System.DateTime> EmiDate { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<int> Outstanding { get; set; }

        





    }


}
