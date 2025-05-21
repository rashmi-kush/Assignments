using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Security.Principal;

namespace Assignment10.Controller
{
    public class BankAccountController : ControllerBase
    {
        [Route("/")]
        public string Start()
        {
            Response.StatusCode = 200;
            return "Welcome to the Best Bank.";
        }
        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            var Accnt_Details = new
            {
                accountNumber = 1001,
                accountHolderName = "Rashu account",
                currentBalance = 5000
            };
            Response.StatusCode = 200;
            return new JsonResult(Accnt_Details);
        }
        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            Response.StatusCode = 200;
            return File("statement.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber:int?}")]
        public IActionResult GetData()
        {
            object accountNumObj;
            if (HttpContext.Request.RouteValues.TryGetValue("accountNumber", out accountNumObj) && accountNumObj is string accountNumber)
            {

                if (string.IsNullOrEmpty(accountNumber))
                {
                    return NotFound("Account Number should be supplied.");
                }

                if (int.TryParse(accountNumber, out int accountnum))
                {
                    var Accnt_Details = new
                    {
                        accountNumber = 1001,
                        accountHolderName = "Example Name",
                        currentBalance = 5000
                    };
                    if (accountnum == 1001)
                    {
                        return Content(Accnt_Details.currentBalance.ToString());
                    }
                    else
                    {
                        return BadRequest("Account Number should be 1001.");
                    }
                }
                else
                {
                    return BadRequest("Invalid Account number");
                }

            }
            else
            {
                return NotFound("Account not supplied.");
            }
        }
    }
}
