using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DoToo.Models;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
	public class TodoItemViewModel : ViewModel
	{
		public event EventHandler ItemStatusChanged;

		public TodoItem Item { get; private set; }

		public string StatusText => Item.Completed ? "Reactivate" : "Completed";

		public TodoItemViewModel(TodoItem item)
		{
			Item = item;
		}

		public ICommand ToggleCompleted => new Command((_) =>
		{
			Item.Completed = !Item.Completed;
			ItemStatusChanged?.Invoke(this, new EventArgs());
		});

		
	}
}
