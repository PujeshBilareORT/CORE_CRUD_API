using CORE_CRUD_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Reflection;

namespace CORE_CRUD_API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        EncryptDecryptService.EncryptDecryptService service1 = new EncryptDecryptService.EncryptDecryptService();

        [Route("api/[controller]/v1/UserLogin")]
        [HttpPost]
        public IActionResult UserLogin(MyUserMasterModel um) 
        {
            MyUserMasterViewModel uvm=new MyUserMasterViewModel();
            List<MyUserMasterModel> userlist= new List<MyUserMasterModel>();
            ResponseStatusModel response=new ResponseStatusModel();
            string sql = "[Checkmail]";
            using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
            {
                var multi = conn.QueryMultiple(sql, new
                {
                    Email = um.Email,
                }, commandType: CommandType.StoredProcedure);
                userlist = multi.Read<MyUserMasterModel>().ToList();
            }
            if(userlist.Count <= 0) 
            {
                response.Status = 400;
                response.Msg = "Invalid Email";
                response.N = 400;
                uvm.ResponseStatus = response;
                return Ok(uvm);
            }
            else 
            {
                EncryptDecryptService.EncryptDecryptService service = new EncryptDecryptService.EncryptDecryptService();
                um.MyKey = service.DecryptKey(userlist.FirstOrDefault()?.EncryptedKey);
                um.EncryptedPassword = service.Encrypt(um);
                ResponseStatusModel  response1 = new ResponseStatusModel();
                string sql1 = "[VerifyUserLogin]";
                using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
                {
                    var multi = conn.QueryMultiple(sql1, new
                    {
                        Email = um.Email,
                        EncryptedPassword = um.EncryptedPassword,
                    }, commandType: CommandType.StoredProcedure);
                    response1 = multi.Read<ResponseStatusModel>().SingleOrDefault();
                }
                if(response1.N == -1 && response1.Status == 0) 
                {
                    response.Status = 400;
                    response.Msg = "Invalid Password";
                    response.N = 400;
                    uvm.ResponseStatus = response;
                    return Ok(uvm);
                }

                if (response1.N == 100 && response1.Status == 100)
                {
                    response.Status = 200;
                    response.Msg = "Login Successfull";
                    response.N = 200;
                    uvm.ResponseStatus = response;
                    return Ok(uvm);
                }
            }
            return Ok(uvm);
        }

        [Route("api/[controller]/v1/UserDetailsByID")]
        [HttpPost]
        public IActionResult UserDetailsByID(MyUserMasterModel um)
        {
            MyUserMasterViewModel uvm = new MyUserMasterViewModel();
            List<MyUserMasterModel> userlist = new List<MyUserMasterModel>();
            ResponseStatusModel response = new ResponseStatusModel();
            string sql = "[UserDetailsByID]";
            using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
            {
                var multi = conn.QueryMultiple(sql, new
                {
                    UID = um.UID,
                }, commandType: CommandType.StoredProcedure);
                userlist = multi.Read<MyUserMasterModel>().ToList();
                response= multi.Read<ResponseStatusModel>().SingleOrDefault();
            }
            uvm.MyUserMasterList = userlist;
            uvm.ResponseStatus= response;
            if (uvm.MyUserMasterList.Count <= 0)
            {
                response.Status = 400;
                response.Msg = "No User Found With Entered ID!";
                response.N = 400;
                uvm.ResponseStatus = response;
                return Ok(uvm);
            }
            return Ok(uvm);
        }

        [Route("api/[controller]/v1/UserDetailsByEmail")]
        [HttpPost]
        public IActionResult UserDetailsByEmail(MyUserMasterModel um)
        {
            MyUserMasterViewModel uvm = new MyUserMasterViewModel();
            List<MyUserMasterModel> userlist = new List<MyUserMasterModel>();
            ResponseStatusModel response = new ResponseStatusModel();
            string sql = "[UserDetailsByEmail]";
            using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
            {
                var multi = conn.QueryMultiple(sql, new
                {
                    Email = um.Email,
                }, commandType: CommandType.StoredProcedure);
                userlist= multi.Read<MyUserMasterModel>().ToList();
                response= multi.Read<ResponseStatusModel>().SingleOrDefault();
            }
            uvm.MyUserMasterList = userlist;
            uvm.ResponseStatus= response;
            if (uvm.MyUserMasterList.Count <= 0)
            {
                response.Status = 400;
                response.Msg = "No User Found With Entered Email!";
                response.N = 400;
                uvm.ResponseStatus = response;
                return Ok(uvm);
            }
            return Ok(uvm);
        }


        [Route("api/[controller]/v1/AllUserData")]
        [HttpGet]
        public IActionResult AllUserData()
        {
            MyUserMasterViewModel uvm = new MyUserMasterViewModel();
            ResponseStatusModel response = new ResponseStatusModel();
            string sql = "[AllUserData]";
            using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
            {
                var multi = conn.QueryMultiple(sql, new
                {
                    
                }, commandType: CommandType.StoredProcedure);
                uvm.MyUserMasterList = multi.Read<MyUserMasterModel>().ToList();
                uvm.ResponseStatus= multi.Read<ResponseStatusModel>().SingleOrDefault();
            }
            if (uvm.MyUserMasterList.Count <= 0)
            {
                response.Status = 400;
                response.Msg = "No User Found / Table Empty !";
                response.N = 400;
                uvm.ResponseStatus = response;
                return Ok(uvm);
            }
            return Ok(uvm);
        }


        [Route("api/[controller]/v1/InsertUser")]
        [HttpPost]
        public IActionResult InsertUser(MyUserMasterModel um)
        {
            MyUserMasterViewModel uvm = new MyUserMasterViewModel();
            ResponseStatusModel response = new ResponseStatusModel();
            um.Email = um.Username.Replace(" ", ".") + "@onerooftech.com";
            um.MyKey = service1.GenerateRandomKey();
            string specialpass = service1.GeneratePassword();
            um.Password = um.Username.Replace(" ", "") + specialpass;
            um.EncryptedPassword = service1.Encrypt(um);
            um.EncryptedKey = service1.EncryptKey(um);
            string sql = "[SPNewUser]";
            using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
            {
                var multi = conn.QueryMultiple(sql, new
                {
                    Username = um.Username,
                    Password = um.Password,
                    Email = um.Email,
                    EncryptedPassword = um.EncryptedPassword,
                    MyKey = um.MyKey,
                    EncryptedKey = um.EncryptedKey,
                }, commandType: CommandType.StoredProcedure);
                uvm.ResponseStatus = multi.Read<ResponseStatusModel>().SingleOrDefault();
            }
            if (uvm.ResponseStatus.N!=1 && uvm.ResponseStatus.Status!=100) 
            {
                response.N = 500;
                response.Status = 500;
                response.Msg = "User Not Added Something went Wrong";
                uvm.ResponseStatus = response;
                return Ok(uvm);
            }
            return Ok(uvm);
        }

		[Route("api/[controller]/v1/SetUserStatus")]
		[HttpPost]
		public IActionResult SetUserStatus(MyUserMasterModel um)
		{
			MyUserMasterViewModel uvm = new MyUserMasterViewModel();
			List<MyUserMasterModel> userlist = new List<MyUserMasterModel>();
			ResponseStatusModel response = new ResponseStatusModel();
			string sql = "[SetUserStatus]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					UID = um.UID,
				}, commandType: CommandType.StoredProcedure);
				userlist = multi.Read<MyUserMasterModel>().ToList();
				response = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			uvm.MyUserMasterList = userlist;
			uvm.ResponseStatus = response;
			if (uvm.ResponseStatus.N != 200 && uvm.ResponseStatus.Status != 200)
			{
				return Ok(uvm);
			}
			return Ok(uvm);
		}

		[Route("api/[controller]/v1/RemoveUser")]
		[HttpPost]
		public IActionResult RemoveUser(MyUserMasterModel um)
		{
			MyUserMasterViewModel uvm = new MyUserMasterViewModel();
			List<MyUserMasterModel> userlist = new List<MyUserMasterModel>();
			ResponseStatusModel response = new ResponseStatusModel();
			string sql = "[     ]";
			using (IDbConnection conn = new SqlConnection(Connection.Connection.GetConnection().ConnectionString))
			{
				var multi = conn.QueryMultiple(sql, new
				{
					UID = um.UID,
				}, commandType: CommandType.StoredProcedure);
				userlist = multi.Read<MyUserMasterModel>().ToList();
				response = multi.Read<ResponseStatusModel>().SingleOrDefault();
			}
			uvm.MyUserMasterList = userlist;
			uvm.ResponseStatus = response;
			if (uvm.ResponseStatus.N != 200 && uvm.ResponseStatus.Status != 200)
			{
				return Ok(uvm);
			}
			return Ok(uvm);
		}

	}
}