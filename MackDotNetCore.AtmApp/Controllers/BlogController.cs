using MackDotNetCore.AtmApp.EFDbContext;
using MackDotNetCore.AtmApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MackDotNetCore.AtmApp.Controllers
{
	public class BlogController : Controller
	{
		private readonly AppDbContext _context;

		public BlogController(AppDbContext context)
		{
			_context = context;
		}
		//get
		[ActionName("Index")]
		public IActionResult BlogIndex()
		{
			List<BlogDataModel> lst = _context.Blogs.ToList();
			return View("BlogIndex", lst);
		}
		[ActionName("Create")]
		public IActionResult BlogCreate()
		{
			return View("BlogCreate");
		}
		[HttpPost]
		[ActionName("Save")]
		public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
		{
			await _context.Blogs.AddAsync(reqModel);
			var result = await _context.SaveChangesAsync();
			var message = result > 0 ? "Registration Successful." : "Registration failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;

			return Redirect("/blog");
		}
		[ActionName("Withdraw")]
		public IActionResult BlogWithdraw()
		{
			return View("BlogWithdraw");
		}
        private IActionResult WithdrawalFailed(string errorMessage)
        {
            TempData["Message"] = errorMessage;
            TempData["IsSuccess"] = false;
            return Redirect("/home");
        }

        [HttpPost]
		[ActionName("Withdraw")]
		public async Task<IActionResult> BlogWithdraw(BlogDataModel reqModel)
		{
			var blog = await _context.Blogs
				.FirstOrDefaultAsync(b => b.CardNum == reqModel.CardNum && b.Pin == reqModel.Pin);

			if (blog == null)
			{
				return WithdrawalFailed("Invalid cardNum or pin.");
			}

			if (reqModel.Balance <= 0)
			{
				return WithdrawalFailed("Invalid withdrawal amount.");
			}

			if (reqModel.Balance > blog.Balance)
			{
				return WithdrawalFailed("Insufficient balance.");
			}

			blog.Balance -= reqModel.Balance;
			_context.Blogs.Update(blog);

			var result = await _context.SaveChangesAsync();

			var message = result > 0 ? "Withdrawal Successful." : "Withdrawal failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;
			return Redirect("/home");
		}
        [ActionName("Deposit")]
        public IActionResult BlogDeposit()
        {
            return View("BlogDeposit");
        }
        [HttpPost]
        [ActionName("Deposit")]
        public async Task<IActionResult> BlogDeposit(BlogDataModel reqModel)
        {
            var blog = await _context.Blogs
                .FirstOrDefaultAsync(b => b.CardNum == reqModel.CardNum && b.Pin == reqModel.Pin);

            if (blog == null)
            {
                return WithdrawalFailed("Invalid cardNum or pin.");
            }


            blog.Balance += reqModel.Balance;
            _context.Blogs.Update(blog);

            var result = await _context.SaveChangesAsync();

            var message = result > 0 ? "Deposit Successful." : "Deposit failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/home");
        }
        [ActionName("Check")]
        public IActionResult BlogCheck()
        {
            return View("BlogCheck");
        }
		[HttpGet]
		[ActionName("Check")]
		public async Task<IActionResult> BlogCheck(BlogDataModel reqModel)
		{
			var blog = await _context.Blogs
				.FirstOrDefaultAsync(b => b.CardNum == reqModel.CardNum && b.Pin == reqModel.Pin);

            if (blog != null)
            {
                return View("BlogCheckResult", blog);
            }
            else
            {
				return View("BlogCheck");
            }

           
           
		}
	}
}
