namespace Unisis.Web.Models;

public class HomeDashboardViewModel
{
    public StudentProfile SampleStudent { get; set; } = new()
    {
        Role = UserRole.Student,
        FullName = "Ayse Yilmaz",
        UniversityId = "2023123456",
        Faculty = "Muhendislik Fakultesi",
        Department = "Bilgisayar Muhendisligi",
        ClassYear = "3"
    };

    public AcademicianProfile SampleAcademician { get; set; } = new()
    {
        Role = UserRole.Academician,
        FullName = "Dr. Ogr. Uyesi Mehmet Kaya",
        UniversityId = "AKD-104",
        Faculty = "Muhendislik Fakultesi",
        Department = "Bilgisayar Muhendisligi",
        Title = "Dr. Ogr. Uyesi",
        OfficeLocation = "A Blok 205"
    };

    public List<AvailabilitySlot> SampleAvailability { get; set; } =
    [
        new AvailabilitySlot
        {
            Date = "2026-04-07",
            StartTime = "10:00",
            EndTime = "12:00",
            Note = "Ofis gorusmesi"
        },
        new AvailabilitySlot
        {
            Date = "2026-04-08",
            StartTime = "14:00",
            EndTime = "16:00",
            Note = "Danismanlik saati"
        }
    ];
}
