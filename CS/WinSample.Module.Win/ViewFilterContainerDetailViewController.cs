using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.Editors;

namespace WinSample.Module {
    public class ViewFilterContainerDetailViewController : ViewController {
        public ViewFilterContainerDetailViewController() {
            TargetObjectType = typeof(ViewFilterContainer);
            TargetViewType = ViewType.DetailView;
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            LookupPropertyEditor editor = (LookupPropertyEditor)((DetailView)View).FindItem("Filter");
            editor.Control.Popup += new EventHandler(Control_Popup);
            editor.Control.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(Control_Closed);
        }
        void Control_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e) {
            if (((LookupEdit)sender).Frame != null) {
                ((LookupEdit)sender).Frame.GetController<NewObjectViewController>().ObjectCreated -= new EventHandler<ObjectCreatedEventArgs>(ViewFilterContainerDetailViewController_ObjectCreated);
            }
        }
        void Control_Popup(object sender, EventArgs e) {
            ((LookupEdit)sender).Frame.GetController<NewObjectViewController>().ObjectCreated += new EventHandler<ObjectCreatedEventArgs>(ViewFilterContainerDetailViewController_ObjectCreated);
        }
        void ViewFilterContainerDetailViewController_ObjectCreated(object sender, ObjectCreatedEventArgs e) {
            if (e.CreatedObject is ViewFilterObject) {
                ViewFilterObject newViewFilterObject = (ViewFilterObject)e.CreatedObject;
                newViewFilterObject.ObjectType = ((ViewFilterContainer)View.CurrentObject).ObjectType;
            }
        }
    }
}
