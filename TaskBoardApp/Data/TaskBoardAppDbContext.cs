using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Models;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data
{
	public class TaskBoardAppDbContext : IdentityDbContext
	{
		private IdentityUser testUser = GetUser();
		private Board openBoard { get; set; }
		private Board inPorgressBoard { get; set; }
		private Board doneBoard { get; set; }
		public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.
				Entity<Task>()
				.HasOne(t => t.Board)
				.WithMany(b => b.Tasks)
				.HasForeignKey(t => t.BoardId)
				.OnDelete(DeleteBehavior.Restrict);

			//SeedUsers();
			builder.Entity<IdentityUser>().HasData(testUser);

			SeedBoards();
			builder.Entity<Board>().HasData(openBoard, inPorgressBoard, doneBoard);

			builder.Entity<Task>().HasData(
				new Task()
				{
					Id = 1,
					Title = "Improve CSS styles",
					Description = "Improve better styling for all public pages",
					CreatedOn = DateTime.Now.AddDays(-200),
					OwnerId = testUser.Id,
					BoardId = openBoard.Id
				},
				new Task()
				{
					Id = 2,
					Title = "Android client App",
					Description = "Create android client app for the Task Restful Api",
					CreatedOn = DateTime.Now.AddMonths(-5),
					OwnerId = testUser.Id,
					BoardId = openBoard.Id
				},
				new Task()
				{
					Id = 3,
					Title = "Desktop client App",
					Description = "Create Windows Forms desktop app for the TaskBoard",
					CreatedOn = DateTime.Now.AddYears(-1),
					OwnerId = testUser.Id,
					BoardId = openBoard.Id
				},
				new Task()
				{
					Id = 4,
					Title = "Create Task",
					Description = "Implement [Create Task] page for adding new tasks",
					CreatedOn = DateTime.Now.AddYears(-1),
					OwnerId = testUser.Id,
					BoardId = openBoard.Id
				}

			);
			base.OnModelCreating(builder);
		}

		private void SeedBoards()
		{
			openBoard = new Board()
			{
				Id = 1,
				Name = "Open"
			};
			inPorgressBoard = new Board()
			{
				Id = 2,
				Name = "In Progress"
			};
			doneBoard = new Board()
			{
				Id = 3,
				Name = "Done"
			};
		}

		private static IdentityUser GetUser()
		{
			var hasher = new PasswordHasher<IdentityUser>();
			var user = new IdentityUser() { 
				UserName = "softuni@test.bg",
				NormalizedUserName = "SOFTUNI@TEST.BG"
			};
			user.PasswordHash = hasher.HashPassword(user, "softuni");
			return user;
		}

		public DbSet<Board> Boards { get; set; }
		public DbSet<Task> Tasks { get; set; }
	}
}
