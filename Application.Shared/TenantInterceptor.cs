using Application.Shared.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Application.Shared
{
    public class TenantInterceptor : IInterceptionBehavior
    {
        public bool WillExecute { get { return true; } }

        //public IEnumerable<Type> GetRequiredInterfaces()
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation context, GetNextInterceptionBehaviorDelegate getNext)
        {
            // before service call
            int originalTenantId = Convert.ToInt32(context.Arguments["tenantId"]);
            int targetTenantId = originalTenantId;

            if (originalTenantId == 51000)
            {
                targetTenantId = 10000;
                System.Diagnostics.Trace.WriteLine($"----\ntranslating {originalTenantId} to {targetTenantId}...");
                context.Arguments["tenantId"] = targetTenantId;
            }

            // process
            System.Diagnostics.Trace.WriteLine($"processing with {targetTenantId}...");
            var result = getNext()(context, getNext);

            // after service call
            System.Diagnostics.Trace.WriteLine("context processed!");

            // if you need to get results and do something
            List<Merchant> merchants = result.ReturnValue as List<Merchant> ?? new List<Merchant>();
            System.Diagnostics.Trace.WriteLine($"Merchants result: {string.Join(", ", merchants.Select(x => x.Name))}\n----");
            return result;
        }
    }
}
