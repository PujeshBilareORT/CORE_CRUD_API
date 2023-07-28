using CORE_CRUD_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CORE_CRUD_API.Connection;
using System.Data.SqlClient;
using System.Data;
using System;
using Dapper;

namespace CORE_CRUD_API.Controllers
{
	/*[Route("api/[controller]")]*/
	[ApiController]
	public class BookController : ControllerBase
	{
		[Route("api/[controller]/v1/AllBooks")]
		[HttpGet]
		public IActionResult AllBooks()
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> Books = new List<BookModel>();
			string sql = "[AllBookList]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
				}, commandType: CommandType.StoredProcedure);
				bvm.BookList = multi.Read<BookModel>().ToList();
				bvm.ResponseStatus = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			return Ok(bvm);
		}

		[Route("api/[controller]/v1/UpdateBook")]
		[HttpPost]
		public IActionResult UpdateBook(BookModel b)
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> Books = new List<BookModel>();
			string sql = "[UpdateMyBook]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					ID = b.ID,
				}, commandType: CommandType.StoredProcedure);
				bvm.ResponseStatus = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			return Ok(bvm);
		}

		[Route("api/[controller]/v1/InsertBook")]
		[HttpPost]
		public IActionResult InsertBook(BookModel b)
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> Books = new List<BookModel>();
			string sql = "[InsertMyBook]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					BookBanner = b.BookBanner,
					BookTitle = b.BookTitle,
					BookAuthor = b.BookAuthor,
				}, commandType: CommandType.StoredProcedure); ;
				bvm.ResponseStatus = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			return Ok(bvm);
		}

		[Route("api/[controller]/v1/BookDetailsByID")]
		[HttpPost]
		public IActionResult BookDetailsByID(BookModel um)
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> booklist = new List<BookModel>();
			ResponseStatusModel response = new ResponseStatusModel();
			string sql = "[BookDetailsByID]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					ID = um.ID,
				}, commandType: CommandType.StoredProcedure);
				booklist = multi.Read<BookModel>().ToList();
				response = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			bvm.BookList= booklist;
			bvm.ResponseStatus = response;
			if (bvm.ResponseStatus.N != 200 && bvm.ResponseStatus.Status != 200)
			{
				response.Status = 400;
				response.Msg = "No Book Details Found With Entered ID!";
				response.N = 400;
				bvm.ResponseStatus = response;
				return Ok(bvm);
			}
			return Ok(bvm);
		}

		[Route("api/[controller]/v1/BookDetailsByTitle")]
		[HttpPost]
		public IActionResult BookDetailsByTitle(BookModel um)
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> booklist = new List<BookModel>();
			ResponseStatusModel response = new ResponseStatusModel();
			string sql = "[BookDetailsByTitle]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					BookTitle = um.BookTitle,
				}, commandType: CommandType.StoredProcedure);
				booklist = multi.Read<BookModel>().ToList();
				response = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			bvm.BookList = booklist;
			bvm.ResponseStatus = response;
			if (bvm.ResponseStatus.N != 200 && bvm.ResponseStatus.Status != 200)
			{
				response.Status = 400;
				response.Msg = "No Book Details Found With Entered ID!";
				response.N = 400;
				bvm.ResponseStatus = response;
				return Ok(bvm);
			}
			return Ok(bvm);
		}


		[Route("api/[controller]/v1/BookDetailsByAuthor")]
		[HttpPost]
		public IActionResult BookDetailsByAuthor(BookModel um)
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> booklist = new List<BookModel>();
			ResponseStatusModel response = new ResponseStatusModel();
			string sql = "[BookDetailsByAuthor]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					BookAuthor = um.BookAuthor,
				}, commandType: CommandType.StoredProcedure);
				booklist = multi.Read<BookModel>().ToList();
				response = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			bvm.BookList = booklist;
			bvm.ResponseStatus = response;
			if (bvm.ResponseStatus.N != 200 && bvm.ResponseStatus.Status != 200)
			{
				response.Status = 400;
				response.Msg = "No Book Details Found With Entered ID!";
				response.N = 400;
				bvm.ResponseStatus = response;
				return Ok(bvm);
			}
			return Ok(bvm);
		}


		[Route("api/[controller]/v1/UpdateBookStatus")]
		[HttpPost]
		public IActionResult UpdateBookStatus(BookModel um)
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> booklist = new List<BookModel>();
			ResponseStatusModel response = new ResponseStatusModel();
			string sql = "[UpdateBookStatusByID]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					ID = um.ID,
				}, commandType: CommandType.StoredProcedure);
				booklist = multi.Read<BookModel>().ToList();
				response = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			bvm.BookList = booklist;
			bvm.ResponseStatus = response;
			if (bvm.ResponseStatus.N != 200 && bvm.ResponseStatus.Status != 200)
			{
				response.Status = 400;
				response.Msg = "No Book Details Found With Entered ID!";
				response.N = 400;
				bvm.ResponseStatus = response;
				return Ok(bvm);
			}
			return Ok(bvm);
		}

		[Route("api/[controller]/v1/RemoveBookByID")]
		[HttpPost]
		public IActionResult RemoveBookByID(BookModel um)
		{
			BookViewModel bvm = new BookViewModel();
			List<BookModel> booklist = new List<BookModel>();
			ResponseStatusModel response = new ResponseStatusModel();
			string sql = "[RemoveBookByID]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					ID = um.ID,
				}, commandType: CommandType.StoredProcedure);
				booklist = multi.Read<BookModel>().ToList();
				response = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			bvm.BookList = booklist;
			bvm.ResponseStatus = response;
			if (bvm.ResponseStatus.N != 200 && bvm.ResponseStatus.Status != 200)
			{
				response.Status = 400;
				response.Msg = "No Book Details Found With Entered ID!";
				response.N = 400;
				bvm.ResponseStatus = response;
				return Ok(bvm);
			}
			return Ok(bvm);

		}
	}
}

