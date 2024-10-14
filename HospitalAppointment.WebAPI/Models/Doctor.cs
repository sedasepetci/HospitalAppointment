using System;
using HospitalAppointment.WebAPI.Models.Enums;

namespace HospitalAppointment.WebAPI.Models
{
    
    public class Doctor: Entity<int>
    {
     
     public Doctor() {
        Name=string.Empty;
        }

      

        public string Name { get; set; }

        public Branch Branch { get; set; }
 
        public IEnumerable<Appointment> Appointments { get; set; }
       
      
    }  
   
   
}
