namespace SoftUniBazar.Data
{
    public static class DataConstants
    {
        public const int AdNameMaxLenght = 25;
        public const int AdNameMinLenght = 5;

        public const int AdDescriptionMinLenght = 15;
        public const int AdDescriptionMaxLenght = 250;

        public const string DateFormat = "yyyy-MM-dd H:mm";

        public const int CategoryNameMinLenght = 3;
        public const int CategoryMaxLenght = 15;

        public const string ErrorStringLenghtMessage = "The {0} field must be between {2} and {1}!";
        public const string ErrorRequiredMessage = "The field is required!";
    }
}
