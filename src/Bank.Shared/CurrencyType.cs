using System.ComponentModel.DataAnnotations;

namespace Bank.Shared
{
    public enum CurrencyType
    {
        [Display(Name = "Gold Galleons")]
        GALLEON,
        [Display(Name = "Silver Sickles")]
        SICKLET,
        [Display(Name = "Bronze Knuts")]
        KNUT
    }
}
