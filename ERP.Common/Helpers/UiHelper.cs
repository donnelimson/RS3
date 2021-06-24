namespace WERP.Common.Helpers
{
    public static class UiHelper
    {
        public static string GetCssClassForPStatusName(string pStatusReason)
        {

            if (pStatusReason == null) return "label label-default";

            string pStatusReasonName = pStatusReason.ToUpper();


            switch (pStatusReasonName)
            {
                case "S":
                    return "label label-primary";
                case "B":
                    //                    return "label label-default";
                    return "label label-danger";
                case "A":
                    //                    return "label label-default";
                    return "label label-danger";
                default:
                    //                    return "label label-default";
                    return "label label-danger";
            }

        }
    }
}