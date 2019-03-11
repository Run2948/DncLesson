using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(10,ErrorMessage = "用户名长度不能超过10位")]
        public string UserName { get; set; }
          
        //[Required(ErrorMessage = "密码不能为空")]
        [StringLength(6,ErrorMessage = "密码长度不能超过6位")]
        public string Password { get; set; }
    }
}
