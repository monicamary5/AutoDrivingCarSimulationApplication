using System.ComponentModel.DataAnnotations;

namespace AutoDrivingCarSimulationApplication.Models
{
    public enum Direction
    {
        [Display(Name ="North")]
        N,
        [Display(Name = "South")]
        S,
        [Display(Name = "West")]
        W,
        [Display(Name = "East")]
        E
    }
}
