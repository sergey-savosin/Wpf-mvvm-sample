using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
	class AddEditCustomerViewModel : BindableBase
	{
		private bool _EditMode;
		private ICustomersRepository _repo;
		private SimpleEditableCustomer _Customer;
		private Customer _editingCustomer = null;
		public RelayCommand CancelCommand { get; private set; }
		public RelayCommand SaveCommand { get; private set; }
		public event Action Done = delegate { };


		public AddEditCustomerViewModel(ICustomersRepository repo)
		{
			_repo = repo;
			CancelCommand = new RelayCommand(OnCancel);
			SaveCommand = new RelayCommand(OnSave, CanSave);
		}

		public bool EditMode
		{
			get { return _EditMode; }
			set { SetProperty(ref _EditMode, value); }
		}

		public SimpleEditableCustomer Customer
		{
			get { return _Customer; }
			set { SetProperty(ref _Customer, value); }
		}

		public void SetCustomer(Customer cust)
		{
			_editingCustomer = cust;
			if (Customer != null)
			{
				Customer.ErrorsChanged -= RaiseCanExecuteChanged;
			}
			Customer = new SimpleEditableCustomer();
			Customer.ErrorsChanged += RaiseCanExecuteChanged;
			CopyCustomer(cust, Customer);
		}

		private void CopyCustomer(Customer source, SimpleEditableCustomer target)
		{
			target.Id = source.Id;
			if (EditMode)
			{
				target.FirstName = source.FirstName;
				target.LastName = source.LastName;
				target.Phone = source.Phone;
				target.Email = source.Email;
			}
		}

		private void RaiseCanExecuteChanged(object sender, EventArgs e)
		{
			SaveCommand.RaiseCanExecuteChanged();
		}

		private void OnCancel()
		{
			Done();
		}

		private async void OnSave()
		{
			Done();
		}

		private bool CanSave()
		{
			return !Customer.HasErrors;
		}
	}
}
