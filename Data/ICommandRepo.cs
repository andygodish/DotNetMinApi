using DotnetMinApi.Models;

namespace DotnetMinApi.Data
{
    public interface ICommandRepo
    {
        // Method signature, not an implementation
        Task SaveChanges(); // Change to the db context takes two steps, add/delete/update something, then save the changes to the context
        Task<Command?> GetCommandById(int id); // from .Models
        Task<IEnumerable<Command>> GetAllCommands();
        Task CreateCommand(Command cmd);
        
        void DeleteCommand(Command cmd);
    }
}

