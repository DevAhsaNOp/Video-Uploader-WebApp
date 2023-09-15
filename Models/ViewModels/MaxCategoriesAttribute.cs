using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoUploaderWebApp.Models.ViewModels
{
    public class MaxCategoriesAttribute : ValidationAttribute
    {
        private readonly int _maxCategories;

        public MaxCategoriesAttribute(int maxCategories)
        {
            _maxCategories = maxCategories;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var categories = value as List<string>;

            if (categories == null || categories.Count > _maxCategories)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    public class CheckBoxRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is bool)
            {
                return (bool)value;
            }

            return false;
        }
    }
}