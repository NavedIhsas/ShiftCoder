using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace ShiftCoderQuery.Contract.Forum.Question
{
    public class AddQuestionQueryModel
    {
        public long CourseId { get; set; }
        public long? AccountId { get; set; }
        public string Body { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(50,ErrorMessage = "نام نباید بیشتر از 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(70, ErrorMessage = "موضوع نباید بیشتر از 70 کاراکتر باشد")]
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool IsTrue { get; set; }
        public List<QuestionQueryModel> LatestQuestion { get; set; }
        public AddQuestionQueryModel()
        {
            LatestQuestion = new List<QuestionQueryModel>();
        }
    }
}