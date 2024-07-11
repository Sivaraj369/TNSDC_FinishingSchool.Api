using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Bussiness.JWT
{
    public interface IJwtUtils
    {
        string GenerateJwtToken(string CandidateId);
    }
}
