using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Invoices.ViewModels
{
    public class InvoiceItemVm
    {
        public int Id { get; set; }

        public string Item { get; set; }

        public double Quantity { get; set; }

        public double Rate { get; set; }

        public double Amout
        {
            get
            {
                return Quantity * Rate;
            }
        }
    }
}
