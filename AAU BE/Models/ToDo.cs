namespace AAU_BE.Models;

public class Appointment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; }
    
    public DateTime AppointmentDate { get; set; }
    
    public DateTime AppointmentTime { get; set; }
    public bool IsCompleted { get; set; }
}