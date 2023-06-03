using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace OEC.Models
{
    [ModelMetadataType(typeof(FarmMetaDats))]
    public partial class Farm : IValidatableObject
    {
        OECContext _context = new OECContext();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // throw new System.NotImplementedException();
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name Cannot be blank or Empty", new[] { "Name" });
            }
            

            // town or County Validation
            if (string.IsNullOrEmpty(Town) && string.IsNullOrEmpty(County))
            {
                yield return new ValidationResult("Atleast Enter Town or County", new[] { "Town" });
                yield return new ValidationResult("Atleast Enter Town or County", new[] { "County" });
            }

            //Province Code Validation
            if (ProvinceCode.Length != 2)
            {
                yield return new ValidationResult("Province cannot be 0 or greater than 2", new[] { "ProvinceCode" });
            }
            ProvinceCode = ProvinceCode.ToUpper();

            var _ProvinceCode = _context.Farm.FirstOrDefault(a => a.ProvinceCode == ProvinceCode);
            if (_ProvinceCode == null)
            {
                yield return new ValidationResult(" Invalid Province code, It doesn't match our record.", new[] { "ProvinceCode" });
            }


            //Postal Code Validation
            Regex CanadianCodePattern = new Regex((@"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d"), RegexOptions.IgnoreCase);
            Regex USCodePattern = new Regex((@"^\d{5}(-\d{4})?"), RegexOptions.IgnoreCase);

            if (PostalCode != null)
            {
                if (CanadianCodePattern.IsMatch(PostalCode.Trim()))
                {
                    if (!PostalCode.Contains(" "))
                    {
                        PostalCode = PostalCode.Insert(3, " ");
                        PostalCode = PostalCode.Trim().ToUpper();
                    }
                    else
                    {
                        PostalCode = PostalCode.Trim().ToUpper();
                    }
                }
                else if (USCodePattern.IsMatch(PostalCode.Trim()))
                {
                    if (!PostalCode.Contains(" "))
                    {
                        PostalCode = PostalCode.Insert(5, "-");
                        PostalCode = PostalCode.Trim().ToUpper();
                    }
                    else
                    {
                        PostalCode = PostalCode.Trim().ToUpper();
                    }
                }
                else
                {
                    yield return new ValidationResult("The Postal Code is not in correct format", new[] { "PostalCode" });
                }
            }


            Regex MobileNoPattern = new Regex(@"^\d{3}-\d{3}-\d{4}");

            
            if (HomePhone != null)
            {
                if (MobileNoPattern.IsMatch(HomePhone))
                {
                    if (!HomePhone.Contains("-"))
                    {
                        HomePhone = HomePhone.Insert(3, "-");
                        HomePhone = HomePhone.Insert(7, "-");
                        HomePhone = HomePhone.Trim();
                    }
                }
                else
                {
                    yield return new ValidationResult("Mobile number is not valid", new[] { "HomePhone" });
                }
            }

            if (CellPhone != null)
            {
                if (MobileNoPattern.IsMatch(CellPhone))
                {
                    if (!CellPhone.Contains("-"))
                    {
                        CellPhone = CellPhone.Insert(3, "-");
                        CellPhone = CellPhone.Insert(7, "-");
                        CellPhone = CellPhone.Trim();
                    }
                }
                else
                {
                    yield return new ValidationResult("CellPhone number is not valid", new[] { "CellPhone" });
                }
            }

            if (DateJoined != null)
            {
                if(DateJoined > DateTime.Now)
                {
                    yield return new ValidationResult("Date cannot be in future", new[] { "DateJoined" });
                }
            }

            if (LastContactDate != null)
            {
               if(DateJoined == null)
                {
                    yield return new ValidationResult("Date Joined Cannot be null", new[] { "DateJoined" });
                }
               else if (LastContactDate < DateJoined)
                {
                    yield return new ValidationResult("Last contact date cannot be before Date Joined", new[] { "LastContactDate" });
                }
            }



            if (Name != null)
            {
                Name = Name.Trim();
            }
            if (Address != null)
            {
                Address = Address.Trim();
            }
            if (Town != null)
            {
                Town = Town.Trim();
            }
            if (County != null)
            {
                County = County.Trim();
            }

            if (Email != null)
            {
                Email = Email.Trim();
            }
            if (Directions != null)
            {
                Directions = Directions.Trim();
            }
            yield return ValidationResult.Success;
        }
    }

    public class FarmMetaDats
    {
        public int FarmId { get; set; }
        [Required(ErrorMessage ="Farm Name is Required")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        [Required(ErrorMessage = "Province Code is Required")]
        public string ProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Directions { get; set; }
        public DateTime? DateJoined { get; set; }
        public DateTime? LastContactDate { get; set; }
    }
}
