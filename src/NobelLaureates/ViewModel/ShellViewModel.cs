using System;
using System.Collections.Generic;
using System.Linq;
using NobelLaureates.HydraVM;
using NobelLaureates.ViewModel.Grid;

namespace NobelLaureates.ViewModel
{
    public class ShellViewModel : HydraViewModelBag
    {
        public ShellViewModel(GridViewModel gridViewModel)
        {
            GridViewModel = gridViewModel;

            AddViewModel(GridViewModel);
        }

        public GridViewModel GridViewModel { get; private set; }
    }
}
