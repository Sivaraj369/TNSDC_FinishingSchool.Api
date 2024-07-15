using System;
using System.Collections.Generic;

namespace TNSDC_FinishingSchool.Domain.Models;

public partial class VerificationMode
{
    public int Id { get; set; }

    public string VerificationType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Otpverification> Otpverifications { get; set; } = new List<Otpverification>();
}
