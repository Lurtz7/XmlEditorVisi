using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Generic;
using System.Text;




namespace XmlEditor.Core
{
    public class ResourceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            
            Resource resource;
            if (value is Resource)
            {
                resource = value as Resource;
            }
            else
            {
                resource = (value as BindingGroup).Items[0] as Resource;
            }


            var validator = Validator.Validate(resource);

            if (!validator.ValidDate)
                return new ValidationResult(false, "Date is not in correct format.");

            if (!validator.ValidLanguage)
                return new ValidationResult(false, "Language is not in correct format. Use language+region, French as used in Canada(fr-CA)");

            if (!validator.ValidTenant)
                return new ValidationResult(false, "Tenant is not recognized.");

            return ValidationResult.ValidResult;
        }
    }
}
