namespace CORE_CRUD_API.Model
{
    public class BookModel
    {
        public int ID { get; set; }
        public string? BookTitle { get; set; }
        public string? BookAuthor { get; set; } 
        public string? BookBanner { get; set; }
        public int Active { get; set; }
        public int Status { get; set; }
        public string? AddedBy { get; set; } 
        public string? AddedOn { get; set; } 
        public string? UpdatedBy { get; set; } 
        public string? UpdatedOn { get; set; } 
    }
    public class BookViewModel
    {
        public List<BookModel> BookList { get; set; }=new List<BookModel>();
        public BookModel Book { get; set; }=new BookModel();
        public ResponseStatusModel ResponseStatus { get; set; } = new ResponseStatusModel();
    }
}
