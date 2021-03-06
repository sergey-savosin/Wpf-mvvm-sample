using System;
using Zza.Data;
using ZzaDashboard.Customers;
using ZzaDashboard.OrderPrep;
using ZzaDashboard.Orders;

using Unity;

namespace ZzaDashboard
{
	class MainWindowViewModel : BindableBase
	{
		private OrderViewModel _orderViewModel = new OrderViewModel();
		private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
		private CustomerListViewModel _customerListViewModel;
		private AddEditCustomerViewModel _addEditViewModel;
		private BindableBase _CurrentViewModel;

		public MainWindowViewModel()
		{
			NavCommand = new RelayCommand<string>(OnNav);

			_customerListViewModel = ContainerHelper.Container.Resolve<CustomerListViewModel>();
			_addEditViewModel = ContainerHelper.Container.Resolve<AddEditCustomerViewModel>();

			_customerListViewModel.PlaceOrderRequested += NavToOrder;
			_customerListViewModel.AddCustomerRequested += NavToAddCustomer;
			_customerListViewModel.EditCustomerRequested += NavToEditCustomer;
			_addEditViewModel.Done += NavToCustomerList;
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

		private void NavToCustomerList()
		{
			CurrentViewModel = _customerListViewModel;
		}
	}
}
