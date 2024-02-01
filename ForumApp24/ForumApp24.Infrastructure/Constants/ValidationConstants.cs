namespace ForumApp24.Infrastructure.Constants
{
    public static class ValidationConstants
    {
        /// <summary>
        /// Title Max lenght
        /// </summary>
        public const int TitleMaxLenght = 50;
        /// <summary>
        /// Title min lenght
        /// </summary>
        public const int TitleMinLenght = 10;
        /// <summary>
        /// Content Max lenght
        /// </summary>
        public const int ContentMaxLenght = 1500;
        /// <summary>
        /// Content min lenght
        /// </summary>
        public const int ContentMinLenght = 30;
        /// <summary>
        /// Requared error message
        /// </summary>
        public const string ErrorMessegaRequared = "The {0} is requared, please insert it!";
        /// <summary>
        /// String lenght error
        /// </summary>
        public const string StringLenghtErrorMessage = "The {0} must be between {2} and {1}";
    }
}
