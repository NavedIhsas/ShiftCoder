using ShiftCoderQuery.Contract.Forum.Answer;

namespace ShiftCoderQuery.Contract.Forum.Question
{
    public interface IForumQuery
    {
        long AddQuestion(AddQuestionQueryModel command);
        QuestionPagination QuestionCourse(long id, int pageId = 1);
        QuestionQueryModel ShowQuestion(long questionId, int pageId = 1);
        void AddAnswer(AddAnswerQueryModel command);
    }
}