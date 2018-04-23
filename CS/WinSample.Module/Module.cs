using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using System.Reflection;
using DevExpress.ExpressApp.Model;


namespace WinSample.Module {
    public interface IModelListViewExt : IModelNode {
        string AdditionalCriteria { get; set; }
    }

    public sealed partial class WinSampleModule : ModuleBase {
        public WinSampleModule() {
            InitializeComponent();
        }
        public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders) {
            base.ExtendModelInterfaces(extenders);
            extenders.Add<IModelListView, IModelListViewExt>();
        }
    }
}
