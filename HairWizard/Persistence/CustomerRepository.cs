using HairWizard.Data;

namespace HairWizard.Persistence
{
    public class CustomerRepository
    {

        private readonly HairWizardContext _ctx;

        public CustomerRepository(HairWizardContext ctx)
        {
            _ctx = ctx;
        }

    }
}
