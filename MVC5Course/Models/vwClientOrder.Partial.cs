namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(vwClientOrderMetaData))]
    public partial class vwClientOrder
    {
    }
    
    public partial class vwClientOrderMetaData
    {
        [Required]
        public int clientid { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        public string firstname { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        public string lastname { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
        public Nullable<decimal> ordertotal { get; set; }
        
        [StringLength(1, ErrorMessage="欄位長度不得大於 1 個字元")]
        public string orderstatus { get; set; }
    }
}
