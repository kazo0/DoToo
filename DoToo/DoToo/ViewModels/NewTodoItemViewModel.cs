
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DoToo.Models;
using DoToo.Repositories;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
	public class NewTodoItemViewModel : ViewModel
	{
		private readonly ITodoItemRepository _todoItemRepository;

		public TodoItem Item { get; set; }

		public NewTodoItemViewModel(ITodoItemRepository todoItemRepository)
		{
			_todoItemRepository = todoItemRepository;
			Item = new TodoItem
			{
				Due = DateTime.Now.AddDays(1),
			};
		}

		public ICommand Save => new Command(async () =>
		{
			await _todoItemRepository.AddOrUpdate(Item);
			await Navigation.PopAsync();
		});
	}
}
