﻿namespace TodoApi.Models
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool IsComplete { get; set; }
        public bool IsImportant { get; set; }

    }
}
