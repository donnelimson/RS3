using Codebiz.Domain.Common.Model.ViewModel.CityTown;
using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.CityTown
{
    public class CityTownIndexDTO
    {
        public int CityTownID { get; set; }
        [DisplayName("CODE")]
        public string CityTownCode { get; set; }
        [DisplayName("CITY TOWN")]
        public string CityTown { get; set; }

        [DisplayName("PROVINCE")]
        public string Province { get; set; }

        [DisplayName("STATUS")]
        public bool Status { get; set; }
       
        [DisplayName("CREATED BY")]
        public string FullName { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime CreatedOn { get; set; }


    }
   
    public class GetDetatilsCityTownDTO : CityTownAddUpdateViewModel
    {
        public int? RegionId { get; set; }
    }
}
