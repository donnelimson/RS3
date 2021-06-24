using Fluid;
using System.Collections.Generic;

namespace darkstar.reporting.Textilize
{
    public class FluidTemplateContext : Fluid.TemplateContext
    {
        public FluidTemplateContext(IReadOnlyDictionary<string, object> context)
        {
            this.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
            foreach (var pair in context)
            {
                this.SetValue(pair.Key, Fluid.Values.FluidValue.Create(pair.Value));
                if (pair.Value != null)
                {
                    //this.MemberAccessStrategy.Register(pair.Value.GetType());
                    this.MemberAccessStrategy.Register(pair.Value.GetType());
                }
            }
        }
    }
}