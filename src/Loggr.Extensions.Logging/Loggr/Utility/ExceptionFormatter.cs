using System;
using System.Globalization;
using System.Net;

namespace Loggr.Utility
{
    public class ExceptionFormatter
    {
        private ExceptionFormatter()
        {
        }

        public static string FormatType(string type)
        {
            if (type == null || type.Length == 0)
            {
                return string.Empty;
            }

            int lastDotIndex = CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(type, '.');

            if (lastDotIndex > 0)
            {
                type = type.Substring(lastDotIndex + 1);
            }

            const string conventionalSuffix = "Exception";

            if (type.Length > conventionalSuffix.Length)
            {
                int suffixIndex = type.Length - conventionalSuffix.Length;

                if (string.Compare(type, suffixIndex, conventionalSuffix, 0, conventionalSuffix.Length) == 0)
                {
                    type = type.Substring(0, suffixIndex);
                }
            }

            return type;
        }

        public static string FormatType(Exception ex)
        {
            if (ex == null)
            {
                throw new System.ArgumentNullException("error");
            }

            return FormatType(ex.GetType().ToString());
        }

        public static string Format(Exception ex)
        {
            return Format(ex, null);
        }

        public static string Format(Exception ex, object traceObject)
        {
            string res = "";

            // send basic info
            res += string.Format("<b>Exception</b>: {0}<br />", ex.Message);
            res += string.Format("<b>Type</b>: {0}<br />", ex.GetType().ToString());
            res += string.Format("<b>Machine</b>: {0}<br />", System.Environment.MachineName);

            res += "<br />";

            if (traceObject != null)
            {
                res += "<br />";
                res += "<b>Traced Object(s)</b><br />";
                res += "<br />";
                res += ObjectDumper.DumpObject(traceObject, 1);
            }

            res += "<br />";
            res += "<b>Stack Trace</b><br />";
            res += "<br />";
            res += string.IsNullOrEmpty(ex.ToString()) ? "No stack trace" : string.Format("<pre>{0}</pre>", ex.ToString());

            return res;
        }
    }
}
