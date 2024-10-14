namespace HospitalAppointment.WebAPI.Models
{
    public class Appointment:Entity<Guid>
    {
        public string PatientName { get; set; } =  string.Empty;

        public int DoctorId { get; set; } 

        public DateTime AppointmentDate { get; set; } 
        public Doctor? Doctor { get; set; }
    }
}
