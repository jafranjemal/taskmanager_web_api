using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services.Interfaces
{
    public interface ITaskManagementService
    {
        #region GET

        Task<IEnumerable<TaskModel>> GetAllTasks(int limit = 0, int offset = 0);
        Task<TaskModel> GetTaskById(int id);

        #endregion


        Task<TaskModel> CreateTask(TaskModel task);
        Task<TaskModel> UpdateTask(int id, TaskModel task);
        Task<bool> DeleteTask(int id);
    }
}
