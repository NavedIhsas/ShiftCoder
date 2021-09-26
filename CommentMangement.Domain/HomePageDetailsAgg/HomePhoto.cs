using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Domain;

namespace CommentManagement.Domain.HomePageDetailsAgg
{
    public class HomePhoto : EntityBase
    {

        [MaxLength(300)]
        public string Picture { get; private set; }
      
        [Required]
        [MaxLength(300)]
        public string PictureAlt { get; private set; }
     
        [MaxLength(300)]
        [Required]
        public string PictureTitle { get; private set; }
    
        [MaxLength(300)]
        public string ButtonsColor { get; private set; }

        public HomePhoto(string picture, string pictureAlt, string pictureTitle, string buttonsColor)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ButtonsColor = buttonsColor;
        }

        public void Edit(string picture, string pictureAlt, string pictureTitle, string buttonsColor)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ButtonsColor = buttonsColor;
        }
    }
}
