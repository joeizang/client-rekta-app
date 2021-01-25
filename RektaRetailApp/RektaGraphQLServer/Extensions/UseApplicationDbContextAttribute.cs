using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using RektaRetailApp.Domain.Data;

namespace RektaGraphQLServer.Extensions
{
    public class UseRektaContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member
        )
        {
            descriptor.UseDbContext<RektaContext>();
        }
    }
}