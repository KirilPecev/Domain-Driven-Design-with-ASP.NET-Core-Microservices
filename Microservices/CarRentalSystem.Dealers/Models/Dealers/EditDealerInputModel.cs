namespace CarRentalSystem.Dealers.Models.Dealers
{
    using System.ComponentModel.DataAnnotations;

    using static CarRentalSystem.Data.DataConstants.Common;
    using static Data.DataConstants.PhoneNumber;

    public class EditDealerInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(MinPhoneNumberLength)]
        [MaxLength(MaxPhoneNumberLength)]
        [RegularExpression(PhoneNumberRegularExpression)]
        public string PhoneNumber { get; set; }
    }
}
