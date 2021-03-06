namespace Homework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        客戶資料Entities db = new 客戶資料Entities();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (db.客戶聯絡人.Where(c => c.Id != this.Id && c.Email == this.Email && c.客戶Id == this.客戶Id && c.IsDelete == false).Any())
            {
                yield return new ValidationResult("該聯絡人Email已存在，不可重複", new string[] { "Email" });
            }
        }

    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]

        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "請輸入正確手機號碼")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
