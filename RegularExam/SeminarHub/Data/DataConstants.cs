namespace SeminarHub.Data
{
    public static class DataConstants
    {
        public const int SeminarTopicMaxLenght = 100;
        public const int SeminarTopicMinLenght = 3;
        public const int SeminarLecturerMaxLenght = 60;
        public const int SeminarLecturerMinLenght = 5;
        public const int SeminarDetailsMaxLenght = 500;
        public const int SeminarDetailsMinLenght = 10;
        public const int SeminarDurationMaxRange = 180;
        public const int SeminarDurationMinRange = 30;

        public const string DateFormat = "dd/MM/yyyy HH:mm";

        public const int CategoryNameMaxLenght = 50;
        public const int CategoryNameMinLenght = 3;

        public const string LenghtErrorMessage = "The {0} field must be between {2} and {1} characters long.";
        public const string RequiredErrorMessage = "The {0} field is required.";

    }
}
