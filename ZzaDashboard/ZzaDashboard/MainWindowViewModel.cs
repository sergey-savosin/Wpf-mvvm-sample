using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private BindableBase _CurrentViewModel;

		public MainWindowViewModel()
		{
			NavCommand = new RelayCommand<string>(OnNav);
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
	}
}
