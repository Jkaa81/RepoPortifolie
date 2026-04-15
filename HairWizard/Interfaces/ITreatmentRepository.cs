using HairWizard.Models;

namespace HairWizard.Interfaces
{

    public interface ITreatmentRepository
    {
        List<Treatment> GetAll();
        Treatment? GetById(int id);
    }

}
