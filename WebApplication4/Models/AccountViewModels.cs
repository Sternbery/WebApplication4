using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "User Role")]
        public bool Role { get; set; }
    }

    public class RegisterGiverViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "User Role")]
        public bool Role { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    //Models for binding survey info
    public class SurveyQuestionPassModel {
        [Required]
        public int SurveyID { get; set; }

        public int QuestionID { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int QuestionTypeID { get; set; }

        public string SurveyName { get; set; }

        public List<SurveyMultipleChoicePassModel> MultiChoice { get; set; }
        public List<SurveyMultipleAnswerPassModel> MultiAnswer { get; set; }

        public SurveyQuestion MakeSurveyQuestion() {
            return new SurveyQuestion { QuestionID = this.QuestionID, SurveyID = this.SurveyID, Text = this.Text, QuestionTypeID = this.QuestionTypeID };
        }
    }

   

    public class SurveyMultipleChoicePassModel
    {
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public int ChoiceOrder { get; set; }
        [Required]
        public string AnswerText { get; set; }
    }
    public class SurveyMultipleAnswerPassModel
    {
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public int ChoiceOrder { get; set; }
        [Required]
        public string AnswerText { get; set; }
    }

    public class ResponseAndAnswer {
        public SurveyResponse response { get; set; }
        public IEnumerable<SurveyAnswer> answers { get; set; }

        public ResponseAndAnswer (SurveyResponse sr, IEnumerable<SurveyAnswer> sas)
        {
            response = sr;
            answers = sas;
        }
    }
    public class SurveyAnswer {
        public SurveyMCA mca { get; set; }
        public SurveyMAA maa { get; set; }
        public SurveySAA saa { get; set; }

        public SurveyAnswer(SurveyMAA maa, SurveyMCA mca, SurveySAA saa) {
            this.maa = maa;
            this.mca = mca;
            this.saa = saa;
        }

        public static IEnumerable<SurveyAnswer> FromDB(IEnumerable<SurveyMAA> maas, IEnumerable<SurveyMCA> mcas, IEnumerable<SurveySAA> saas) {
            var sa1 = maas.Select(a => new SurveyAnswer(a, null, null));
            var sa2 = mcas.Select(a => new SurveyAnswer(null, a, null));
            var sa3 = saas.Select(a => new SurveyAnswer(null, null, a));

            var ret = sa1.Concat(sa2).Concat(sa3);

            return ret;
        }


        public int QuestionID { get
            {
                if (mca != null) return mca.QuestionID;
                if (maa != null) return maa.QuestionID;
                if (saa != null) return saa.QuestionID;
                throw new System.Exception("Illegal State: model does not represent an answer record");
            } } 
        public int ResponseID
        {
            get
            {
                if (mca != null) return mca.ResponseID;
                if (maa != null) return maa.ResponseID;
                if (saa != null) return saa.ResponseID;
                throw new System.Exception("Illegal State: model does not represent an answer record");
            }
        }
    }
}
