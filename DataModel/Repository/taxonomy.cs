//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class taxonomy
    {
        public int taxonomy_id { get; set; }
        public Nullable<int> parent_taxonomy_id { get; set; }
        public string text { get; set; }
        public Nullable<bool> archive { get; set; }
        public string definition { get; set; }
        public string magic_tree { get; set; }
        public Nullable<bool> is_practice { get; set; }
        public Nullable<int> legacy_id { get; set; }
        public Nullable<int> legacy_parent_id { get; set; }
    }
}
