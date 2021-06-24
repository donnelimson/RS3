using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Route
{
    public class RouteModalDTO
    {
        public int Id { get; set; }
        public int UnitsId { get; set; }
        public int RouteId { get; set; }
        public string RouteCode { get; set; }
        public string Barangay { get; set; }
        public string Unit { get; set; }
        public string MeterReader { get; set; }
        public bool CannotDelete { get; set; }
        public bool IsSelected { get; set; }

    }
    public class RouteMeterReadingDTO
    {
        public int Id { get; set; }
        public int MeterReadingId { get; set; }
        public int UnitMeterReadingId { get; set; }
        public int BarangayId { get; set; }
        public string Unit { get; set; }
        public string MeterReader { get; set; }
        public int RouteId { get; set; }

    }

    public class RouteLookUpDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Barangay { get; set; }
    }
}
