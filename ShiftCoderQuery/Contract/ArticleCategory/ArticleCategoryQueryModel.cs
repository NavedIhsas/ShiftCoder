namespace ShiftCoderQuery.Contract.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ArticlesCount { get; internal set; }
    }
}