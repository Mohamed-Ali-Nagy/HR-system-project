namespace HRSystem.Constants
{
    public class Permission
    {
        public static List<string> createPermissionsListForModel(string model)
        {
            return new List<string>()
            {
                  $"Permission.{model}.View",
                  $"Permission.{model}.Create",
                  $"Permission.{model}.Edit",
                  $"Permission.{model}.Delete",


            };
        }
        public static List<string> creatAllPirmissions()
        {
            List<string> allPermissions = new List<string>();
            var models = Enum.GetNames(typeof(Models));
            foreach (var model in models)
            {
                allPermissions.AddRange(createPermissionsListForModel(model));
            }
            return allPermissions;
        }

        public static class Employee
        {
            public const string View = "Permission.Employee.View";
            public const string Create = "Permission.Employee.Create";
            public const string Edit = "Permission.Employee.Edit";
            public const string Delete = "Permission.Employee.Delete";
        }
        public static class Attendance
        {
            public const string View = "Permission.Attendance.View";
            public const string Create = "Permission.Attendance.Create";
            public const string Edit = "Permission.Attendance.Edit";
            public const string Delete = "Permission.Attendance.Delete";
        }
        public static class GeneralSetting
        {
            public const string View = "Permission.GeneralSetting.View";
            public const string Create = "Permission.GeneralSetting.Create";
            public const string Edit = "Permission.GeneralSetting.Edit";
            public const string Delete = "Permission.GeneralSetting.Delete";
        }
        public static class Holidays
        {
            public const string View = "Permission.Holidays.View";
            public const string Create = "Permission.Holidays.Create";
            public const string Edit = "Permission.Holidays.Edit";
            public const string Delete = "Permission.Holidays.Delete";
        }
        public static class SalaryReport
        {
            public const string View = "Permission.SalaryReport.View";
            public const string Create = "Permission.SalaryReport.Create";
            public const string Edit = "Permission.SalaryReport.Edit";
            public const string Delete = "Permission.SalaryReport.Delete";
        }

    }
}
