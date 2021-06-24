namespace Infrastructure.Models
{
    /// <summary>
    /// The column filter
    /// </summary>
    public class ColumnFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnFilter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public ColumnFilter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}
