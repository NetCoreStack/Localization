using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    public static class ExceptionFormatter
    {
        private static string LineSeparator = "======================================";

        private static NameValueCollection CollectAdditionalInfo()
        {
            NameValueCollection nameValueCollection = null;
            NameValueCollection additionalInfo = new NameValueCollection();
            if (additionalInfo["MachineName:"] == null)
            {
                additionalInfo.Add("MachineName:", string.Concat("MachineName: ", GetMachineName()));
                DateTime utcNow = DateTime.UtcNow;
                additionalInfo.Add("TimeStamp:", string.Concat("TimeStamp: ", utcNow.ToString(CultureInfo.CurrentCulture)));
                additionalInfo.Add("FullName:", string.Concat("FullName: ", Assembly.GetEntryAssembly().GetCustomAttribute<TargetFrameworkAttribute>().FrameworkName));
                additionalInfo.Add("ApplicationName:", string.Concat("ApplicationName: ", Assembly.GetEntryAssembly().GetName().Name));
                additionalInfo.Add("WindowsIdentity:", string.Concat("WindowsIdentity: ", GetWindowsIdentity()));
                nameValueCollection = additionalInfo;
            }
            else
            {
                nameValueCollection = null;
            }
            return nameValueCollection;
        }

        private static string GetMachineName()
        {
            string machineName = null;
            try
            {
                machineName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }
            catch (SecurityException)
            {
                machineName = "Permission Denied";
            }
            return machineName;
        }

        private static string GetWindowsIdentity()
        {
            string name = null;
            try
            {
                WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
                if (windowsIdentity == null)
                {
                    name = null;
                }
                else
                {
                    name = windowsIdentity.Name;
                }
            }
            catch (SecurityException)
            {
                name = "Permission Denied";
            }
            return name;
        }

        private static void ProcessAdditionalInfo(PropertyInfo propinfo, object propValue, StringBuilder stringBuilder)
        {
            if (!(propinfo.Name == "AdditionalInformation"))
            {
                stringBuilder.AppendFormat("" + Environment.NewLine + "{0}: {1}", propinfo.Name, propValue);
            }
            else if (propValue != null)
            {
                NameValueCollection currAdditionalInfo = (NameValueCollection)propValue;
                if (currAdditionalInfo.Count > 0)
                {
                    stringBuilder.AppendFormat("" + Environment.NewLine + "AdditionalInformation:", new object[1]);
                    int i = 0;
                    while (i < currAdditionalInfo.Count)
                    {
                        stringBuilder.AppendFormat("" + Environment.NewLine + "{0}: {1}", currAdditionalInfo.GetKey(i), currAdditionalInfo[i]);
                        i = i + 1;
                    }
                }
            }
        }

        private static void ReflectException(Exception currException, StringBuilder strEventInfo)
        {
            object propValue = null;
            PropertyInfo[] properties = currException.GetType().GetProperties();
            int ınt32 = 0;
            while (ınt32 < Convert.ToInt32(properties.Length))
            {
                PropertyInfo propinfo = properties[ınt32];
                if (propinfo.Name == "InnerException" ? false : propinfo.Name != "StackTrace")
                {
                    if (!propinfo.CanRead ? false : Convert.ToInt32(propinfo.GetIndexParameters().Length) == 0)
                    {
                        try
                        {
                            propValue = propinfo.GetValue(currException, null);
                        }
                        catch (TargetInvocationException)
                        {
                            propValue = "Access failed";
                        }
                        if (propValue != null)
                        {
                            ProcessAdditionalInfo(propinfo, propValue, strEventInfo);
                        }
                        else
                        {
                            strEventInfo.AppendFormat("" + Environment.NewLine + "{0}: NULL", propinfo.Name);
                        }
                    }
                }
                ınt32 = ınt32 + 1;
            }
        }

        private static string InternalGetMessage(Exception exception)
        {
            StringBuilder eventInformation = new StringBuilder();
            NameValueCollection additionalInfo = CollectAdditionalInfo();
            foreach (string info in additionalInfo)
            {
                eventInformation.AppendFormat("" + Environment.NewLine + "--> {0}", additionalInfo.Get(info));
            }
            if (exception != null)
            {
                Exception currException = exception;
                do
                {
                    eventInformation.AppendFormat("" + Environment.NewLine + "" + Environment.NewLine + "{0}" + Environment.NewLine + "{1}", "Exception Information Details:", LineSeparator);
                    eventInformation.AppendFormat("" + Environment.NewLine + "{0}: {1}", "Exception Type", currException.GetType().FullName);
                    ReflectException(currException, eventInformation);
                    if (currException.StackTrace != null)
                    {
                        eventInformation.AppendFormat("" + Environment.NewLine + "" + Environment.NewLine + "{0} " + Environment.NewLine + "{1}", "StackTrace Information Details:", LineSeparator);
                        eventInformation.AppendFormat("" + Environment.NewLine + "{0}", currException.StackTrace);
                    }
                    currException = currException.InnerException;
                } while (currException != null);
            }
            return eventInformation.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="values">Dictionary values</param>
        /// <returns></returns>
        public static string CreateMessage(Exception exception, IDictionary<string, object> values = null)
        {
            var resultString = new StringBuilder();
            if (values != null)
            {
                resultString.AppendFormat("AdditionalInfo:");
                foreach (KeyValuePair<string, object> item in values)
                {
                    resultString.AppendFormat("" + Environment.NewLine + "--> {0}: {1}", item.Key, item.Value);
                }
                resultString.AppendLine(Environment.NewLine + LineSeparator);
            }

            resultString.AppendLine(InternalGetMessage(exception));
            return resultString.ToString();
        }
    }
}