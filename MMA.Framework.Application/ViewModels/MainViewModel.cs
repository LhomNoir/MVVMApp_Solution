using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMA.Framework.Application.ViewModels
{
    public class MainViewModel : MainViewModelBase
    {
        #region Properties

        #endregion


        #region -- Construtor --
        public MainViewModel()
        {

        } 
        #endregion

        #region -- Méthods --
        public void Exit()
        {
            Environment.Exit(0);
        }

        #endregion
    }
}
