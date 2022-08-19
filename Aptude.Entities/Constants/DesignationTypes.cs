using System.Collections.Generic;

namespace Aptude.Entities.Constants
{
    public static class DesignationTypes
    {
        public static List<string> types { get; set; } = new List<string>
        {
            "Software Engineer",
            "Business Analyst",
            "Project Manager",
            "HR Manager"
        };

        public static bool ValidateType (string type)
        {
            var isValid = types.Exists(t => t == type);

            return isValid;
        }
    }
}
