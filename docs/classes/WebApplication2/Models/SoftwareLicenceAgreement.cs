using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication2.Models
{

    //public class RequiredIfOtherFieldIsNullAttribute : ValidationAttribute, IClientValidatable
    //{
    //    private readonly string _otherProperty;
    //    public RequiredIfOtherFieldIsNullAttribute(string otherProperty)
    //    {
    //        _otherProperty = otherProperty;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var property = validationContext.ObjectType.GetProperty(_otherProperty);
    //        if (property == null)
    //        {
    //            return new ValidationResult(string.Format(
    //                CultureInfo.CurrentCulture,
    //                "Unknown property {0}",
    //                new[] { _otherProperty }
    //            ));
    //        }
    //        var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

    //        if (otherPropertyValue == null || otherPropertyValue as string == string.Empty)
    //        {
    //            if (value == null || value as string == string.Empty)
    //            {
    //                return new ValidationResult(string.Format(
    //                    CultureInfo.CurrentCulture,
    //                    FormatErrorMessage(validationContext.DisplayName),
    //                    new[] { _otherProperty }
    //                ));
    //            }
    //        }

    //        return null;
    //    }

    //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    //    {
    //        var rule = new ModelClientValidationRule
    //        {
    //            ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
    //            ValidationType = "requiredif",
    //        };
    //        rule.ValidationParameters.Add("other", _otherProperty);
    //        yield return rule;
    //    }
    //}
    public class SoftwareLicenceAgreement
    {
        [Required]
        public string AgreementText { get; set; } =
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ille incendat? 
Duo Reges: constructio interrete. 
Quae ista amicitia est? Quare conare, quaeso. 
Negat enim summo bono afferre incrementum diem.Hoc simile tandem est? Quod equidem non reprehendo; Non potes, nisi retexueris illa.
Erat enim Polemonis. Et nemo nimium beatus est; Quid de Pythagora?
Eam stabilem appellas.Age sane, inquam.Stoici scilicet. 
Dat enim intervalla et relaxat.Tibi hoc incredibile, quod beatissimum.";

        [StringLength(50)] public string SoftwareProductName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Licensee Name")]
        public string LicenseeName { get; set; }

        [ConfirmValue(true, ErrorMessage = "Please accept the licensing agreement.")]
        [Display(Name = "Agreement Accepted")]
        public bool AgreementAccepted { get; set; }
    }


    public class SoftwareLicenceAgreementModel
    {
        [Required]
        public string AgreementText { get; set; } =
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ille incendat? 
Duo Reges: constructio interrete. 
Quae ista amicitia est? Quare conare, quaeso. 
Negat enim summo bono afferre incrementum diem.Hoc simile tandem est? Quod equidem non reprehendo; Non potes, nisi retexueris illa.
Erat enim Polemonis. Et nemo nimium beatus est; Quid de Pythagora?
Eam stabilem appellas.Age sane, inquam.Stoici scilicet. 
Dat enim intervalla et relaxat.Tibi hoc incredibile, quod beatissimum.";

        [StringLength(50)] public string SoftwareProductName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Licensee Name")]
        public string LicenseeName { get; set; }

        [ConfirmValue(true, ErrorMessage = "Please accept the licensing agreement")]
        [Display(Name = "Agreement Accepted")]
        public bool AgreementAccepted { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ConfirmValueAttribute : ValidationAttribute, IClientModelValidator
    {
        private object _expectedValue;

        public ConfirmValueAttribute(object expectedValue)
        {
            _expectedValue = expectedValue;

        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return Equals(value, _expectedValue);

        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (string.IsNullOrWhiteSpace(ErrorMessage))
            {
                ErrorMessage = string.Format("{0} must be true.", context.ModelMetadata.DisplayName);
            }

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-confirmvalue", ErrorMessage);
            context.Attributes.Add("data-val-confirmvalue-expectedvalue", _expectedValue.ToString());
        }
    }




}