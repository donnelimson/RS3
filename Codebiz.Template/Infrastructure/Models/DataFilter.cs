using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ApprovalTemplate;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class DataSearch
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int TotalRecordCount { get; set; }
        public int FilteredRecordCount { get; set; }
        public List<AllAppUserApprovalTemplateModalDetailsDTO> AllApprovalTemplateData { get; set; }
        public List<AllAppUserApprovalTemplateModalDetailsDTO> AllAppUserData { get; set; }
        public List<AllAprovalStageLookUpDTO> AllApprovalStageData { get; set; }
        public List<AllAprovalStageLookUpForUpdateDTO> AllApprovalStageForUpdateData { get; set; }
    }

    /// <summary>
    /// The data filter attributes class.
    /// </summary>
    public class DataFilter
    {
        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        public int Draw { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the sort column.
        /// </summary>
        /// <value>
        /// The sort column.
        /// </value>
        public string SortColumn { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public string SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the column filters.
        /// </summary>
        /// <value>
        /// The column filters.
        /// </value>
        public List<ColumnFilter> ColumnFilters { get; set; }

        /// <summary>
        /// Gets or sets the total record count.
        /// </summary>
        /// <value>
        /// The total record count.
        /// </value>
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the filtered record count.
        /// </summary>
        /// <value>
        /// The filtered record count.
        /// </value>
        public int FilteredRecordCount { get; set; }
    }
}
