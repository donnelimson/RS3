using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
   public class PoleToExcel
    {


        [DisplayName("POLE ID")]
        public string PoleNo { get; set; }
        [DisplayName("CODE")]
        public string Code { get; set; }
        [DisplayName("LOCATION")]
        public string Location { get; set; }
        [DisplayName("ITEM")]
        public string Item { get; set; }
        [DisplayName("LONGHITUDE - LATITUDE")]
        public string LonghitudeLatitude { get; set; }
        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime DateCreated { get; set; }
    }
}
