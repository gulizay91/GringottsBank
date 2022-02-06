using System.ComponentModel.DataAnnotations;

namespace Bank.Shared
{
    public enum ProcessType
    {
        [Display(Name = "Para Yatırma")]
        WITHDRAW_MONEY,
        [Display(Name = "Para Çekme")]
        WITHDRAWAL
    }
}
