using System.Collections.Generic;

namespace Codebiz.Domain.Common.Model.ViewModel.Division
{
    public class DivisionViewModel
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
