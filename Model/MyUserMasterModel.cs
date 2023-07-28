namespace CORE_CRUD_API.Model
{
    public class MyUserMasterModel
    {
        public int UID { get; set; }
        public string? Email { get; set; } 
        public string? Password { get; set; }
        public string? Role { get; set; } 
        public string? EncryptedPassword { get; set; }
        public string? MyKey { get; set; } 
        public string? Username { get; set; } 
        public string? AddedBy { get; set; }
        public string? AddedOn { get; set; }
        public string? Token { get; set; } 
        public string? UpdatedBy { get; set; } 
        public string? UpdatedOn { get; set; } 
        public string? TokenExpire { get; set; }
        public string? MailExpiry { get; set; }
        public string? TokenTime { get; set; }
        public string? EncryptedKey { get; set; }
        public int Active { get; set; }
        public int Status { get; set; }
    }

    public class MyUserMasterViewModel
    {
        public List<MyUserMasterModel>? MyUserMasterList { get; set; }
        public ResponseStatusModel? ResponseStatus { get; set; }
        public MyUserMasterModel? MyUserMaster { get; set; }

    }

    public class LoginResponseModel
    {
        public ResponseStatusModel ResponseStatus { get; set; } = new ResponseStatusModel();
        public MyUserMasterModel MyUserMaster { get; set; } = new MyUserMasterModel();
    }
}
