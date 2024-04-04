using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Services
{
    public class TaskManagementService : ITaskManagementService
    {
        private readonly DataContext _context;

        public TaskManagementService(DataContext context)
        {
            _context = context;
        }

        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync(); // Commit changes to the database asynchronously

                // Return the created task with its updated properties (e.g., ID)
                return task;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteTask(int id)
        {
            try
            {
                var taskToDelete = await _context.Tasks.FindAsync(id);
                if (taskToDelete == null)
                {
                    return false;
                }

                _context.Tasks.Remove(taskToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks(int limit = 0, int offset = 0)
        {
            IQueryable<TaskModel> query = _context
                .Tasks.AsNoTracking()
                .OrderByDescending(t => t.Id);

            if (limit > 0)
            {
                query = query.Take(limit);
            }
            if (offset > 0)
            {
                query = query.Skip(offset);
            }

            return await query.ToListAsync();
        }

        public Task<TaskModel> GetTaskById(int id)
        {
            var task = _context.Tasks.FindAsync(id);

            return task;
        }

        public async Task<TaskModel> UpdateTask(int id, TaskModel updatedTask)
        {
            try
            {
                // Retrieve the existing task from the database by its ID
                var existingTask = await _context.Tasks.FindAsync(id);

                if (existingTask == null)
                {
                    // Task with the specified ID was not found
                    return null;
                }

                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;

                // Save the changes to the database asynchronously
                await _context.SaveChangesAsync();

                // Return the updated task
                return existingTask;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }
    }
}
