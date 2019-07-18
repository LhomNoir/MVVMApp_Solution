using MMA.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Application
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App //: Application
    {

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //ApplicationThemeHelper.UpdateApplicationThemeName();

            InitializeInstances();
        }

        private void InitializeInstances()
        {
            var locator = InstanceLocator.Current;

            //locator.RegisterInstance<IModelFactory, ModelFactory>();
            //locator.RegisterInstance<IMetaModelRepository, MetaModelRepository>();
            //locator.RegisterInstance<IEntityService, EntityService>();
            //locator.RegisterInstance<IMetaDataService, MetaDataService>();
        }

        #endregion
    }
}
