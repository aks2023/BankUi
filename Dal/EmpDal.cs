using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEntity;



namespace Dal
{
    public class EmpDal
    {

        //all database related task
        MphasisBankEntities1 ob = new MphasisBankEntities1();
        public int ValidateEmployee(string username, string password)
        {

            var r = (from c in ob.Employees
                     where c.EmpName == username & c.Pwd == password &c.DepName==0
                     select c).Count();
            if (r > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }




        public int ManagerLogin(string username, string password)
        {

            var r = (from c in ob.Employees
                     where c.EmpName == username & c.Pwd == password & c.DepName == 2
                     select c).Count();
            if (r > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }





        public int ApplyLoanLogin(string username, string password)
        {

            var r = (from c in ob.Employees
                     where c.EmpName == username & c.Pwd == password & c.DepName==1
                     select c).Count();
            if (r > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Clogin(string username, string password)
        {
            var r = (from c in ob.Customers
                     where c.CustName == username & c.Pwd == password & c.status == true
                     select c).Count();
            if(r>0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }




        public int VlidatePanCus(string p)
        {
            var r = (from c in ob.Customers
                     where c.Pan == p
                     select c).Count();


            if (r > 0)
            {
                return 1;
            }

            else
            {
                return 0;
            }
        }
        public int VlidatePanEmp(string p)
        {
            var r = (from c in ob.Employees
                     where c.Pan == p
                     select c).Count();


            if (r > 0)
            {
                return 1;
            }

            else
            {
                return 0;
            }
        }

        public int CustIdValidation(string custid)
        {
            var r = (from c in ob.SavingsAccounts
                     where c.CustId == custid
                     select c).Count();


            if (r > 0)
            {
                return 1;
            }

            else
            {
                return 0;
            }
        }




        public int CustomerRegisration(CustomerEntity ce, string balance)
        {
            try
            {
                var custid = "a";
                var cd = ob.Customers.OrderByDescending(c => c.CustId).FirstOrDefault();
                if (cd == null)
                {
                    custid = "MLA11111";
                }
                else
                {
                    custid = "MLA" + (Convert.ToInt32(cd.CustId.Substring(3, 5)) + 1).ToString();
                }
                Customer st = new Customer() { CustId = custid, Pwd = ce.Pwd, CustName = ce.CustName, Dob = ce.Dob, Pan = ce.Pan };
                ob.Customers.Add(st);



                var acid = "a";
                var accd = ob.SavingsAccounts.OrderByDescending(c => c.AccountId).FirstOrDefault();
                if (accd == null)
                {
                    acid = "SB11111";
                }
                else
                {
                    acid = "SB" + (Convert.ToInt32(accd.AccountId.Substring(2, 5)) + 1).ToString();
                }
                SavingsAccount sa = new SavingsAccount() { AccountId = acid, CustId = custid, Balance = int.Parse(balance) };
                ob.SavingsAccounts.Add(sa);
                return ob.SaveChanges();

                
            }
            catch(Exception e)
            {
                return 0;
            }
        }




        public int EmployeeRegistration(EmployeeEntity e)
        {
            try
            {
                var custid = "a";
                var cd = ob.Employees.OrderByDescending(c => c.EmpId).FirstOrDefault();
                if (cd == null)
                {
                    custid = "10";
                }
                else
                {
                    custid = (Convert.ToInt32(cd.EmpId) + 1).ToString();
                }
                Employee st = new Employee() { EmpId = e.EmpId, Pwd = e.Pwd, EmpName = e.EmpName, DepName = (byte?)e.DepName, Pan = e.Pan };
                ob.Employees.Add(st);              
                return ob.SaveChanges();


            }
            catch (Exception a)
            {
                return 0;
            }
        }
















        public int DepositAmount(SavingsTransactionEntity s, String AccountID)
        {

            try
            {
                var res = from c in ob.SavingsAccounts
                          where c.AccountId == AccountID
                          select c;
                if (res == null)
                {
                    return 0;
                }
                else
                {
                    string transid;
                    var lasttransID = ob.SavingsTransactions.OrderByDescending(t => t.TransactionId).FirstOrDefault();
                    if (lasttransID == null)
                    {
                        transid = "00001";
                    }
                    else
                    {
                        transid = (Convert.ToInt32(lasttransID.TransactionId) + 1).ToString();
                    }
                    SavingsTransaction st = new SavingsTransaction() { TransactionId = Int32.Parse(transid), AccountId = AccountID, TransactionDate = DateTime.Now, TransactionType = "SB", Amount = s.Amount };
                    (from p in ob.SavingsAccounts
                     where p.AccountId == AccountID
                     select p).ToList().ForEach(x => x.Balance = x.Balance + s.Amount);
                    ob.SavingsTransactions.Add(st);
                    return ob.SaveChanges();
                }
            }
            catch(Exception e)
            {
                return 0;
            }

        }

        public int WithdrawalAmount(SavingsTransactionEntity s, String AccountID)
        {
            var res = from c in ob.SavingsAccounts
                      where c.AccountId == AccountID
                      select c;
            if (res == null)
            {
                return 0;
            }
            else
            {
                string transid;
                var lasttransID = ob.SavingsTransactions.OrderByDescending(t => t.TransactionId).FirstOrDefault();
                if (lasttransID == null)
                {
                    transid = "00001";
                }
                else
                {
                    transid = (Convert.ToInt32(lasttransID.TransactionId) + 1).ToString();
                }
                var amount = ob.SavingsAccounts.OrderByDescending(t => t.Balance).Where(f => f.AccountId == AccountID).FirstOrDefault();
                if (s.Amount <= (Convert.ToInt32(amount.Balance)))
                {
                    SavingsTransaction st = new SavingsTransaction() { TransactionId = Int32.Parse(transid), AccountId = AccountID, TransactionDate = DateTime.Now, TransactionType = "W", Amount = s.Amount };
                    (from p in ob.SavingsAccounts
                     where p.AccountId == AccountID
                     select p).ToList().ForEach(x => x.Balance = x.Balance - s.Amount);
                    ob.SavingsTransactions.Add(st);
                    return ob.SaveChanges();
                }
                else
                {
                    return -1;
                }
            }
        }





        public int Transaction(string Accountid)
        {
            var res = (from t in ob.SavingsAccounts
                       where t.AccountId == Accountid
                       select t).Count();
            if (res > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public List<SavingsTransactionEntity> TransactionsByid(string Accountid)
        {
            var res = from t in ob.SavingsTransactions
                      where t.AccountId == Accountid
                      select t;
            List<SavingsTransactionEntity> k = new List<SavingsTransactionEntity>();

            foreach (var item in res)
            {

                k.Add(new SavingsTransactionEntity() { AccountId = item.AccountId, TransactionId = item.TransactionId, TransactionDate = (DateTime)item.TransactionDate, TransactionType = item.TransactionType, Amount = (int)item.Amount });
            }
            return k;
        }

        public List<LoanTransactionEntity> viewreport(string ac)
        {

            var lastloan = from t in ob.LoanTransactions
                           where t.loanAcid == ac
                           select t;
            List<LoanTransactionEntity> li = new List<LoanTransactionEntity>();
            foreach (var item in lastloan)
            {


                li.Add(new LoanTransactionEntity() { Amount = item.Amount, Outstanding = item.Outstanding, transactionNo = item.transactionNo, EmiDate = item.EmiDate, loanAcid = item.loanAcid });
            }
            return li;
        }




        public int closing(string clo)
        {

            var res = (from t in ob.Customers
                       where t.CustId == clo
                       select t).Count();
            if (res > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }




        public List<CustomerEntity> CustomerByid(string hello)
        {
            var res = from t in ob.Customers
                      where t.CustId == hello
                      select t;
            List<CustomerEntity> k = new List<CustomerEntity>();

            foreach (var item in res)
            {

                k.Add(new CustomerEntity() { CustId = item.CustId, Pwd = item.Pwd, CustName = item.CustName, Dob = (DateTime)item.Dob, Pan = item.Pan });
            }
            return k;
        }



        public int DeActivateCustomer(string custid)
        {

            var res = ob.Customers.Where(t => t.CustId == custid);
            if (res.Count() > 0)
            {
                foreach (var item in res)
                {
                    item.status = false;
                }
                ob.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }





        public int loanacc(LoanAccountEntity l, string tenure, string Salary)
        {
            try
            {
                string loanid;
                double rateofint = 0;
                int n;
                double emical;
                var res = (from t in ob.SavingsAccounts
                           where t.CustId == l.Custid
                           select t).Count();//-------------------------checking if customer has a savings accounts or not rex
                var res1 = (from t in ob.LoanAccounts
                            where t.Custid == l.Custid
                            select t).Count();//-----------------------he shouldnt apply for loan 2nd time
                
                if (res > 0 & res1 < 1)
                {
                    var lastloan = ob.LoanAccounts.OrderByDescending(c => c.LoanAcid).FirstOrDefault();
                    if (lastloan == null)
                    {
                        loanid = "LN10000";
                    }
                    else
                    {
                        loanid = "LN" + (Convert.ToInt32(lastloan.LoanAcid.Substring(2, 5)) + 1).ToString();
                    }

                    if (l.LoanAmount < 500000)
                    {
                        rateofint = double.Parse("0.00833");
                    }

                    if (l.LoanAmount > 500000 & l.LoanAmount < 1000000)
                    {
                        rateofint = double.Parse("0.00791");
                    }

                    if (l.LoanAmount > 1000000)
                    {
                        rateofint = double.Parse("0.0075");

                    }
                    n = Convert.ToInt32(tenure) * 12;                 
                    emical =(double)( l.LoanAmount * rateofint * Math.Pow(1 + rateofint, n) / (Math.Pow(1 + rateofint, n) - 1));
                    double Salaryem = Convert.ToDouble(Salary) * 0.6;                 
                        LoanAccount k = new LoanAccount() { Custid = l.Custid, Tenure = Convert.ToInt32(tenure), LoanAcid = loanid, LoanAmount = l.LoanAmount, LnRoi = rateofint, Emi = emical, StartDate = DateTime.Now };
                        ob.LoanAccounts.Add(k);
                        return ob.SaveChanges();             
                      }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<LoanAccountEntity> viewloan()
        {
            var lastloan = ob.LoanAccounts.OrderByDescending(c => c.LoanAcid).FirstOrDefault();
            List<LoanAccountEntity> li = new List<LoanAccountEntity>();


            li.Add(new LoanAccountEntity() { Emi = lastloan.Emi, LnRoi = lastloan.LnRoi, LoanAcid = lastloan.LoanAcid });
            return li;
        }
        //public int Payemi(LoanTransactionEntity e)
        //{
        //    var res = from t in ob.LoanAccounts
        //              where t.LoanAcid == e.loanAcid
        //              select t;

        //    int loantransid;
        //    var lasttrans = ob.LoanTransactions.OrderByDescending(c => c.transactionNo).FirstOrDefault();
        //    if (lasttrans == null)
        //    {
        //        loantransid = 10000;
        //    }
        //    else
        //    {
        //        loantransid = 1 + Convert.ToInt32(lasttrans.transactionNo);
        //    }
        //    if (res.Count() > 0)
        //    {


        //        (from t in ob.LoanAccounts
        //         where t.LoanAcid == e.loanAcid
        //         select t).ToList().ForEach(x => x.Emi = x.Emi - e.Amount);
        //        int b;
        //        var j = (from t in ob.LoanAccounts
        //                 where t.LoanAcid == e.loanAcid
        //                 select t).Single();


        //        b = (int)(Convert.ToInt32(j) - e.Amount);


        //        LoanTransaction k = new LoanTransaction() { transactionNo = loantransid, Amount = e.Amount, loanAcid = e.loanAcid, EmiDate = DateTime.Now, Outstanding = b - e.Amount };
        //        ob.LoanTransactions.Add(k);
        //        return ob.SaveChanges();
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}




        public int payemi(LoanTransactionEntity lt, String lacid)
        {
            var res = (from c in ob.LoanAccounts
                       where c.LoanAcid == lacid
                       select c);
            var am = (from c in ob.LoanAccounts
                      where c.LoanAcid == lacid
                      select c.LoanAmount).SingleOrDefault();

            int b = Convert.ToInt32(am);
            if (res.Count() > 0)
            {
                int os;
                string transno;
                var tn = ob.LoanTransactions.OrderByDescending(t => t.transactionNo).FirstOrDefault();
                var last = ob.LoanTransactions.Where(t => t.loanAcid== lacid).OrderBy(t => t.Outstanding).FirstOrDefault();


                if (tn == null)
                {
                    transno = "10001";
                }
                else
                {
                    transno = (Convert.ToInt32(tn.transactionNo) + 1).ToString();
                }

                if (last == null)
                {
                    os = (int)(am - lt.Amount);
                }
                else
                {
                    os = ((int)(Convert.ToInt32(last.Outstanding) - lt.Amount));
                }

               
                LoanTransaction lnt = new LoanTransaction { transactionNo = Convert.ToInt32(transno), loanAcid = lacid, EmiDate = DateTime.Now, Amount = lt.Amount, Outstanding = os };


                ob.LoanTransactions.Add(lnt);
                ob.SaveChanges();

            }
            else
            {
                return 0;
            }
            return 1;

        }







        public int BeforViewLoan(string ac)
        {
            var res = (from t in ob.LoanAccounts
                       where t.LoanAcid == ac
                       select t).Count();
            if (res > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int CustomerInfo(string hello)
        {
            var res = (from t in ob.Customers
                       where t.CustId == hello
                       select t).Count();
            if (res > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
