using HairWizard.Data;
using HairWizard.Interfaces;
using HairWizard.Models;

namespace HairWizard.Persistence
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly HairWizardContext _context;

        public TreatmentRepository(HairWizardContext context)
        {
            _context = context;
        }

        public List<Treatment> GetAll() => _context.Treatments.ToList();

        public Treatment? GetById(int id) => _context.Treatments.Find(id);
    }
}