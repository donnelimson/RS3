using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
	public class GSISARAFormsViewModel
	{
		public GSISARAFormsViewModel(string formName)
		{
			FormName = formName;
		}

		public string FormName { get; private set; }

		[Display(Name = "Month")]
		[Required]
		public int? Month { get; set; }
		public List<KeyValuePair<int, string>> Months { get; set; }

		[Display(Name = "Year")]
		[Required]
		public int? Year { get; set; }
		public List<KeyValuePair<int, int>> Years { get; set; }
	}
}