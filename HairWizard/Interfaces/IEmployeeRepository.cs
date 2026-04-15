using HairWizard.Models;

namespace HairWizard.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee Employee);
        void Delete(int id);
        List<Employee> GetAll();
        Employee? GetById(int id);
        void Update(Employee employee);



    }
}
