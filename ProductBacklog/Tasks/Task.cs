﻿namespace ProductBacklog.Tasks
{
    public class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskProgressStatus Status { get; set; }
    }
}
