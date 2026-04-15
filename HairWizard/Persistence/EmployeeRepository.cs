using HairWizard.Data;
using HairWizard.Interfaces;
using HairWizard.Models;

namespace HairWizard.Persistence
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HairWizardContext _ctx;

        public EmployeeRepository(HairWizardContext ctx)
        {
            _ctx = ctx;
        }

        public Employee? GetById(int id)
        {
            var room = _ctx.Employees.Find(id);
            return room;
        }

        public List<Employee> GetAll()
        {
            var list = _ctx.Employees.ToList();   //ToList method never returns null(instead, if no data, it will just return empty list), so we dont need to check for nulls
            return list;
        }

        public void Add(Employee employee)
        {
            if (employee == null) return;

            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();
        }

        public void Update(Employee employee)
        {
            if (employee == null) return;

            var employeeToUpdate = _ctx.Employees.Find(employee.EmployeeId);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.Name = employee.Name;


                _ctx.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var employee = _ctx.Employees.Find(id);
            if (employee != null)
            {
                _ctx.Employees.Remove(employee);
                _ctx.SaveChanges();
            }
        }
    }
}
