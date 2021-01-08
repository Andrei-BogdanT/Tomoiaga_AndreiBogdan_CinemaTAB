using System;

namespace Tomoiaga_AndreiBogdan_CinemaTAB.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
