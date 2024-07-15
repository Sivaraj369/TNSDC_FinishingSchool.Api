using System;
using System.Collections.Generic;

namespace TNSDC_FinishingSchool.Domain.Models;

public partial class Otpverification
{
    public int Id { get; set; }

    public int VerificationModeId { get; set; }

    public long MobileNo { get; set; }

    public int VerificationCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsVerified { get; set; }

    public virtual VerificationMode VerificationMode { get; set; } = null!;
}
