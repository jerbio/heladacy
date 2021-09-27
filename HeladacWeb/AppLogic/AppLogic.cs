using HeladacWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.AppLogic
{
    public class AppLogic
    {
        protected HeladacDbContext context;
        bool _persistAfterChanges { get; set; } = true;
        public AppLogic(HeladacDbContext context)
        {
            this.context = context;
        }
        public Task persistDbChanges()
        {
            return context.SaveChangesAsync();
        }

        /// <summary>
        /// This is a flag that verifies if the pertinent object instantiation should be commited to the database.
        /// So if this is set to true, any changes or instantiation of a pertaining object will be persisted.
        /// However, additionally all the child objects that descendant of this calss will not persisted if this is set to false.
        /// </summary>
        virtual public bool persistAfterChanges
        { 
            get {
                return _persistAfterChanges;
            }
            set {
                _persistAfterChanges = value;
            }
        }
    }
}
