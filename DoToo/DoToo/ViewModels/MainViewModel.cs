using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DoToo.Models;
using DoToo.Repositories;
using DoToo.Views;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
	public class MainViewModel : ViewModel
	{
		private readonly ITodoItemRepository _todoItemRepository;

		public ObservableCollection<TodoItemViewModel> Items { get; set; }

		public TodoItemViewModel SelectedItem
		{
			get { return null; }
			set
			{
				//gross
				Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
				RaisePropertyChanged(nameof(SelectedItem));
			}
		}

		public bool ShowAll { get; set; }

		public string FilterText => ShowAll ? "All" : "Active";

		public MainViewModel(ITodoItemRepository todoItemRepository)
		{
			_todoItemRepository = todoItemRepository;
			_todoItemRepository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
			_todoItemRepository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

			Task.Run(async () => await LoadData());
		}

		public ICommand AddItem => new Command(async () =>
		{
			var newItemView = Resolver.Resolve<NewTodoItemView>();
			await Navigation.PushAsync(newItemView);
		});

		public ICommand ToggleFilter => new Command(async () =>
		{
			ShowAll = !ShowAll;
			await LoadData();
		});

		private async Task LoadData()
		{
			var todoItems = await _todoItemRepository.GetItems();
			if (!ShowAll)
			{
				todoItems = todoItems.Where(x => !x.Completed).ToList();
			}
			var itemViewModels = todoItems.Select(CreateTodoItemViewModel);
			Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
		}

		private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
		{
			var vm = new TodoItemViewModel(item);
			vm.ItemStatusChanged += OnItemStatusChanged;
			return vm;
		}

		private void OnItemStatusChanged(object sender, EventArgs e)
		{
			if (sender is TodoItemViewModel itemVm)
			{
				if (!ShowAll && itemVm.Item.Completed)
				{
					Items.Remove(itemVm);
				}

				Task.Run(async () => await _todoItemRepository.UpdateItem(itemVm.Item));
			}
		}

		private async Task NavigateToItem(TodoItemViewModel itemViewModel)
		{
			var itemView = Resolver.Resolve<NewTodoItemView>();
			var vm = itemView.BindingContext as NewTodoItemViewModel;
			vm.Item = itemViewModel.Item;

			await Navigation.PushAsync(itemView);
		}
	}
}
