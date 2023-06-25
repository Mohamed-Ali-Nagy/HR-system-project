namespace HRSystem.Constants
{
    public class Permission
    {
        public static List<string> createPermissionsListForModel(string model)
        {
            return new List<string>()
            {
                  $"Permission.{model}.View",
                  $"Permission.{model}.Add",
                  $"Permission.{model}.Edit",
                  $"Permission.{model}.Delete",


            };
        }
        public static List<string> creatAllPirmissions()
        {
            List<string> allPermissions = new List<string>();
            var models=Enum.GetValues(typeof(Models));
            foreach (var model in models)
            {
                allPermissions.AddRange(createPermissionsListForModel(model.ToString()));
            }
            return allPermissions;
        }
        //public static class Employee
        //{
           
        //}
    }
}
