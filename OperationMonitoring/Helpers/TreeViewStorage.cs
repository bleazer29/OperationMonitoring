using OperationMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Helpers
{
    public class TreeViewStorage
    {
        public string id { get { return Storage.Id.ToString(); } }
        public string text { get { return Storage.Name; } }
        public string icon { get { return Storage.Name; } }
        public string parent { 
            get {
                if (Storage.Parent != null)
                    return Storage.Parent.Id.ToString();
                else return "#";
            } 
        }
        public TreeViewItemState state { get; set; }
        Storage Storage;
        public LinkAttributes a_attr { get; set; }
        public TreeViewStorage(Storage storage)
        {
            Storage = storage;
            state = new TreeViewItemState() { disabled = false, opened = true, selected = false };
            a_attr = new LinkAttributes() { href = "/Storages/Details/id=" + id };
        }
    }
}
