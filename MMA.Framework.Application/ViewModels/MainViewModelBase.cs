using MMA.Framework.Application.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMA.Framework.Application.ViewModels
{
    public class MainViewModelBase
    {
        #region Fields

        public const string DOCUMENTS_GROUP_NAME = "DocumentsGroup";

        #endregion

        #region Properties

        public virtual string AboutLabel { get; } = ApplicationLabels.MenuApplicationAboutLabel;
        public virtual string ApplicationLabel { get; } = ApplicationLabels.MenuApplicationLabel;
        public virtual string DocumentsGroupName { get; } = DOCUMENTS_GROUP_NAME;
        public virtual string ExitLabel { get; } = ApplicationLabels.MenuApplicationExitLabel;
        public virtual string GeneralLabel { get; } = ApplicationLabels.MenuCategoryGeneralLabel;
        public virtual string MainMenuLabel { get; } = ApplicationLabels.MainMenuLabel;
        public virtual ObservableCollection<object> Panels { get; } = new ObservableCollection<object>();
        public virtual string StatusBarLabel { get; } = ApplicationLabels.StatusBarLabel;

        #endregion

        public MainViewModelBase()
        {

        }
    }
}