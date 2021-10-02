using _0_FrameWork.Domain;

namespace CommentManagement.Domain.SliderAgg
{
    public class Slider : EntityBase
    {
        public Slider(string picture, string pictureAlt, string pictureTitle, string buttonText, string title,
            string shortTitle, string buttonLink)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ButtonText = buttonText;
            Title = title;
            ShortTitle = shortTitle;
            ButtonLink = buttonLink;
        }

        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Title { get; private set; }
        public string ShortTitle { get; private set; }
        public string ButtonText { get; private set; }
        public string ButtonLink { get; private set; }

        public void Edit(string picture, string pictureAlt, string pictureTitle, string buttonText, string title,
            string shortTitle, string buttonLink)
        {
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ButtonText = buttonText;
            Title = title;
            ShortTitle = shortTitle;
            ButtonLink = buttonLink;
        }
    }
}