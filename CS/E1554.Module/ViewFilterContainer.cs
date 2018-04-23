using System;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;

namespace E1554.Module {
    [NonPersistent]
    public class ViewFilterContainer : BaseObject {
        public ViewFilterContainer(Session session) : base(session) { }
        private ViewFilterObject _Filter;
        [DataSourceProperty("Filters")]
        [ImmediatePostData]
        public ViewFilterObject Filter {
            get { return _Filter; }
            set { SetPropertyValue("Filter", ref _Filter, value); }
        }
        private XPCollection<ViewFilterObject> _Filters;
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ViewFilterObject> Filters {
            get {
                if (_Filters == null && ObjectType != null) {
                    _Filters = new XPCollection<ViewFilterObject>(PersistentCriteriaEvaluationBehavior.InTransaction, Session, new BinaryOperator("ObjectType", ObjectType));
                }
                return _Filters;
            }
        }
        [CriteriaOptions("ObjectType")]
        [ImmediatePostData]
        public string Criteria {
            get { return Filter != null ? Filter.Criteria : String.Empty; }
            set {
                if (Filter != null) {
                    Filter.Criteria = value;
                }
            }
        }
        private Type _ObjectType;
        [MemberDesignTimeVisibility(false)]
        public Type ObjectType {
            get { return _ObjectType; }
            set { SetPropertyValue("ObjectType", ref _ObjectType, value); }
        }
    }
}
