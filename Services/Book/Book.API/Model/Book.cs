namespace Book.API.Model
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public double Weight { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public virtual BookLanguage BookLanguage { get; set; }
        public int BookLanguageId { get; set; }
        public virtual BookAuthor BookAuthor { get; set; }
        public int BookAuthorId { get; set; }
        public virtual BookPublisher BookPublisher { get; set; }
        public int BookPublisherId { get; set; }
        public int TotalCopies { get; set; }
        public int BorrowedCopies { get; set; }
        public string Description { get; set; }
        public Book() { }
    }
}
