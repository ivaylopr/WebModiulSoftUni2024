﻿using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Models.Board
{
	public class BoardViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
