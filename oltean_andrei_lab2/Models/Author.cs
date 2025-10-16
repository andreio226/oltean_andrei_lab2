namespace oltean_andrei_lab2.Models
{
    public class Author
    {
        
        
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }
        
        public ICollection<Book>? Books { get; set; } //navigation property
    }
}