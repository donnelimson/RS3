using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web
{
    public class AllowedFileExtensions : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public AllowedFileExtensions(string fileExtensions)
        {
            AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file != null)
            {
                var fileName = file.FileName;

                bool extensionValid = AllowedExtensions.Any(y => fileName.EndsWith(y));

                return extensionValid ? ValidationResult.Success : new ValidationResult("Please use a file with .png, .jpg or .jpeg extension.");
            }

            return ValidationResult.Success;
        }
    }
}