using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Customers;
using ZzaDashboard.OrderPrep;
using ZzaDashboard.Orders;

namespace ZzaDashboard
{
	class MainWindowViewModel : BindableBase
	{
		private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
		private OrderViewModel _orderViewModel = new OrderViewModel();
		private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
		private AddEditCustomerViewModel _addEditViewModel = new AddEditCustomerViewModel();

		private BindableBase _CurrentViewModel;

		public MainWindowViewModel()
		{
			NavCommand = new RelayCommand<string>(OnNav);
			_customerListViewModel.PlaceOrderRequested += NavToOrder;
			_customerListViewModel.AddCustomerRequested += NavToAddCustomer;
			_customerListViewModel.EditCustomerRequested += NavToEditCustomer;
		}

		public BindableBase CurrentViewModel
		{
			get { return _CurrentViewModel; }
			set { SetProperty(ref _CurrentViewModel, value); }
		}

		public RelayCommand<string> NavCommand { get; private set; }

		private void OnNav(string destination)
		{
			switch (destination)
			{
				case "orderPrep":
					CurrentViewModel = _orderPrepViewModel;
					break;
				case "customers":
				default:
					CurrentViewModel = _customerListViewModel;
					break;
			}
		}

		private void NavToOrder(Guid customerId)
		{
			_orderViewModel.CustomerId = customerId;
			CurrentViewModel = _orderViewModel;
		}

		private void NavToAddCustomer(Customer cust)
		{
			_addEditViewModel.EditMode = false;
			_addEditViewModel.SetCustomer(cust);
			CurrentViewModel = _addEditViewModel;
		}

		private void NavToEditCustomer(Customer cust)
		{
			_addEditViewModel.EditMode = true;
			_addEditViewModel.SetCustomer(cust);
			CurrentViewModel = _addEditViewModel;
		}
	}
}
