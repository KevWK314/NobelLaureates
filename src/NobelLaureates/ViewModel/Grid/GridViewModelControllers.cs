using NobelLaureates.HydraVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NobelLaureates.ViewModel.Grid
{
    public class GridViewModelControllers : HydraController
    {
        private readonly List<HydraController> _controllers = new List<HydraController>();

        public GridViewModelControllers(
            LoadGridData loadGridData)
        {
            _controllers.Add(loadGridData);
        }

        public override void Start()
        {
            _controllers.ForEach(x => x.Start());
        }
    }
}
