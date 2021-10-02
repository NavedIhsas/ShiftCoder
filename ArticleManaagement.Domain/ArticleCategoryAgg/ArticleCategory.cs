using System.Collections.Generic;
using _0_FrameWork.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public ArticleCategory(string name, string keyword, string metaDescription, int showOrder,
            string canonicalAddress)
        {
            Name = name;
            Keyword = keyword;
            MetaDescription = metaDescription;
            ShowOrder = showOrder;
            CanonicalAddress = canonicalAddress;
        }

        public string Name { get; private set; }
        public string Keyword { get; private set; }
        public string MetaDescription { get; private set; }
        public int ShowOrder { get; private set; }
        public string CanonicalAddress { get; private set; }
        public List<Article> Articles { get; private set; }

        public void Edit(string name, string keyword, string metaDescription, int showOrder, string canonicalAddress)
        {
            Name = name;
            Keyword = keyword;
            MetaDescription = metaDescription;
            ShowOrder = showOrder;
            CanonicalAddress = canonicalAddress;
        }
    }
}