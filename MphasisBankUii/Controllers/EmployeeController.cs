using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLogic;
using BEntity;
using PagedList;
using PagedList.Mvc;


namespace MphasisBankUii.Controllers
{
    //LOGIN                  39
    //EmployeeHomePage       69
    //AccountOpen            85
    //SavingAccount          130
    //DepWi                  144
    //CustomerRegistration   159
    //DepositAmount          198
    //WithdrawalAmount       255
    //Transaction            312
    //TransactionById        352
    //
    //
    //
    //
    //
    //
    //

    public class EmployeeController : Controller
    {
        // GET: Employee
        EmpBLogic ob = new EmpBLogic();
        CustomerEntity ob1 = new CustomerEntity();
        SavingsTransactionEntity ob2 = new SavingsTransactionEntity();
        // GET: Employee
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string uname, string password)
        {
            int res = ob.ValidateEmployee(uname, password);
            int res1 = ob.ApplyLoanLogin(uname, password);
            int res2 = ob.Clogin(uname, password);
            int res3 = ob.ManagerLogin(uname, password);
            if (res > 0)
            {
                
                Session["user"] = uname;
                return RedirectToAction( "EmployeeHomePage");
                //return View("EmployeeHomePage", "SiteMaster");
            }
            else if (res1 > 0)
            {
                Session["user"] = uname;
                return View("Choose", "site2Master");
            }
            else if(res2>0)
            {
                Session["user"] = uname;
                return View("CustomerInfo", "CustomerMasterPage");
            }
            else if (res3 > 0)
            {
                Session["user"] = uname;
                return View("ManagerHomePage", "Site1Master");
            }
            else
            {
                ViewData["a"] = "Invalid User";
            }


            return View();

        }



        public ActionResult EmployeeHomePage()
        {
            if (Session["user"] != null)
            {
                return View("EmployeeHomePage","SiteMaster");
            }
            else
            {

                return RedirectToAction("MainPage" );
            }
        }




        public ActionResult AccountOpen()
        {
            if (Session["user"] != null)
            {


                //if (Session["user"].ToString() == "ayush")
                //{
                //    return View("someviewname", "masterpage");
                //}
                //if (Session["user"].ToString() == "manager"])
                //{
                //    return View("someviewname", "manager masterpage");
                //}
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }

        [HttpPost]
        public ActionResult AccountOpen(string pannum)
        {
            if (ModelState.IsValid)
            {
                int res1 = ob.ValidatePanCus(pannum);
                int res2 = ob.ValidatePanEmp(pannum);
                if (res1 > 0)
                {
                    ViewData["a"] = "Customer already exist";
                }
                else if (res2 > 0)
                {
                    ViewData["a"] = "Employee Cannot be Customer";
                }

                else
                {
                    return RedirectToAction("CustomerRegistration");
                }
                return View();


            }
            else
            {
                return View();
            }


        }

        public ActionResult SavingsAccount()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }


        public ActionResult DepWi()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }



        public ActionResult CustomerRegistration()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }
        [HttpPost]
        public ActionResult CustomerRegistration(CustomerEntity s, string balance)
        {

            if (ModelState.IsValid)
            {
                int res = ob.CustomerRegistration(s, balance);
                if (res > 0)
                {
                    ViewData["status"] = "Record Inserted";
                    TempData["b"] = TempData["a"];

                }
                else
                {
                    ViewData["status"] = "Enter Correct values check validations";
                }
                return View();
            }
            else
            {
                return View();
            }


        }
        public ActionResult EmployeeRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeRegistration(EmployeeEntity e)
        {
            if (ModelState.IsValid)
            {
                int res = ob.EmployeeRegistration(e);
                if (res > 0)
                {
                    ViewData["status"] = "Record Inserted";
                    

                }
                else
                {
                    ViewData["status"] = "Enter Correct values check validations";
                }
                return View();
            }
            else
            {
                return View();
            }

        }

        public ActionResult DepositAmount()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }
        [HttpPost]
        public ActionResult DepositAmount(SavingsTransactionEntity s, SavingsAccountEntities e)
        {
            if(ModelState.IsValid)
            {


                if (Convert.ToInt32(s.Amount) <= 1000)
                {
                    ViewData["status"] = "Amount cannot be less than 1000";
                    return View();
                }

                else
                {
                    int res = ob.DepositAmount(s, s.AccountId);
                    if (res > 0)
                    {
                       
                        ViewData["status"] = "DEPOSIT SUCCESSFULL";
                        return View();
                    }
                    else
                    {
                       
                        ViewData["status"] = "ENTER VALID ACCOUNTID";
                        return View();
                    }
                }
                return View();            }
            else
            {
                return View();
            }
        }










        public ActionResult WithdrawalAmount()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }
        [HttpPost]
        public ActionResult WithdrawalAmount(SavingsTransactionEntity s, SavingsAccountEntities e)
        {

            if (Convert.ToInt32(s.Amount) <= 1)
            {
                ViewData["status"] = "Amount Invalid";
                return View();
            }

            else
            {
                int res = ob.WithdrawalAmount(s, s.AccountId);

                if (res == 0)
                {
                    ViewData["status"] = "INVALID ACCOUNT NUMBER";
                    return View();
                }
                else if (res == -1)
                {
                    ViewData["status"] = "INSUFFICIENT AMOUNT ";

                    return View();
                }
                else
                {
                    ViewData["status"] = "WITHDRAWAL SUCCESSFULL";
                    return View();
                }
            }


            return View();
        }










        public ActionResult Transaction()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
           
        }


        [HttpPost]
        public ActionResult Transaction(string Accountid)
        {
            TempData["a"] = Accountid;
            TempData.Keep();
            int res = ob.Transaction(Accountid);
            if (res > 0)
            {
                return RedirectToAction("TransactionsByid");
            }
            else
            {
                ViewData["a"] = "Still no Transactions Done";
            }
            return View();
        }











        public ActionResult TransactionsByid(string Accountid ,int? page)
        {
            

            var pageNumber = page ?? 1;            
            Accountid = TempData["a"].ToString();
            TempData.Keep();
            var res = ob.TransactionsByid(Accountid);
            return View(res.ToList().ToPagedList(pageNumber, 5));
        }
        public ActionResult Viewreport(string ac, int? page)
        {
            var pageNumber = page ?? 1;
            ac = TempData["a"].ToString();
            TempData.Keep();
            var res = ob.viewreport(ac);
            return View(res.ToList().ToPagedList(pageNumber, 5));
        }











        public ActionResult Closing()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }
        [HttpPost]
        public ActionResult Closing(string clo)
        {
            TempData["a"] = clo;
            TempData.Keep();
            int res = ob.Closing(clo);
            if (res > 0)
            {
                return RedirectToAction("CustomerByid");
            }
            else
            {
                ViewData["a"] = "No Customer Found";
            }
            return View();

        }
        public ActionResult CustomerByid(string hello)
        {
            hello = TempData["a"].ToString();
            var res = ob.CustomerByid(hello);
            return View(res);
        }


        public ActionResult CloseAccount()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }
        }
        [HttpPost]
        public ActionResult CloseAccount(String cid)
        {
            string Custid = cid;
            int res = ob.DeactivateCustomer(cid);
            if (res > 0)
            {
                ViewData["a"] = "Deactivated  Sucessfully";
            }
            else
            {
                ViewData["a"] = "Please Check CustomerId";
            }
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("MainPage");
        }


       

        public ActionResult CustIdValidation()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("MainPage");
            }


        }
        [HttpPost]
        public ActionResult CustIdValidation(string custid)
        {
            int res = ob.CustIdValidation(custid); 
            if(res>0)
            {
                return RedirectToAction("LoanRegistration");
            }
            else
            {
              ViewData["a"] = "There Is No Customer Found With This CustomerId ";
            }
            return View();
        }

        

        public ActionResult Choose()
        {
            return View();
        }

        public ActionResult Loanaccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Loanaccount(LoanAccountEntity l, string tenure, string Salary)
        {
            if (ModelState.IsValid)
            {
                int res = ob.loanacc(l, tenure, Salary);
                if (res > 0)
                {
                    return RedirectToAction("viewloan");
                }
                else
                {
                    ViewData["status"] = "check validations enter correct values";
                }
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult viewloan()
        {
            var result = ob.viewloan();
            return View(result);
        }


        public ActionResult ManagerHomePage()
        {
           
            return View();
        }
        public ActionResult Payemi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payemi(LoanTransactionEntity lt, String lacid)
        {
            int res = ob.payemi(lt,lacid);
            if (ModelState.IsValid)
            {
                if (res > 0)
                {
                    ViewData["status"] = "Emi Paid Successfully";
                }
                else
                {
                    ViewData["status"] = "The Loan Id doesnot Exist ";
                }
                return View();
            }
            else
            {
                return View();
            }
        }




        
        public ActionResult BeforViewLoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BeforViewLoan(string ac)
        {
            TempData["a"] = ac;
            TempData.Keep();
            int res = ob.BeforViewLoan(ac);
            if (res > 0)
            {
                return RedirectToAction("Viewreport");
            }
            else
            {
                ViewData["a"] = "Still no Transactions Done";
            }
            return View();
        }




        public ActionResult CustomerInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerInfo(string hello)
        {
            TempData["a"] = hello;
            TempData.Keep();
            int res = ob.CustomerInfo(hello);
            if (res > 0)
            {
                return RedirectToAction("CustomerByid");
            }
            else
            {
                ViewData["a"] = "Customer Info found";
            }
            return View();
        }

    }

}



  