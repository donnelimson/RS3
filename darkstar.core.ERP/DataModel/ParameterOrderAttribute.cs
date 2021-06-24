using System;

namespace Codebiz.Domain.ERP.Model.Data
{
    public class ParameterOrderAttribute : Attribute
    {
        public int Order { get; private set; }
        public ParameterOrderAttribute(int order)
        {
            Order = order;
        }
    }

    public class WidthAttribute : Attribute
    {
        public int Width { get; private set; }
        public WidthAttribute(int width)
        {
            Width = width;
        }
    }
}
