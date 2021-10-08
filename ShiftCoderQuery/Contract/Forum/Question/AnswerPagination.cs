using System.Collections.Generic;
using ShiftCoderQuery.Contract.Forum.Answer;

namespace ShiftCoderQuery.Contract.Forum.Question
{
    public class AnswerPagination
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<AnswerQueryModel> Answers { get; set; }
    }
}