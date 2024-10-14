namespace HospitalAppointment.WebAPI.Models
{
    public class Appointment:Entity<Guid>
    {
        public Appointment() {
        PatientName=string.Empty;
        Doctor=new Doctor();
        }

     

        public string PatientName { get; set; } 

        public int DoctorId { get; set; } 

        public DateTime AppointmentDate { get; set; } 
        public Doctor Doctor { get; set; }
    }
}
