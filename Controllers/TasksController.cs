using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly ITaskManagementService _taskManagementService;

        public TasksController(ITaskManagementService taskManagementService)
        {
            _taskManagementService = taskManagementService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTasks()
        {
            int limit = GetQueryStringValue("limit", defaultValue: 0);
            int offset = GetQueryStringValue("offset", defaultValue: 0);

            var tasks = await _taskManagementService.GetAllTasks(limit, offset);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetTasksByID(int id)
        {
            var tasks = await _taskManagementService.GetTaskById(id);
            if (tasks == null)
            {
                return BadRequest("Task not found");
            }
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateTask(TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                // return BadRequest(ModelState); // Returns validation errors
                var errors = ModelState
                    .Keys.SelectMany(key =>
                        ModelState[key]
                            .Errors.Select(error => new ValidationErrorModel(
                                key,
                                error.ErrorMessage
                            ))
                    )
                    .ToList();

                return Content(HttpStatusCode.BadRequest, new { Errors = errors });
            }

            var newTask = await _taskManagementService.CreateTask(task);

            return Content(HttpStatusCode.Created, newTask);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateTask(int id, TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                // return BadRequest(ModelState); // Returns validation errors
                var errors = ModelState
                    .Keys.SelectMany(key =>
                        ModelState[key]
                            .Errors.Select(error => new ValidationErrorModel(
                                key,
                                error.ErrorMessage
                            ))
                    )
                    .ToList();

                return Content(HttpStatusCode.BadRequest, new { Errors = errors });
            }

            var updatedTask = await _taskManagementService.UpdateTask(id, task);

            return Content(HttpStatusCode.OK, updatedTask);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be emoty or negative");
            }

            var deletedTask = await _taskManagementService.DeleteTask(id);
            if (!deletedTask)
            {
                return Content(HttpStatusCode.NotFound, "Task Not found");
            }

            return Content(HttpStatusCode.OK, "Deleted Success");
        }

        private int GetQueryStringValue(string key, int defaultValue)
        {
            string value = Request
                .GetQueryNameValuePairs()
                .FirstOrDefault(q => q.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                .Value;

            return !string.IsNullOrEmpty(value) && int.TryParse(value, out int result)
                ? result
                : defaultValue;
        }
    }
}
