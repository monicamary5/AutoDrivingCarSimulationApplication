using System.ComponentModel.DataAnnotations;

namespace AutoDrivingCarSimulationApplication.Models
{
    public class CarSimulationInput
    {
        [Required(ErrorMessage = "Please Enter Width")]
        public int? Width { get; set; }
        [Required(ErrorMessage = "Please Enter Height")]
        public int? Height { get; set; }
        [Required(ErrorMessage = "Please Enter Car Current Position in (x,y) format")]
        public string CurrentPosition { get; set; }
        public Direction FacingDirection { get; set; }

        [Required(ErrorMessage = "Please enter command For Car simulation")]
        public string Commands { get; set; }
        public string Opr { get; set; }
        public string? Positionresult { get; set; }
        public string? Directionresult { get; set; }
    }
}
