using System;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;

namespace WinSample.Module {
    [DefaultProperty("FilterName")]
    public class ViewFilterObject : BaseObject {
        public ViewFilterObject(Session session) : base(session) { }
        private string _ObjectTypeName;
        [MemberDesignTimeVisibility(false)]
        public string ObjectTypeName {
            get { return _ObjectTypeName; }
            set { SetPropertyValue<string>("ObjectTypeName", ref _ObjectTypeName, value); }
        }
        [NonPersistent, MemberDesignTimeVisibility(false)]
        public Type ObjectType {
            get { return ObjectTypeName != null ? XafTypesInfo.Instance.FindTypeInfo(ObjectTypeName).Type : null; }
            set {
                string stringValue = value == null ? null : value.FullName;
                string savedObjectTypeName = ObjectTypeName;
                try {
                    if (stringValue != ObjectTypeName) {
                        ObjectTypeName = stringValue;
                    }
                } catch (Exception) {
                    ObjectTypeName = savedObjectTypeName;
                }
                Criteria = String.Empty;
            }
        }
        private string _Criteria;
        [CriteriaObjectTypeMember("ObjectType")]
        public string Criteria {
            get { return _Criteria; }
            set { SetPropertyValue("Criteria", ref _Criteria, value); }
        }
        private string _FilterName;
        public string FilterName {
            get { return _FilterName; }
            set { SetPropertyValue("FilterName", ref _FilterName, value); }
        }
    }
}
