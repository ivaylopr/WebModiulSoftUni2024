namespace Library.Data
{
    public static class DataConstants
    {
        public const int BookTitleMaxLenght = 50;
        public const int BookTitleMinLenght = 10;
        public const int AuthorMaxLenght = 50;
        public const int AuthorMinLenght = 5;
        public const int DescriptionMinLenght = 5;
        public const int DescriptionMaxLenght = 5000;
        public const double RatingMinRange = 0.00;
        public const double RatingMaxRange = 10.00;


        public const int CategoryNameMaxLenght = 50;
        public const int CategoryNameMinLenght = 5;

        public const string RangeRatingErrorMessage = "The rating must be between {1} and {2}";
        public const string RequiredErrorMessage = "The {0} field is required!";
        public const string StringLenghtErrorMessage = "The {0} field should be between {2} and {1} characters long!";
    }
}
