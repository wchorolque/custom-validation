using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using CookBook.Recipes.Core.DefectTracker.Attributes;

namespace CookBook.Recipes.Core.DefectTracker
{
    public class DeffectTrackerProcessor
    {
        public string GetDetails (string assemblyPath, string className)
        {
            StringBuilder details = new StringBuilder ();
            Assembly assembly = Assembly.LoadFrom (assemblyPath);
            Type type = assembly.GetType (className, true, true);

            //check whether the constructors have the custom attribute
            ConstructorInfo[] constructorInfo = type.GetConstructors ();
            foreach (ConstructorInfo item in constructorInfo) {
                //IEnumerable<DefectTrackerAttribute> attributes = item.GetCustomAttributes<DefectTrackerAttribute>();
                object[] attributes = item.GetCustomAttributes (false);
                details.Append (GetMemberDetails (item.Name, attributes));
            }

            //check whether the methods have custom attribute
            MethodInfo[] methodInfo = type.GetMethods ();

            foreach (var item in methodInfo) {
                //IEnumerable<DefectTrackerAttribute> attributes = item.GetCustomAttributes<DefectTrackerAttribute> (false);
                object[] attributes = item.GetCustomAttributes (false);
                if (attributes.Length > 0) {
                    details.Append (GetMemberDetails (item.Name, attributes));
                }
            }

            return details.ToString ();
        }

        //public string GetMemberDetails (string memberName, IEnumerable<DefectTrackerAttribute> attributes)
        public string GetMemberDetails (string memberName, object[] attributes)
        {
            StringBuilder sb = new  StringBuilder ();
            sb.Append ("\n");
            if (false == sb.ToString ().Contains (memberName)) {
                sb.Append (memberName);
            }
            foreach (var attribute in attributes) {
                DefectTrackerAttribute attr = (DefectTrackerAttribute)attribute;
                sb.Append ("ID-");
                sb.Append (attr.DefectID);
                sb.Append ("\t");
                sb.Append ("Resolved By-");
                sb.Append (attr.ResolvedBy);
                sb.Append ("\t");
                sb.Append ("Resolved On-");
                sb.Append (attr.ResolvedOn);
            }

            return sb.ToString ();
        }
    }
}

