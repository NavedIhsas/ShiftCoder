namespace BlogManagement.Application.Contract.ArticleCategory
{
    public class ArticleCategoryViewModel
    {
        public string Name { get; set; }
        public int ArticleCount { get; set; }
        public int ShowOrder { get; set; }
        public string CreationDate { get; set; }

        public long Id { get; set; }
    }
}