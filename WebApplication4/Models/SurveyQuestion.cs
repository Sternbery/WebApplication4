//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SurveyQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SurveyQuestion()
        {
            this.SurveyMAAs = new HashSet<SurveyMAA>();
            this.SurveyMAQs = new HashSet<SurveyMAQ>();
            this.SurveyMCAs = new HashSet<SurveyMCA>();
            this.SurveyMCQs = new HashSet<SurveyMCQ>();
            this.SurveySAAs = new HashSet<SurveySAA>();
        }
    
        public int QuestionID { get; set; }
        public int SurveyID { get; set; }
        public string Text { get; set; }
        public int QuestionTypeID { get; set; }
    
        public virtual Survey Survey { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SurveyMAA> SurveyMAAs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SurveyMAQ> SurveyMAQs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SurveyMCA> SurveyMCAs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SurveyMCQ> SurveyMCQs { get; set; }

        [Display(Name = "Question Type")]
        public virtual TypeEnum TypeEnum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SurveySAA> SurveySAAs { get; set; }
        public object UserID { get; internal set; }
    }
}
