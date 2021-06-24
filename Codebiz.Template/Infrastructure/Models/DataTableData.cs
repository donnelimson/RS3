using System.Collections.Generic;

namespace Infrastructure.Models
{
    /// <summary>
    /// Data Table Data class.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class DataTableData<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        public int Draw { get; set; }

        /// <summary>
        /// Gets or sets the records total.
        /// </summary>
        /// <value>
        /// The records total.
        /// </value>
        public int RecordsTotal { get; set; }

        /// <summary>
        /// Gets or sets the records filtered.
        /// </summary>
        /// <value>
        /// The records filtered.
        /// </value>
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<T> DataList { get; set; }
    }
}
