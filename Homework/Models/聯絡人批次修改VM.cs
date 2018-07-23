using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework.Models
{
    public class 聯絡人批次修改VM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 職稱 { get; set; }
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "請輸入正確手機號碼")]
        public string 手機 { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        public int 客戶Id { get; set; }
    }
}