//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TRPManagement.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Program
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public decimal TRPScore { get; set; }
        public int ChannelId { get; set; }
        public System.TimeSpan AirTime { get; set; }
    
        public virtual Channel Channel { get; set; }
    }
}
