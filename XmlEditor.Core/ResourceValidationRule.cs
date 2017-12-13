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
            IsValid isValid = new IsValid();
            Resource resource = (value as BindingGroup).Items[0] as Resource;

            List<IsValid> valid = isValid.ValidList(resource);
            if (valid[0].Valid)
            {
                return ValidationResult.ValidResult;
            }
            else if(!valid[0].ValidDate)
            {
                return new ValidationResult(false, "Date is not in correct format.");
            }
            else if (!valid[0].ValidLanguage)
            {
                return new ValidationResult(false, "Language is not in correct format.");
            }
            else if (!valid[0].ValidTenant)
            {
                return new ValidationResult(false, "Tenant is not recognized.");
            }
            else 
            {
                return new ValidationResult(false, "Gör om gör rätt!");
            }
        }
    }
}
