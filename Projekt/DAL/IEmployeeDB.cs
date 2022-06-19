using Projekt.Models;

namespace Projekt.DAL
{
    public interface IEmployeeDB
    {
        public List<Employee> List();
        public int addNewUser(Employee employee);
    }
}
