namespace MakeYourJournal.DAL.Dtos
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ArticleTitle { get; set; }
        public int ArticleId { get; set; }
    }
}