namespace Homies.Data
{
    public static class DataConstants
    {
        public const int EventNameMaxLenght = 20;
        public const int EventNameMinLenght = 5;
        public const int EventDescriptionMaxLenght = 150;
        public const int EventDescriptionMinLenght = 15;

        public const int TypeNameMaxLenght = 15;
        public const int TypeNameMinLenght = 5;

        public const string DateFormat = "yyyy-MM-dd H:mm";

        public const string StringLenghtErrorMessage = "The {0} field must be between {2} and {1} characters long!";
        public const string RequiredErrorMessage = "The {0} field is required!";

    }
}
