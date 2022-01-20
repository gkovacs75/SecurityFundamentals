using Microsoft.AspNetCore.Authorization;

namespace SecurityFundamentals.Authorization
{
    // Creating custom Requirements

    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public int ProbationMonths { get; }

        public HRManagerProbationRequirement(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }
    }

    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }

            var employeeDate = DateTime.Parse(context.User.FindFirst(claim => claim.Type == "EmploymentDate").Value);

            var probationPeriod = DateTime.Now - employeeDate;

            if (probationPeriod.Days > (30 * requirement.ProbationMonths))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
