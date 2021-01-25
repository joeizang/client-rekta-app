using System.Collections.Generic;

namespace RektaGraphQLServer.Common
{
    public abstract class PayloadBase
    {
        protected PayloadBase(IReadOnlyList<UserError>? errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<UserError>? Errors { get; }
    }
}