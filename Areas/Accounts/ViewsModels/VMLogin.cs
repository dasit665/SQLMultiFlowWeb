using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Accounts.ViewsModels
{
    public class VMLogin
    {
        [Required(ErrorMessage ="Не введено имя пользователя")]
        [Display(Name ="Login:")]
        [RegularExpression(@"[a-z]{3,48}", ErrorMessage ="Латинские символы длиной от 3 до 48 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не введен пароль")]
        [Display(Name ="Password:")]
        public string Password { get; set; }
    }
}