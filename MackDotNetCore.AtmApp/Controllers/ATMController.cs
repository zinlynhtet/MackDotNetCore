using MackDotNetCore.AtmApp.EFDbContext;
using MackDotNetCore.AtmApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MackDotNetCore.AtmApp.Controllers
{
	public class ATMController : Controller
	{
		private readonly AppDbContext _context;

		public ATMController(AppDbContext context)
		{
			_context = context;
		}
		//get
		[ActionName("Index")]
		public IActionResult ATMIndex()
		{
			List<BlogDataModel> lst = _context.Blogs.ToList();
			return View("ATMIndex", lst);
		}
		[ActionName("Create")]
		public IActionResult ATMCreate()
		{
			return View("ATMCreate");
		}

		[HttpPost]
		[ActionName("Save")]
		public async Task<IActionResult> ATMSave(BlogDataModel reqModel)
		{
			await _context.Blogs.AddAsync(reqModel);
			var result = await _context.SaveChangesAsync();
			var message = result > 0 ? "Registration Successful." : "Registration failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;

			return Redirect("/atm");
		}

		[ActionName("Withdraw")]
		public IActionResult ATMWithdraw()
		{
			return View("ATMWithdraw");
		}

		[HttpPost]
		[ActionName("Withdraw")]
		public async Task<IActionResult> ATMWithdraw(BlogDataModel reqModel)
		{
			var blog = await _context.Blogs
				.FirstOrDefaultAsync(b => b.CardNum == reqModel.CardNum && b.Pin == reqModel.Pin);

			if (blog == null || reqModel.Balance <= 0 || reqModel.Balance > blog.Balance)
			{
				TempData["Message"] = "Withdrawal failed. Invalid data or insufficient balance.";
				TempData["IsSuccess"] = false;

				return View("ATMWithdraw");
			}

			blog.Balance -= reqModel.Balance;
			_context.Blogs.Update(blog);

			var result = await _context.SaveChangesAsync();

			var message = result > 0 ? $"Withdraw Successful. Remaining Balance: {blog.Balance} $" : "Withdraw failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;
			return Redirect("/home");
		}
		[ActionName("Deposit")]
		public IActionResult ATMDeposit()
		{
			return View("ATMDeposit");
		}
		[HttpPost]
		[ActionName("Deposit")]
		public async Task<IActionResult> ATMDeposit(BlogDataModel reqModel)
		{
			var blog = await _context.Blogs
				.FirstOrDefaultAsync(b => b.CardNum == reqModel.CardNum && b.Pin == reqModel.Pin);


			if (blog == null)
			{
				TempData["Message"] = "Invalid cardNum or pin.";
				TempData["IsSuccess"] = false;
				return View("ATMDeposit");
			}

			blog.Balance += reqModel.Balance;
			_context.Blogs.Update(blog);

			var result = await _context.SaveChangesAsync();

			var message = result > 0 ? $"Deposit Successful. New Balance: {blog.Balance} $" : "Deposit failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;
			return Redirect("/home");
		}
		[ActionName("Check")]
		public IActionResult ATMCheck()
		{
			return View("ATMCheck");
		}
		[HttpGet]
		[ActionName("Check")]
		public async Task<IActionResult> ATMCheck(BlogDataModel reqModel)
		{
			var blog = await _context.Blogs
				.FirstOrDefaultAsync(b => b.CardNum == reqModel.CardNum && b.Pin == reqModel.Pin);
			if (blog != null)
			{
				return View("ATMCheckResult", blog);
			}
			return View("ATMCheck");
		}
	}
}
