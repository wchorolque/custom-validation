using System;

namespace CookBook.Recipes.Core.DefectTracker.Attributes
{
    [AttributeUsage(AttributeTargets.Class |
            AttributeTargets.Constructor |
            AttributeTargets.Field |
            AttributeTargets.Method |
            AttributeTargets.Property,
                    AllowMultiple = true)]
    public class DefectTrackerAttribute : Attribute
    {
        #region Public Properties
        public int DefectID { get; set; }
        public string ResolvedBy { get; set; }
        public string ResolvedOn { get; set; }
        public string Comments { get; set; }
        #endregion

        public DefectTrackerAttribute (int defectID, string resolvedBy, string resolvedOn, 
                                       string comments = "")
        {
            this.DefectID = defectID;
            this.ResolvedBy = resolvedBy;
            this.ResolvedOn = resolvedOn;
            this.Comments = comments;
        }
    }
}

